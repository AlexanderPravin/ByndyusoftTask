using ConsoleCalculator.Parsers;

namespace Tests;

public class ExpressionParserTest
{
    [Fact]
    public void TestAddition()
    {
        var parser = new ExpressionParser();
        Assert.Equal(5, parser.ParseAndEvaluate("2+3"));
    }

    [Fact]
    public void TestSubtraction()
    {
        var parser = new ExpressionParser();
        Assert.Equal(1, parser.ParseAndEvaluate("3-2"));
    }

    [Fact]
    public void TestMultiplication()
    {
        var parser = new ExpressionParser();
        Assert.Equal(6, parser.ParseAndEvaluate("2*3"));
    }

    [Fact]
    public void TestDivision()
    {
        var parser = new ExpressionParser();
        Assert.Equal(2, parser.ParseAndEvaluate("6/3"));
    }

    [Fact]
    public void TestComplexExpression()
    {
        var parser = new ExpressionParser();
        Assert.Equal(7, parser.ParseAndEvaluate("1+2*3"));
    }

    [Fact]
    public void TestExpressionWithParentheses()
    {
        var parser = new ExpressionParser();
        Assert.Equal(9, parser.ParseAndEvaluate("(1+2)*3"));
    }

    [Fact]
    public void TestExpressionWithNestedParentheses()
    {
        var parser = new ExpressionParser();
        Assert.Equal(17, parser.ParseAndEvaluate("2*(3+(4*2))+1"));
    }
}