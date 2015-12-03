using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Enums;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class MaterialTypeOracleContext : EntityOracleContext<MaterialType>, IMaterialTypeContext
    {
        public List<MaterialType> GetAll()
        {
            var query = "SELECT * FROM materialtype ORDER BY materialtypeid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public MaterialType GetById(object id)
        {
            var query = "SELECT * FROM materialtype WHERE materialtypeid = :materialtypeid ORDER BY materialtypeid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("materialtypeid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public MaterialType GetByName(string name)
        {
            var query = "SELECT * FROM materialtype WHERE name = :name ORDER BY materialtypeid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("name", name)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public MaterialType Insert(MaterialType entity)
        {
            var query =
                "INSERT INTO materialtype (materialtypeid, name) VALUES (seq_materialtype.nextval, :name) RETURNING materialtypeid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("name", entity.Name),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
        }

        public bool Update(MaterialType entity)
        {
            var query = "UPDATE materialtype SET name = :name WHERE materialtypeid = :materialtypeid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("name", entity.Name),
                new OracleParameter("materialtypeid", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(MaterialType entity)
        {
            var query = "DELETE FROM materialtype WHERE materialtypeid = :materialtypeid";
            var parameters = new List<OracleParameter> { new OracleParameter("materialtypeid", entity.ID) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override MaterialType GetEntityFromRecord(List<string> record)
        {
            return record == null ? null : new MaterialType(Convert.ToInt32(record[0]), record[1]);
        }
    }
}
