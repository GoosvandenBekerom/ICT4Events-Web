using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class ReservationWristbandLogic
    {
        private readonly IReservationWristband _context;

        public ReservationWristbandLogic(IReservationWristband context)
        {
            _context = context;
        }

        public List<ReservationWristband> GetAllPersons()
        {
            return _context.GetAll();
        }

        public ReservationWristband GetByID(int id)
        {
            return _context.GetById(id);
        }

        public bool UpdatePerson(ReservationWristband person)
        {
            return _context.Update(person);
        }

        public bool Insert(ReservationWristband person)
        {
            return _context.Insert(person);
        }

        public ReservationWristband GetLastAdded()
        {
            return _context.GetLastAdded();
        }

        public List<ReservationWristband> GetReservationByUserIdAll(int id)
        {
            return _context.GetReservationByUserIdAll(id);
        }
    }
}
