using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IReservationContext : IRepositoryContext<Reservation>
    {
        Reservation GetLastAdded();
        bool InsertReservationAccount(ReservationAccount reservation);
        int GetCountReservationOfPlace(int id);
    }
}
