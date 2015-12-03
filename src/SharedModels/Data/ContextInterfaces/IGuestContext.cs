using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IGuestContext : IRepositoryContext<Guest>
    {
        List<Guest> GetAllByEvent(Event ev);
        Guest GetGuestByEvent(Event ev, int userID);
        List<Guest> GetGuestsByUser(User user);
        List<Guest> GetGuestsByGroup(Event ev, int leaderID);
        Guest GetByRfid(string rfid, Event ev);
        int GetGuestCountByEvent(Event ev);
        int GetGuestCountByLocation(Location location);
    }
}