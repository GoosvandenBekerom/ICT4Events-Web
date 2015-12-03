using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface ILocationContext : IRepositoryContext<Location>
    {
        List<Location> GetAllByEvent(Event ev);
    }
}
