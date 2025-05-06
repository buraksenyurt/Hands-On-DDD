using App.Domain;
using App.Domain.Exceptions;
using App.Tests.Mocks;

namespace App.Tests;

public class BookEntityTests
{
    private readonly Book _book;
    public BookEntityTests()
    {
        _book = new Book(new BookId(Guid.NewGuid()), new MemberId(Guid.NewGuid()));
    }
    [Fact]
    public void Cannot_Publish_Without_Title()
    {
        _book.UpdateDetails(BookDetails.FromString("Çok iyi durumda, her sayfası ilk günkü gibi yeni. Birinci baskıdan"));
        _book.UpdateSalesPrice(SalesPrice.FromDecimal(19.99M, "TL", new CurrencyCodeInfoLookupMock()));
        Assert.Throws<InvalidEntityStateException>(_book.RequestToPublish);
    }
    [Fact]
    public void Cannot_Publish_Without_Details()
    {
        _book.SetTitle(BookTitle.FromString("Programming with C#,The C# Guru,2005"));
        _book.UpdateSalesPrice(SalesPrice.FromDecimal(19.99M, "TL", new CurrencyCodeInfoLookupMock()));
        Assert.Throws<InvalidEntityStateException>(_book.RequestToPublish);
    }
    [Fact]
    public void Cannot_Publish_Without_SalesPrice()
    {
        _book.SetTitle(BookTitle.FromString("Programming with C#,The C# Guru,2005"));
        _book.UpdateDetails(BookDetails.FromString("Çok iyi durumda, her sayfası ilk günkü gibi yeni. Birinci baskıdan"));
        Assert.Throws<InvalidEntityStateException>(_book.RequestToPublish);
    }
    [Fact]
    public void Cannot_Publish_With_Zero_SalesPrice()
    {
        _book.SetTitle(BookTitle.FromString("Programming with C#,The C# Guru,2005"));
        _book.UpdateDetails(BookDetails.FromString("Çok iyi durumda, her sayfası ilk günkü gibi yeni. Birinci baskıdan"));
        _book.UpdateSalesPrice(SalesPrice.FromDecimal(0.00M, "TL", new CurrencyCodeInfoLookupMock()));
        Assert.Throws<InvalidEntityStateException>(_book.RequestToPublish);
    }
    [Fact]
    public void Can_State_Correct_With_Valid_Book_Data()
    {
        _book.SetTitle(BookTitle.FromString("Programming with C#,The C# Guru,2005"));
        _book.UpdateDetails(BookDetails.FromString("Çok iyi durumda, her sayfası ilk günkü gibi yeni. Birinci baskıdan"));
        _book.UpdateSalesPrice(SalesPrice.FromDecimal(10.00M, "TL", new CurrencyCodeInfoLookupMock()));
        _book.RequestToPublish();
        var expected = Book.BookSalesState.PendingReview;
        Assert.Equal(_book.SalesState, expected);
    }
}
