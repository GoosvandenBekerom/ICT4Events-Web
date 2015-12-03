using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class EventOracleContext : EntityOracleContext<Event>, IEventContext
    {
        public List<Event> GetAll()
        {
            var query = "SELECT * FROM event ORDER BY eventid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public Event GetById(object id)
        {
            var query = "SELECT * FROM event WHERE eventid = :eventid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public Event Insert(Event ev)
        {
            var query =
                "INSERT INTO event (eventid, name, startdate, enddate, location, mapfilename, capacity) VALUES (seq_event.nextval, :name, :startdate, :enddate, :location, :mapfilename, :capacity) RETURNING eventid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("name", ev.Name),
                new OracleParameter("startdate", ev.StartDate) {OracleDbType = OracleDbType.Date},
                new OracleParameter("enddate", ev.EndDate) {OracleDbType = OracleDbType.Date},
                new OracleParameter("location", ev.Location),
                new OracleParameter("mapfilename", ev.MapPath),
                new OracleParameter("capacity", ev.MaxCapacity),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
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
                new OracleParameter("location", ev.Location),
                new OracleParameter("mapfilename", ev.MapPath),
                new OracleParameter("capacity", ev.MaxCapacity),
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

            return new Event(Convert.ToInt32(record[0]), record[1], DateTime.Parse(record[2]), DateTime.Parse(record[3]),
                record[4], record[5], Convert.ToInt32(record[6]));
        }
    }
}
