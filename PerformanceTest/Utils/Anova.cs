using System;
using MathNet.Numerics.Distributions;
using System.Linq;
using System.Reflection;

public static class Anova
{
    public static double[] CalculateColumnMeans(int alternatives, int measurementsCount, double[][] measurements)
    {
        double[] columnMeans = new double[alternatives];
        for (int i = 0; i < alternatives; i++)
        {
            double[] column = new double[measurementsCount];
            for (int j = 0; j < measurementsCount; j++)
            {
                column[j] = measurements[j][i];
            }
            columnMeans[i] = CalculateColumnMean(column);
        }
        return columnMeans;
    }

    public static double CalculateColumnMean(double[] column)
    {
        double sum = 0;
        for (int i = 0; i < column.Length; i++)
        {
            sum += column[i];
        }
        return sum / column.Length;
    }

    public static double CalculateColumnVariance(double[] column, double mean)
    {
        double sum = 0;
        for (int i = 0; i < column.Length; i++)
        {
            sum += Math.Pow(column[i] - mean, 2);
        }
        return sum / column.Length;
    }

    public static double CalculateDeviationFromMean(double measurement, double mean)
    {
        return mean - measurement;
    }

    public static double CalculateTotalMean(double[] columnMeans)
    {
        double sum = 0;
        for (int i = 0; i < columnMeans.Length; i++)
        {
            sum += columnMeans[i];
        }
        return sum / columnMeans.Length;
    }

    public static double CalculateEffect(double columnMean, double totalMean)
    {
        return columnMean - totalMean;
    }

    public static double CalculateSSE(int alternatives, int measurementsCount, double[][] measurements, double[] columnMeans)
    {
        double sum = 0;
        for (int i = 0; i < alternatives; i++)
        {
            for (int j = 0; j < measurementsCount; j++)
            {
                sum += Math.Pow(measurements[j][i] - columnMeans[i], 2);
            }
        }
        return sum;
    }

    public static double CalculateSSA(int alternatives, double[] columnMeans, double totalMean, int measurementsCount)
    {
        double sum = 0;
        for (int i = 0; i < alternatives; i++)
        {
            sum += Math.Pow(columnMeans[i] - totalMean, 2);
        }
        return sum * measurementsCount;
    }

    public static double CalculateSST(int alternatives, int measurementsCount, double[][] measurements, double[] columnMeans, double totalMean)
    {
        double sum = 0;
        for (int i = 0; i < alternatives; i++)
        {
            for (int j = 0; j < measurementsCount; j++)
            {
                sum += Math.Pow(measurements[j][i] - totalMean, 2);
            }
        }
        return sum;
    }

    public static double CalculateVarianceSSA(double ssa, int alternatives)
    {
        return ssa / (alternatives - 1);
    }

    public static double CalculateVarianceSSE(double sse, int alternatives, int measurementsCount)
    {
        return sse / (alternatives * (measurementsCount - 1));
    }

    public static double CalculateVarianceSST(double sst, int alternatives, int measurementsCount)
    {
        return sst / ((alternatives * measurementsCount) - 1);
    }

    public static double CalculateF(double varianceSsa, double varianceSse)
    {
        return varianceSsa / varianceSse;
    }

    public static double TabulatedF(int degreesOfFreedom1, int degreesOfFreedom2, double probability = 0.95)
    {
        if (probability >= 1)
        {
            probability = 0.95;
        }

        return FisherSnedecor.InvCDF(degreesOfFreedom1, degreesOfFreedom2, probability);
    }

    public static AnovaSummary CalculateSummary(string path)
    {
        double[][] measurements = FileUtils.ReadCSV(path);
        int measurementsCount = measurements.Length;
        int alternatives = measurements[0].Length;
        return CalculateSummary(alternatives, measurementsCount, measurements);
    }
    public static AnovaSummary CalculateSummary(int alternatives, int measurementsCount, double[][] measurements)
    {
        double[] columnMeans = CalculateColumnMeans(alternatives, measurementsCount, measurements);
        double totalMean = CalculateTotalMean(columnMeans);
        double ssa = CalculateSSA(alternatives, columnMeans, totalMean, measurementsCount);
        double sse = CalculateSSE(alternatives, measurementsCount, measurements, columnMeans);
        double sst = CalculateSST(alternatives, measurementsCount, measurements, columnMeans, totalMean);
        double varianceSsa = CalculateVarianceSSA(ssa, alternatives);
        double varianceSse = CalculateVarianceSSE(sse, alternatives, measurementsCount);
        double varianceSst = CalculateVarianceSST(sst, alternatives, measurementsCount);
        double fValue = CalculateF(varianceSsa, varianceSse);
        double tabulatedF = TabulatedF(alternatives - 1, alternatives * (measurementsCount - 1));
        double[] effects = new double[alternatives];

        for (int i = 0; i < alternatives; i++)
        {
            effects[i] = CalculateEffect(columnMeans[i], totalMean);
        }


        return new AnovaSummary(new double[] { ssa, sse, sst }, new double[] { alternatives - 1, alternatives * (measurementsCount - 1), (alternatives * measurementsCount) - 1 },
            new double[] { varianceSsa, varianceSse, varianceSst }, fValue, tabulatedF, effects);
    }



    public static double CalculateContrast(string path, int system1, int system2)
    {
        double[][] measurements = FileUtils.ReadCSV(path);
        return CalculateContrast(system1, system2, measurements);
    }

    public static double CalculateContrast(int system1, int system2, double[][] measurements)
    {
        int measurementsCount = measurements.Length;
        int alternatives = measurements[0].Length;

        double[] columnMeans = CalculateColumnMeans(alternatives, measurementsCount, measurements);
        double totalMean = CalculateTotalMean(columnMeans);

        double[] effects = new double[alternatives];

        for (int i = 0; i < alternatives; i++)
        {
            effects[i] = CalculateEffect(columnMeans[i], totalMean);
        }

        double c1 = effects[system1 - 1];
        double c2 = effects[system2 - 1];
        double contrast = c1 - c2;

        return contrast;
    }

    public static Tuple<double, double> CalculateContrastInterval(string path, int system1, int system2, double probability = 0.95)
    {
        double[][] measurements = FileUtils.ReadCSV(path);
        return CalculateContrastInterval(measurements, system1, system2, probability);
    }

    public static Tuple<double, double> CalculateContrastInterval(double[][] measurements, int system1, int system2, double probability = 0.95)
    {
        double contrast = CalculateContrast(system1, system2, measurements);
        int measurementsCount = measurements.Length;
        int alternatives = measurements[0].Length;

        double varianceSSE = CalculateVarianceSSE(CalculateSSE(alternatives, measurementsCount, measurements, CalculateColumnMeans(alternatives, measurementsCount, measurements)), alternatives, measurementsCount);

        if (probability >= 1)
        {
            probability = 0.95;
        }
        double alpha = 1 - probability;
        double dof = alternatives * (measurementsCount - 1);

        double t = StudentT.InvCDF(location: 0.0, scale: 1.0, freedom: dof, p: alpha);

        double interval = Math.Sqrt(2 * varianceSSE / (alternatives * measurementsCount));
        if ((interval > 0 && t > 0) || (interval < 0 && t < 0))
        {
            return new Tuple<double, double>(contrast - interval * t, contrast + interval * t);
        }
        else
        {

            return new Tuple<double, double>(contrast + interval * t, contrast - interval * t);
        }
    }
}
