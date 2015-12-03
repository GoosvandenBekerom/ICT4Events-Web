using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IEventContext : IRepositoryContext<Event>
    {
        List<string> GetTagsByEvent(Event ev);
    }
}
