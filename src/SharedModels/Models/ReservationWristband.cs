namespace SharedModels.Models
{
    public class ReservationWristband
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }

        public ReservationWristband(int id, int reservationId, int userId)
        {
            Id = id;
            ReservationId = reservationId;
            UserId = userId;
        }
    }
}
