using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class EventOracleContext : EntityOracleContext<Event>, IEventContext
    {
        public List<Event> GetAll()
        {
            var query = "p_event.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Event GetById(int id)
        {
            var query = "p_event.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("eventId", Convert.ToInt32(id))
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(Event ev)
        {
            var query =
                "INSERT INTO event (eventid, name, startdate, enddate, location, mapfilename, capacity) VALUES (seq_event.nextval, :name, :startdate, :enddate, :location, :mapfilename, :capacity) RETURNING eventid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("locationID", ev.LocationID),
                new OracleParameter("name", ev.Name),
                new OracleParameter("startdate", ev.StartDate),
                new OracleParameter("enddate", ev.EndDate),
                new OracleParameter("capacity", ev.Capacity),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(Event ev)
        {
            const string query = "UPDATE event SET name = :name, startdate = :startdate, enddate = :enddate, location = :location, mapfilename = :mapfilename, capacity = :capacity WHERE eventid = :eventid";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", ev.ID),
                new OracleParameter("name", ev.Name),
                new OracleParameter("startdate", ev.StartDate),
                new OracleParameter("enddate", ev.EndDate),
                new OracleParameter("capacity", ev.Capacity)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Event ev)
        {
            var query = "DELETE FROM event WHERE eventid = :eventid";
            var parameters = new List<OracleParameter> { new OracleParameter("eventid", ev.ID) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<string> GetTagsByEvent(Event ev)
        {
            var query =
                "SELECT t.tagname FROM posttags t INNER JOIN post p ON p.postid = t.postid WHERE p.eventid = :eventid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", ev.ID)
            };

            var res = Database.ExecuteReader(query, parameters);

            return res.Any() ? res.Select(t => t[0]).ToList() : null;
        }

        protected override Event GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            return new Event(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), record[2],
                DateTime.Parse(record[3]), DateTime.Parse(record[4]), Convert.ToInt32(record[5]));
        }
    }
}
