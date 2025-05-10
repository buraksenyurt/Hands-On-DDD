namespace App.Framework;

public interface IUnitOfWork
    :IDisposable
{
    Task CommitAsync();
}
