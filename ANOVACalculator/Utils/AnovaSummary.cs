using System;
using System.Dynamic;
public class AnovaSummary
{
    public double[] SumOfSquares { get; set; } = new double[3];
    public double[] DegsOfFreedom { get; set; } = new double[3];
    public double[] MeanSquares { get; set; } = new double[2];
    public double ComputedF { get; set; }
    public double TabulatedF { get; set; }
    public double[] Effects { get; set; }

    public AnovaSummary(double[] sumOfSquares, double[] degsOfFreedom, double[] meanSquares, double computedF, double tabulatedF, double[] effects)
    {
        SumOfSquares = sumOfSquares;
        DegsOfFreedom = degsOfFreedom;
        MeanSquares = meanSquares;
        ComputedF = computedF;
        TabulatedF = tabulatedF;
        Effects = effects;
    }

    public override string ToString()
    {
        string str = "";
        str += ("Sum of squares: " + SumOfSquares[0] + ", " + SumOfSquares[1] + ", " + SumOfSquares[2]);
        str += ("\nDegrees of freedom: " + DegsOfFreedom[0] + ", " + DegsOfFreedom[1] + ", " + DegsOfFreedom[2]);
        str += ("\nMean squares: " + MeanSquares[0] + ", " + MeanSquares[1]);
        str += ("\nComputed F: " + ComputedF);
        str += ("\nTabulated F: " + TabulatedF);
        return str;
    }
}
