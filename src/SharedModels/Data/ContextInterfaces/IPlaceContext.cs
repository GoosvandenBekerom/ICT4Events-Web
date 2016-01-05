using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IPlaceContext : IRepositoryContext<Place>
    {
        List<Place> GetAllByEvent(Event ev);
    }
}
