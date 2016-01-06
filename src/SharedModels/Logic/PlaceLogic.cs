using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class PlaceLogic
    {
        private IPlaceContext _context;

        public PlaceLogic(IPlaceContext context)
        {
            _context = context;
        }

        public Place GetPlaceByID(int id)
        {
            return _context.GetById(id);
        }

        public List<Place> GetAllPlaces()
        {
            return _context.GetAll();
        }

        public List<Place> GetPlacesByEvent(Event ev)
        {
            return _context.GetAllByEvent(ev);
        }

        public bool InsertPlace(Place place)
        {
            return _context.Insert(place);
        }

        public bool UpdatePlace(Place place)
        {
            return _context.Update(place);
        }

        public bool DeletePlace(Place place)
        {
            return _context.Delete(place);
        }
    }
}
