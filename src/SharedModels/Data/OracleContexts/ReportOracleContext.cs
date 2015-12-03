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
    public class ReportOracleContext : EntityOracleContext<Report>, IReportContext
    {
        public List<Report> GetAll()
        {
            var query = "SELECT * FROM report ORDER BY postid, userid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        // TODO: Do reports need IDs?
        public Report GetById(object id)
        {
            throw new InvalidOperationException("Reports have no unique IDs");
        }

        public Report Insert(Report entity)
        {
            var query =
                "INSERT INTO report (userid, postid, reportdate, reason) VALUES (:userid, :postid, :reportdate, :reason)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", entity.GuestID),
                new OracleParameter("postid", entity.PostID),
                new OracleParameter("reportdate", entity.Date) {OracleDbType = OracleDbType.Date},
                new OracleParameter("reason", entity.Reason)
            };

            if (!Database.ExecuteNonQuery(query, parameters)) return null;
            entity.Status = false;

            return entity;
        }

        public bool Update(Report entity)
        {
            var query =
                "UPDATE report SET reason = :reason, status = :status WHERE userid = :userid AND postid = :postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("reason", entity.Reason),
                new OracleParameter("status", Convert.ToInt32(entity.Status)),
                new OracleParameter("userid", entity.GuestID),
                new OracleParameter("postid", entity.PostID),
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Report entity)
        {
            var query =
                "DELETE FROM report WHERE userid = :userid AND postid = :postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", entity.GuestID),
                new OracleParameter("postid", entity.PostID),
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Report> GetAllByPost(Post post)
        {
            var query = "SELECT * FROM report WHERE postid = :postid ORDER BY postid, userid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID),
            };

            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetEntityFromRecord).ToList();
        }

        protected override Report GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            return new Report(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), record[3],
                DateTime.Parse(record[2]), Convert.ToBoolean(Convert.ToInt32(record[4])));
        }
    }
}
