using System.Globalization;
using System.Text;
using ConsoleCalculator.Interfaces;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator.Parsers;

public class ExpressionParser : IExpressionParser
{
    private readonly Dictionary<char, IOperation> _operations = new()
    {
        { '+', new Addition() },
        { '-', new Subtraction() },
        { '*', new Multiplication() },
        { '/', new Division() }
    };
    
    private readonly Dictionary<char, int> _operationPriority = new()
    {
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 },
        { '(', 0 },
        { ')', 0 }
    };

    public double ParseAndEvaluate(string expression)
    {
        var tokens = new Stack<double>();
        var operators = new Stack<char>();
        var position = 0;
        var expectUnary = true;

        try
        {
            while (position < expression.Length)
            {
                if (char.IsDigit(expression[position]) || expression[position] == '.')
                {
                    ParseNumber(expression, ref position, tokens);
                    expectUnary = false;
                }
                else if (_operations.ContainsKey(expression[position]) || expression[position] == '(' || expression[position] == ')') 
                    HandleOperatorOrParenthesis(expression, ref position, tokens, operators, ref expectUnary);
                else
                    throw new ArgumentException($"Недопустимый символ в выражении: {expression[position]}");
            }

            if (operators.Contains('(') || operators.Contains(')'))
                throw new ArgumentException("Недостающая скобка в выражении.");

            EvaluateRemainingOperations(tokens, operators);
            return tokens.Pop();
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Некорректное выражение.");
        }
    }

    private void ParseNumber(string expression, ref int pos, Stack<double> tokens)
    {
        var sb = new StringBuilder();
        
        while (pos < expression.Length && (char.IsDigit(expression[pos]) || expression[pos] == '.'))
        {
            sb.Append(expression[pos]);
            pos++;
        }
        
        tokens.Push(double.Parse(sb.ToString(), CultureInfo.InvariantCulture));
    }

    private void HandleOperatorOrParenthesis(string expression, 
        ref int pos, 
        Stack<double> tokens, 
        Stack<char> operators, 
        ref bool expectUnary)
    {
        switch (expression[pos])
        {
            case '(':
                operators.Push(expression[pos]);
                expectUnary = true;
                break;
            
            case ')' when !operators.Contains('('):
                throw new ArgumentException("Недостающая открывающая скобка.");
            
            case ')':
                while (operators.Peek() != '(')
                    EvaluateTopOperation(tokens, operators);
                
                operators.Pop();
                expectUnary = false;
                break;
            
            default:
                var operand = expression[pos];
                
                if (expectUnary && operand is '+' or '-')
                    operators.Push(operand == '+' ? 'u' : 'n');
                else
                {
                    while (operators.Count > 0 && _operationPriority[operators.Peek()] >= _operationPriority[operand])
                        EvaluateTopOperation(tokens, operators);

                    operators.Push(operand);
                }

                expectUnary = true;
                break;
        }
        pos++;
    }

    private void EvaluateRemainingOperations(Stack<double> tokens, Stack<char> operators)
    {
        while (operators.Count > 0)
            EvaluateTopOperation(tokens, operators);
    }

    private void EvaluateTopOperation(Stack<double> tokens, Stack<char> operators)
    {
        var op = operators.Pop();
        switch (op)
        {
            case 'u':
                return;
            
            case 'n':
                tokens.Push(-tokens.Pop());
                break;
            
            default:
                if (tokens.Count < 2)
                    throw new ArgumentException("Некорректное выражение.");
                
                var b = tokens.Pop();
                var a = tokens.Pop();
                tokens.Push(_operations[op].Execute(a, b));
                break;
        }
    }
}
