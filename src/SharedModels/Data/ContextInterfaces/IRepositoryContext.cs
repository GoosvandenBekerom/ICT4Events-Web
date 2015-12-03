using System.Collections.Generic;

namespace SharedModels.Data.ContextInterfaces
{
    /// <summary>
    /// Generic interface implemented by repositories
    /// </summary>
    /// <typeparam name="TEntity">Type of model implemented</typeparam>
    public interface IRepositoryContext<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(object id);
        TEntity Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}