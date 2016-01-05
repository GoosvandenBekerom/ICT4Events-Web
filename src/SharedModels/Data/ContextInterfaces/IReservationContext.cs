using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IReservationContext : IRepositoryContext<Reservation>
    {
        Reservation GetLastAdded();
    }
}
