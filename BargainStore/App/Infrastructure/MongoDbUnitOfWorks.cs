using App.Domain.Infrastructure;
using MongoDB.Driver;

namespace App.Infrastructure;

public class MongoDbUnitOfWorks(IMongoClient client, ILogger<MongoDbUnitOfWorks> logger)
    : IMongoDbUnitOfWork
{
    private readonly IMongoClient _client = client;
    private IClientSessionHandle? _session;
    private readonly ILogger<MongoDbUnitOfWorks> _logger = logger;
    private bool _disposed;

    public async Task StartTransactionAsync()
    {
        if (_session == null)
        {
            _logger.LogInformation("Starting a new transaction");
            _session = await _client.StartSessionAsync();
            _session.StartTransaction();
            _logger.LogInformation("Transaction started");
        }
    }

    public async Task CommitAsync()
    {
        if (_session != null && !_disposed)
        {
            try
            {
                await _session.CommitTransactionAsync();
                _logger.LogInformation("Transaction committed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Transaction commit failed: {}", ex.Message);
                await _session.AbortTransactionAsync();
                throw;
            }
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
