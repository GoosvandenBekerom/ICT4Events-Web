using System;
using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IMaterialContext : IRepositoryContext<Material>
    {
        List<Material> GetAllByEvent(Event ev);
        List<Material> GetAllByEventAndNonReserved(Event ev);
        List<Material> GetReservedMaterialsByGuest(Guest guest);
        List<Material> GetAllReservedMaterials(Event ev);
        Material AddReservation(Material material, int guestId, DateTime startDate, DateTime endDate);
        bool UpdateReservation(Material material, DateTime startDate, DateTime endDate);
        bool RemoveReservation(Material material);
    }
}