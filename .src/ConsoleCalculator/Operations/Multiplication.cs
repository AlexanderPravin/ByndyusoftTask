using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Multiplication : IOperation
{
    public double Execute(double a, double b) => a * b;
}