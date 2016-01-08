using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IReservationWristband : IRepositoryContext<ReservationWristband>
    {
        ReservationWristband GetLastAdded();
        List<ReservationWristband> GetReservationByUserIdAll(int id);
    }
}
