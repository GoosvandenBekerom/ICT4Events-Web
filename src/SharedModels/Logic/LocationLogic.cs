using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class LocationLogic
    {
        private ILocationContext _context;

        public LocationLogic(ILocationContext context)
        {
            _context = context;
        }

        public Location GetById(int id)
        {
            return _context.GetById(id);
        }

        //public List<Location> GetLocationsByEvent(Event ev)
        //{
        //    return _context.GetAllByEvent(ev);
        //}

        public bool InsertLocation(Location location)
        {
            return _context.Insert(location);
        }

        public bool UpdateLocation(Location location)
        {
            return _context.Update(location);
        }

        public bool DeleteLocation(Location location)
        {
            return _context.Delete(location);
        }
    }
}
