using App.Domain;

namespace App.Tests;

public class MoneyInstanceTests
{
    [Fact]
    public void Money_instances_with_the_same_amount_should_be_equal()
    {
        var leftAmount = Money.FromDecimal(49.99M);
        var rightAmount = Money.FromDecimal(49.99M);
        Assert.Equal(leftAmount, rightAmount);
    }
    [Fact]
    public void Sum_of_money_gives_right_amount()
    {
        var amount1 = Money.FromDecimal(12.50M);
        var amount2 = Money.FromDecimal(12.50M);
        var expected = Money.FromDecimal(25.00M);
        var actual = amount1 + amount2;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Subtrahend_of_money_gives_right_amount()
    {
        var amount1 = Money.FromDecimal(12.50M);
        var amount2 = Money.FromDecimal(2.50M);
        var expected = Money.FromDecimal(10.00M);
        var actual = amount1 - amount2;
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Create_money_instance_from_string_should_works()
    {
        var amount = "34.55";
        var expected = Money.FromDecimal(34.55M);
        var actual = Money.FromString(amount);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Sum_of_money_with_different_currency_should_raise_an_exception()
    {
        var amount1 = Money.FromDecimal(34.55M, "USD");
        var amount2 = Money.FromDecimal(350.00M);
        Assert.Throws<Money.MismatchedCurrencyCodeException>(() => amount1 + amount2);
    }
}