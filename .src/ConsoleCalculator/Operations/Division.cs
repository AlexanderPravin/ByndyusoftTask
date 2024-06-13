using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Operations;

public class Division : IOperation
{
    public double Execute(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Division by zero is not allowed");

        return a / b;
    }
}