using ConsoleCalculator.Calculators;
using ConsoleCalculator.Interfaces;
using ConsoleCalculator.Parsers;

Console.WriteLine("Введите выражение:");

var input = Console.ReadLine() ??
               throw new ArgumentException("Expression must be provided");

try
{
    IExpressionParser parser = new ExpressionParser();
    
    ICalculator calculator = new Calculator(parser);
    
    var result = calculator.Calculate(input);
    
    Console.WriteLine($"Результат: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}