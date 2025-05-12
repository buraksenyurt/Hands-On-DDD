namespace App.Framework;

public interface IUnitOfWork
    : IDisposable
{
    Task CommitAsync();
    Task StartTransactionAsync();
}
