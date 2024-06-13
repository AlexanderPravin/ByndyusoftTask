using ConsoleCalculator.Interfaces;
using ConsoleCalculator.Parsers;

namespace ExpressionParserTests;

public class ExpressionParserTest
{
    private readonly IExpressionParser _parser = new ExpressionParser();
    
    [Fact]
    public void TestAddition()
    {
        Assert.Equal(5, _parser.ParseAndEvaluate("2+3"));
    }

    [Fact]
    public void TestSubtraction()
    {
        Assert.Equal(1, _parser.ParseAndEvaluate("3-2"));
    }

    [Fact]
    public void TestMultiplication()
    {
        Assert.Equal(6, _parser.ParseAndEvaluate("2*3"));
    }

    [Fact]
    public void TestDivision()
    {
        Assert.Equal(2, _parser.ParseAndEvaluate("6/3"));
    }

    [Fact]
    public void TestComplexExpression()
    {
        Assert.Equal(7, _parser.ParseAndEvaluate("1+2*3"));
    }

    [Fact]
    public void TestExpressionWithParentheses()
    {
        Assert.Equal(9.3, _parser.ParseAndEvaluate("(1.1+2)*3"));
    }

    [Fact]
    public void TestExpressionWithNestedParentheses()
    {
        Assert.Equal(29, _parser.ParseAndEvaluate("2*((3+4)*2)+1"));
    }
    [Fact]
    public void TestUnaryPlus()
    {
        IExpressionParser parser = new ExpressionParser();
        Assert.Equal(5, parser.ParseAndEvaluate("+5"));
    }

    [Fact]
    public void TestUnaryMinusInExpression()
    {
        IExpressionParser parser = new ExpressionParser();
        Assert.Equal(3, parser.ParseAndEvaluate("5+(-2)"));
    }

    [Fact]
    public void TestMissingOpeningParenthesis()
    {
        IExpressionParser parser = new ExpressionParser();
        var exception = Assert.Throws<ArgumentException>(() => parser.ParseAndEvaluate("2*(3+4*2))+1"));
        Assert.Equal("Недостающая открывающая скобка.", exception.Message);
    }

    [Fact]
    public void TestMissingClosingParenthesis()
    {
        IExpressionParser parser = new ExpressionParser();
        var exception = Assert.Throws<ArgumentException>(() => parser.ParseAndEvaluate("2*(3+(4*2)+1"));
        Assert.Equal("Недостающая скобка в выражении.", exception.Message);
    }

    [Fact]
    public void TestInvalidCharacter()
    {
        IExpressionParser parser = new ExpressionParser();
        var exception = Assert.Throws<ArgumentException>(() => parser.ParseAndEvaluate("2+3a"));
        Assert.Equal("Недопустимый символ в выражении: a", exception.Message);
    }
}   