using System;

namespace SharedModels.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public int PersonId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Paid { get; set; }

        public Reservation(int id, int personId, DateTime dateStart, DateTime dateEnd, bool paid)
        {
            ID = id;
            PersonId = personId;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Paid = paid;
        }
    }
}
