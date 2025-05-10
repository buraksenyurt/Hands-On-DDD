using App.Framework;
using MongoDB.Driver;

namespace App.Infrastructure.Repositories;

public class MongoDbUnitOfWorks
    : IUnitOfWork
{
    private readonly IMongoClient _client;
    private IClientSessionHandle _session;
    private readonly ILogger<MongoDbUnitOfWorks> _logger;
    private bool _disposed;

    public MongoDbUnitOfWorks(IMongoClient client, ILogger<MongoDbUnitOfWorks> logger)
    {
        _client = client;
        _session = _client.StartSession();
        _session.StartTransaction();
        _logger = logger;
    }

    public async Task CommitAsync()
    {
        try
        {
            await _session.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Transaction commit failed: {}", ex.Message);
            await _session.AbortTransactionAsync();
            throw;
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _session?.Dispose();
            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }
}
