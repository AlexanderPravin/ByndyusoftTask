using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Addition : IOperation
{
    public double Execute(double a, double b) => a + b;
}