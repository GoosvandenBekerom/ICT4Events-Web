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
    public class MediaOracleContext : EntityOracleContext<Media>, IMediaContext
    {
        public List<Media> GetAll()
        {
            var query = "SELECT * FROM media ORDER BY mediaid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public Media GetById(object id)
        {
            var query = "SELECT * FROM media WHERE mediaid = :mediaid ORDER BY mediaid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("mediaid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public Media Insert(Media entity)
        {
            var query =
                "INSERT INTO media (mediaid, userid, eventid, mediatypeid, name, fileextension, filepath) VALUES (seq_media.nextval, :userid, :eventid, :mediatypeid, :name, :fileextension, :filepath) RETURNING mediaid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", entity.UserID),
                new OracleParameter("eventid", entity.EventID),
                new OracleParameter("mediatypeid", Convert.ToInt32(entity.Type)),
                new OracleParameter("name", entity.Name),
                new OracleParameter("fileextension", entity.FileExtension),
                new OracleParameter("filepath", entity.Path),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
        }

        public bool Update(Media entity)
        {
            var query = "UPDATE media SET name = :name, fileextension = :fileextension, filepath = :filepath WHERE mediaid = :mediaid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("name", entity.Name),
                new OracleParameter("fileextension", entity.FileExtension),
                new OracleParameter("filepath", entity.Path),
                new OracleParameter("mediaid", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Media entity)
        {
            var query = "DELETE FROM media WHERE mediaid = :mediaid";
            var parameters = new List<OracleParameter> { new OracleParameter("mediaid", entity.ID) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Media> GetAllById(int guestId, Event ev)
        {
            var query = "SELECT * FROM media WHERE eventid = :eventid AND userid = :userid ORDER BY mediaid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", ev.ID),
                new OracleParameter("userid", guestId)
            };

            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<Media> GetAllMedia(Event ev)
        {
            var query = "SELECT * FROM media WHERE eventid = :eventid ORDER BY mediaid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", ev.ID),
            };

            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetEntityFromRecord).ToList();
        }

        protected override Media GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            return new Media(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), Convert.ToInt32(record[2]),
                (MediaType) Convert.ToInt32(record[3]), record[4], record[5], record[6]);
        }
    }
}
