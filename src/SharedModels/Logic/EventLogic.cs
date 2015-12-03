using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class EventLogic
    {
        private readonly IEventContext _context;

        public EventLogic(IEventContext context)
        {
            _context = context;
        }

        public List<Event> GetAllEvents()
        {
            return _context.GetAll();
        }

        public Event GetByID(int id)
        {
            return _context.GetById(id);
        }

        public bool UpdateEvent(Event ev)
        {
            return _context.Update(ev);
        }

        public Event AddEvent(Event ev)
        {
            return _context.Insert(ev);
        }
    }
}
