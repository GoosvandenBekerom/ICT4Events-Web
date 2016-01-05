using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class LocationOracleContext : EntityOracleContext<Location>, ILocationContext
    {
        public List<Location> GetAll()
        {
            var query = "p_locatie.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Location GetById(int id)
        {
            var query = "p_locatie.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("locatieId", Convert.ToInt32(id))
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(Location entity)
        {
            var query =
                "INSERT INTO location (locationid, eventid, name, capacity, price, x, y) VALUES (seq_location.nextval, :eventid, :name, :capacity, :price, :x, :y) RETURNING locationid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                /*
                new OracleParameter("eventid", entity.EventID),
                new OracleParameter("name", entity.Name),
                new OracleParameter("capacity", entity.Capacity),
                new OracleParameter("price", entity.Price),
                new OracleParameter("x", entity.Coordinates.X),
                new OracleParameter("y", entity.Coordinates.Y),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
                */
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(Location entity)
        {
            const string query = "UPDATE location SET name = :name, capacity = :capacity, price = :price, x = :x, y = :y WHERE locationid = :locationid";

            var parameters = new List<OracleParameter>
            {
                //new OracleParameter("locationid", entity.ID),
                //new OracleParameter("name", entity.Name),
                //new OracleParameter("capacity", entity.Capacity),
                //new OracleParameter("price", entity.Price),
                //new OracleParameter("x", entity.Coordinates.X),
                //new OracleParameter("y", entity.Coordinates.Y),
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Location entity)
        {
            var query = "DELETE FROM location WHERE locationid = :locationid";
            var parameters = new List<OracleParameter> { new OracleParameter("locationid", entity.ID) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Location> GetAllByEvent(Event ev)
        {
            var query = "SELECT * FROM location WHERE eventid = :eventid ORDER BY locationid";
            var parameters = new List<OracleParameter> { new OracleParameter("eventid", ev.ID) };
            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetEntityFromRecord).ToList();
        }

        protected override Location GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            // ID locatie_id nummer capaciteit comfortplek handicap afmeting kraan x y prijs
            // 0  1          2      3          4           5        6        7     8 9 10

            return new Location(Convert.ToInt32(record[0]), record[1], record[2], record[3], record[4], record[5]);
        }
    }
}