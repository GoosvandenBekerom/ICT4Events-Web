using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Data.OracleContexts
{
    using System.Data;

    using Oracle.DataAccess.Client;

    using SharedModels.Data.ContextInterfaces;
    using SharedModels.Models;

    public class PostOracleContext : EntityOracleContext<Message>, IMessageContext
    {
        public List<Message> GetAll()
        {
            var query = "p_post.GetAllMainPost";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Message GetById(int id)
        {
            var query = "p_post.GetPostByID";
            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("p_postID", id)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(Message entity)
        {
            var query = "p_post.Post";
            var parameters = new List<OracleParameter>
                                 {
                                     new OracleParameter("p_accountID", entity.UserID),
                                     new OracleParameter("p_title", entity.Title),
                                     new OracleParameter("p_content", entity.Content)
                                 };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(Message entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Message entity)
        {
            throw new NotImplementedException();
        }

        public bool LikeMessage(User user, Message message)
        {
            throw new NotImplementedException();
        }

        public bool ReportMessage(User user, Message message)
        {
            throw new NotImplementedException();
        }

        public List<int> GetLikesByMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public List<int> GetReportsByMessage(Message message)
        {
            throw new NotImplementedException();
        }

        protected override Message GetEntityFromRecord(List<string> record)
        {
            return new Message(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), DateTime.Parse(record[2]), record[5], record[6]);
        }

    }
}
