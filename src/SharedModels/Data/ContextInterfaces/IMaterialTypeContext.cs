using System;
using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IMaterialTypeContext : IRepositoryContext<MaterialType>
    {
        MaterialType GetByName(string name);
    }
}