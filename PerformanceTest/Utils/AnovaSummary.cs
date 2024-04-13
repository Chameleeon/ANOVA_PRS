using System;
public class AnovaSummary
{
    double[] _sumOfSquares = new double[3];
    double[] _degsOfFreedom = new double[3];
    double[] _meanSquares = new double[2];
    double _computedF;
    double _tabulatedF;

    public AnovaSummary(double[] sumOfSquares, double[] degsOfFreedom, double[] meanSquares, double computedF, double tabulatedF)
    {
        _sumOfSquares = sumOfSquares;
        _degsOfFreedom = degsOfFreedom;
        _meanSquares = meanSquares;
        _computedF = computedF;
        _tabulatedF = tabulatedF;
    }

    public void ToString()
    {
        Console.WriteLine("Sum of squares: " + _sumOfSquares[0] + ", " + _sumOfSquares[1] + ", " + _sumOfSquares[2]);
        Console.WriteLine("Degrees of freedom: " + _degsOfFreedom[0] + ", " + _degsOfFreedom[1] + ", " + _degsOfFreedom[2]);
        Console.WriteLine("Mean squares: " + _meanSquares[0] + ", " + _meanSquares[1]);
        Console.WriteLine("Computed F: " + _computedF);
        Console.WriteLine("Tabulated F: " + _tabulatedF);
    }
}
