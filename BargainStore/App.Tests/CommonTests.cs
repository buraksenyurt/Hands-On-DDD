using App.Domain;

namespace App.Tests;

public class CommonTests
{
    [Fact]
    public void Money_instances_with_the_same_amount_should_be_equal()
    {
        var leftAmount = new Money(49.99M);
        var rightAmount = new Money(49.99M);
        Assert.Equal(leftAmount, rightAmount);
    }
    [Fact]
    public void Sum_of_money_gives_right_amount()
    {
        var amount1 = new Money(12.50M);
        var amount2 = new Money(12.50M);
        var expected = new Money(25.00M);
        var actual = amount1 + amount2;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Subtrahend_of_money_gives_right_amount()
    {
        var amount1 = new Money(12.50M);
        var amount2 = new Money(2.50M);
        var expected = new Money(10.00M);
        var actual = amount1 - amount2;
        Assert.Equal(expected, actual);
    }
}