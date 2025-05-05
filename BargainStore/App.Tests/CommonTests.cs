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
}