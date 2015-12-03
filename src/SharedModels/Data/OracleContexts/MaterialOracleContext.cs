using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class MaterialOracleContext : EntityOracleContext<Material>, IMaterialContext
    {
        public List<Material> GetAll()
        {
            var query =
                "SELECT m.*, r.userid, r.datestart, r.dateend FROM material m LEFT OUTER JOIN reservation r ON m.materialid = r.materialid ORDER BY m.materialid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<Material> GetAllByEventAndNonReserved(Event ev)
        {
            var query = 
                "SELECT m.*, r.userid, r.datestart, r.dateend " +
                "FROM material m " +
                "LEFT OUTER JOIN reservation r ON m.materialid = r.materialid " +
                "WHERE eventid = :eventid AND r.userid IS NULL"
                ;
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", (int) ev.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<Material> GetReservedMaterialsByGuest(Guest guest)
        {
            var query =
                "SELECT m.*, r.userid, r.datestart, r.dateend FROM material m LEFT OUTER JOIN reservation r ON m.materialid = r.materialid WHERE eventid = :eventid AND userid = :userid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", guest.EventID),
                new OracleParameter("userid", guest.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<Material> GetAllReservedMaterials(Event ev)
        {
            var query =
                "SELECT m.*, r.userid, r.datestart, r.dateend " +
                "FROM material m " +
                "LEFT OUTER JOIN reservation r ON m.materialid = r.materialid " +
                "WHERE eventid = :eventid AND r.userid IS NOT NULL"
                ;
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", (int) ev.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Material GetById(object id)
        {
            var query =
                "SELECT m.*, r.userid, r.datestart, r.dateend FROM material m LEFT OUTER JOIN reservation r ON m.materialid = r.materialid WHERE m.materialid = :materialid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public Material Insert(Material entity)
        {
            var query =
                "INSERT INTO material (materialid, eventid, materialtypeid, name) VALUES (seq_material.nextval, :eventid, :materialtypeid, :name) RETURNING materialid INTO :lastID";
            var parameters = new List<OracleParameter>
            { 
                new OracleParameter("eventid", entity.EventID),
                new OracleParameter("materialtypeid", entity.TypeID),
                new OracleParameter("name", entity.Name),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
        }

        
        public bool Update(Material entity)
        {
            const string query = "UPDATE material SET name = :name, materialtypeid = :materialtypeid WHERE materialid = :materialid";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialid", entity.ID),
                new OracleParameter("name", entity.Name),
                new OracleParameter("materialtypeid", entity.TypeID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Material entity)
        {
            var query = "DELETE FROM material WHERE materialid = :materialid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialid", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Material> GetAllByEvent(Event ev)
        {
            var query = "SELECT m.*, r.userid, r.datestart, r.dateend FROM material m LEFT OUTER JOIN reservation r ON m.materialid = r.materialid WHERE eventid = :eventid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", (int) ev.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Material AddReservation(Material material, int guestID, DateTime startDate, DateTime endDate)
        {
            var query =
                "INSERT INTO reservation (userid, materialid, datestart, dateend) VALUES (:userid, :materialid, :datestart, :dateend)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", guestID),
                new OracleParameter("materialid", material.ID),
                new OracleParameter("datestart", startDate) { OracleDbType = OracleDbType.Date }, 
                new OracleParameter("dateend", endDate) { OracleDbType = OracleDbType.Date }
            };

            var result = Database.ExecuteNonQuery(query, parameters);

            if (!result) return null;

            material.GuestID = guestID;
            material.StartDate = startDate;
            material.EndDate = endDate;

            return material;
        }

        public bool RemoveReservation(Material material)
        {
            var query = "DELETE FROM reservation WHERE materialid = :materialid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialid", material.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool UpdateReservation(Material material, DateTime startDate, DateTime endDate)
        {
            var query = "UPDATE reservation SET datestart = :datestart, dateend = :dateend WHERE materialid = :materialid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialid", material.ID),
                new OracleParameter("datestart", startDate) {OracleDbType = OracleDbType.Date},
                new OracleParameter("dateend", endDate) {OracleDbType = OracleDbType.Date}
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override Material GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            var material = new Material(Convert.ToInt32(record[0]), record[3], Convert.ToInt32(record[1]),
                Convert.ToInt32(record[2]));

            if (!string.IsNullOrWhiteSpace(record[4]) && record[4] != "0")
            {
                material.GuestID = Convert.ToInt32(record[4]);
                material.StartDate = DateTime.Parse(record[5]);
                material.EndDate = DateTime.Parse(record[6]);
            }

            return material;
        }
    }
}
