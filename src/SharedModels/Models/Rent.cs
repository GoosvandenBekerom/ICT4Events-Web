using System;

namespace SharedModels.Models
{
    public class Rent
    {
        public int ID { get; }
        public int ItemID { get; set; }
        public int ReservationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }

        public Rent(int id, int itemId, int reservationId, DateTime startDate, DateTime endDate, decimal price, bool paid = false)
        {
            ID = id;
            ItemID = itemId;
            ReservationID = reservationId;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Paid = paid;
        }
    }
}
