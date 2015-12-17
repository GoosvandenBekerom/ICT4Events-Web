using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface ILocationContext : IRepositoryContext<Place>
    {
        List<Place> GetAllByEvent(Event ev);
    }
}
