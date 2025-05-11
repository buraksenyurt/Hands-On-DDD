using App.Framework;
using Microsoft.EntityFrameworkCore.Storage;

namespace App.Infrastructure;

public class PostgresUnitOfWork(MembershipDbContext dbContext, ILogger<PostgresUnitOfWork> logger) 
    : IUnitOfWork, IDisposable
{
    private readonly MembershipDbContext _dbContext = dbContext;
    private IDbContextTransaction _transaction;
    private readonly ILogger<PostgresUnitOfWork> _logger = logger;

    public async Task StartTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await _transaction.RollbackAsync();
            _logger.LogError(ex, "Transaction failed: {Message}", ex.Message);
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
