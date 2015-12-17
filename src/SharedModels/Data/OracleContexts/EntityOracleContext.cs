using System.Collections.Generic;

namespace SharedModels.Data.OracleContexts
{
    public abstract class EntityOracleContext<TEntity> where TEntity : class
    {
        protected abstract TEntity GetEntityFromRecord(List<string> record);
    }
}