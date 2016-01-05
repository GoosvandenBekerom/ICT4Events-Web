using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class ReservationLogic
    {
        private readonly IReservationContext _context;

        public ReservationLogic(IReservationContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAllReservations()
        {
            return _context.GetAll();
        }

        public Reservation GetByID(int id)
        {
            return _context.GetById(id);
        }

        public bool Insert(Reservation reservation)
        {
            return _context.Insert(reservation);
        }

        public Reservation GetLastAdded()
        {
            return _context.GetLastAdded();
        }
    }
}
