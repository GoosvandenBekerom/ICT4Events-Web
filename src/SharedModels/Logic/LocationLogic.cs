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

        public Place GetLocationByID(int id)
        {
            return _context.GetById(id);
        }

        public List<Place> GetLocationsByEvent(Event ev)
        {
            return _context.GetAllByEvent(ev);
        }

        public Place InsertLocation(Place place)
        {
            return _context.Insert(place);
        }

        public bool UpdateLocation(Place place)
        {
            return _context.Update(place);
        }

        public bool DeleteLocation(Place place)
        {
            return _context.Delete(place);
        }
    }
}
