namespace ConsoleCalculator.Interfaces;

public interface IExpressionParser
{
    double ParseAndEvaluate(string expression);
}