namespace App.Framework;

public interface IEntityStore
{
    Task<TEntity> LoadAsync<TEntity>(string id) where TEntity : Entity;
    Task SaveAsync<TEntity>(TEntity entity) where TEntity : Entity;
    Task<bool> IsExistsAsync<TEntity>(string id) where TEntity : Entity;
}
