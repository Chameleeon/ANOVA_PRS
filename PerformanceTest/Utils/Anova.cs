using System;
using MathNet.Numerics.Distributions;

public class Anova
{
    private int _alternativesCount { get; set; }
    private int _measurementsCount { get; set; }
    private double[]? _columnMeans { get; set; }
    private double[][]? _measurements { get; set; }


    public Anova() { }
    public Anova(int alternatives, int measurements)
    {
        _alternativesCount = alternatives;
        _measurementsCount = measurements;
        _columnMeans = new double[alternatives];

        _measurements = new double[alternatives][];
        for (int i = 0; i < alternatives; i++)
        {
            _measurements[i] = new double[measurements];
        }
    }

    public double CalculateColumnMean(double[] column)
    {
        double sum = 0;
        for (int i = 0; i < column.Length; i++)
        {
            sum += column[i];
        }
        return sum / column.Length;
    }

    public double CalculateColumnVariance(double[] column, double mean)
    {
        double sum = 0;
        for (int i = 0; i < column.Length; i++)
        {
            sum += Math.Pow(column[i] - mean, 2);
        }
        return sum / column.Length;
    }

    public double CalculateDeviationFromMean(double measurement, double mean)
    {
        return mean - measurement;
    }

    public double CalculateTotalMean(double[] columnMeans)
    {
        double sum = 0;
        for (int i = 0; i < _alternativesCount; i++)
        {
            sum += columnMeans[i];
        }
        return sum / _alternativesCount;
    }

    public double CalculateEffect(double columnMean, double totalMean)
    {
        return columnMean - totalMean;
    }

    public double CalculateSSE()
    {
        double sum = 0;
        for (int i = 0; i < _alternativesCount; i++)
        {
            for (int j = 0; j < _measurementsCount; j++)
            {
                sum += Math.Pow(_measurements[j][i] - _columnMeans[i], 2);
            }
        }
        return sum;
    }

    public double CalculateSSA()
    {
        double sum = 0;
        for (int i = 0; i < _alternativesCount; i++)
        {
            sum += Math.Pow(_columnMeans[i] - CalculateTotalMean(_columnMeans), 2);
        }
        return sum * _measurementsCount;
    }

    public double CalculateSST()
    {
        double sum = 0;
        for (int i = 0; i < _alternativesCount; i++)
        {
            for (int j = 0; j < _measurementsCount; j++)
            {
                sum += Math.Pow(_measurements[j][i] - CalculateTotalMean(_columnMeans), 2);
            }
        }
        return sum;
    }

    public double CalculateVarianceSSA()
    {
        return CalculateSSA() / (_alternativesCount - 1);
    }

    public double CalculateVarianceSSE()
    {
        return CalculateSSE() / (_alternativesCount * (_measurementsCount - 1));
    }

    public double CalculateVarianceSST()
    {
        return CalculateSST() / (_alternativesCount * _measurementsCount - 1);
    }

    public double CalculateF()
    {
        return CalculateVarianceSSA() / CalculateVarianceSSE();
    }

    public double TabulatedF(int degreesOfFreedom1, int degreesOfFreedom2, double probability = 0.95)
    {
        if (probability >= 1)
        {
            probability = 0.95;
        }


        double tableValue = FisherSnedecor.InvCDF(degreesOfFreedom1, degreesOfFreedom2, probability);
        return tableValue;
    }

    public AnovaSummary CalculateSummary(string path)
    {
        _measurements = FileUtils.ReadCSV("measurements.csv");
        for (int i = 0; i < _alternativesCount; i++)
        {
            for (int j = 0; j < _measurementsCount; j++)
            {
                Console.WriteLine(_measurements[i][j]);
            }
        }
        _alternativesCount = _measurements[0].Length;
        _measurementsCount = _measurements.Length;
        _columnMeans = new double[_alternativesCount];

        for (int i = 0; i < _alternativesCount; i++)
        {
            double[] column = new double[_measurementsCount];
            for (int j = 0; j < _measurementsCount; j++)
            {
                column[j] = _measurements[j][i];
            }
            _columnMeans[i] = CalculateColumnMean(column);
        }

        return new AnovaSummary(
            new double[] { CalculateSSA(), CalculateSSE(), CalculateSST() },
            new double[] { _alternativesCount - 1, _alternativesCount * (_measurementsCount - 1), _alternativesCount * _measurementsCount - 1 },
            new double[] { CalculateVarianceSSA(), CalculateVarianceSSE() },
            CalculateF(),
            TabulatedF(_alternativesCount - 1, _alternativesCount * (_measurementsCount - 1))
        );
    }
}
