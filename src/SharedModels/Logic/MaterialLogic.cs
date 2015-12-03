using System;
using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class MaterialLogic
    {
        private readonly IMaterialContext _contextMaterial;
        private readonly IMaterialTypeContext _contextMaterialType;
        private readonly IGuestContext _contextGuest;

        
        public MaterialLogic()
        {
            _contextGuest = new GuestOracleContext();
            _contextMaterial = new MaterialOracleContext();
            _contextMaterialType = new MaterialTypeOracleContext();
        }

        public MaterialLogic(IMaterialContext context)
        {
            _contextGuest = new GuestOracleContext();
            _contextMaterial = context;
            _contextMaterialType = new MaterialTypeOracleContext();
        }
        
        #region Material
        public Material Insert(Material entity)
        {
            return _contextMaterial.Insert(entity);
        }

        public bool Update(Material entity)
        {
            return _contextMaterial.Update(entity);
        }

        public bool Delete(Material entity)
        {
            return _contextMaterial.Delete(entity);
        }

        public List<Material> GetAllByEvent(Event ev)
        {
            return _contextMaterial.GetAllByEvent(ev);
        }

        public List<Material> GetAllByEventAndNonReserved(Event ev)
        {
            return _contextMaterial.GetAllByEventAndNonReserved(ev);
        }

        public List<Material> GetReservedMaterialsByGuest(Guest guest)
        {
            return _contextMaterial.GetReservedMaterialsByGuest(guest);
        }

        public List<Material> GetAllReservedMaterials(Event ev)
        {
            return _contextMaterial.GetAllReservedMaterials(ev);
        }

        public Material AddReservation(Material material, int guestId, DateTime startDate, DateTime endDate)
        {
            return _contextMaterial.AddReservation(material, guestId, startDate, endDate);
        }

        public bool UpdateReservation(Material material, DateTime startDate, DateTime endDate)
        {
            return _contextMaterial.UpdateReservation(material, startDate, endDate);
        }

        public bool RemoveReservation(Material material)
        {
            return _contextMaterial.RemoveReservation(material);
        }
        #endregion

        #region Material Type
        public List<MaterialType> GetAll()
        {
            return _contextMaterialType.GetAll();
        }

        public MaterialType GetById(object id)
        {
            return _contextMaterialType.GetById(id);
        }

        public MaterialType GetByName(string name)
        {
            return _contextMaterialType.GetByName(name);
        }

        public MaterialType Insert(MaterialType entity)
        {
            return _contextMaterialType.Insert(entity);
        }

        public bool Update(MaterialType entity)
        {
            return _contextMaterialType.Update(entity);
        }

        public bool Delete(MaterialType entity)
        {
            return _contextMaterialType.Delete(entity);
        }
        #endregion
    }
}