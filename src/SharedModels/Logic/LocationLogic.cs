using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Location GetLocationByID(int id)
        {
            return _context.GetById(id);
        }

        public List<Location> GetLocationsByEvent(Event ev)
        {
            return _context.GetAllByEvent(ev);
        }

        public Location InsertLocation(Location location)
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
