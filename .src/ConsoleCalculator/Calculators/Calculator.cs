using ConsoleCalculator.Interfaces;

namespace ConsoleCalculator.Calculators;

public class Calculator(IExpressionParser parser) : ICalculator
{
    public double Calculate(string expression) => 
        parser.ParseAndEvaluate(expression);
}