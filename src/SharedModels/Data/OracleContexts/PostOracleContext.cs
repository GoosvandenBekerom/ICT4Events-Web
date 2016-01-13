using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters)?.FirstOrDefault());
        }

        public bool Insert(Message entity)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMediaPostsByUser(User user)
        {
            var query = "p_post.GetMediaPostsByUser";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                new OracleParameter("p_accountId", user.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<Message> GetAllReportedPosts()
        {
            var query = "p_post.GetAllReportedPosts";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Message InsertReturnMessage(Message entity)
        {
            var query = "p_post.AddPost";
            var parameters = new List<OracleParameter>
                                 {
                new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                                     new OracleParameter("p_accountID", entity.UserID),
                                     new OracleParameter("p_title", entity.Title),
                                     new OracleParameter("p_content", entity.Content)
                                 };

            string newID;
            Database.ExecuteNonQuery(query, out newID, parameters);
            return GetById(Convert.ToInt32(newID));
        }

        public bool Update(Message entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Message entity)
        {
            var query = "p_post.RemovePost";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_postId", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public int AddReply(User user, Message message, string content)
        {
            var query = "p_post.AddReply";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_accountId", user.ID),
                new OracleParameter("p_postId", message.ID),
                new OracleParameter("p_content", content),
                new OracleParameter("o_newId", OracleDbType.Int32, ParameterDirection.ReturnValue)
            };

            string newId;
            Database.ExecuteNonQuery(query, out newId, parameters);
            return string.IsNullOrEmpty(newId) ? 0 : Convert.ToInt32(newId);
        }

        public bool LikeMessage(User user, Message message)
        {
            var query = "p_post.ToggleLike";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_accountId", user.ID),
                new OracleParameter("p_bijdrageId", message.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool ReportMessage(User user, Message message)
        {
            var query = "p_post.AddReport";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_accountId", user.ID),
                new OracleParameter("p_bijdrageId", message.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Message> GetRepliesByPost(Message message)
        {
            var query = "p_post.GetRepliesByPost";
            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("p_postId", message.ID)
                };

            var res = Database.ExecuteReader(query, parameters);
            return res?.Select(GetEntityFromRecord).ToList();
        }

        public List<int> GetLikesByMessage(Message message)
        {
            var query = "p_post.GetLikesByPost";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_postId", message.ID),
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res?.Select(x => Convert.ToInt32(x[0])).ToList();
        }

        public List<int> GetReportsByMessage(Message message)
        {
            var query = "p_post.GetReportsByPost";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("p_postId", message.ID),
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res?.Select(x => Convert.ToInt32(x[0])).ToList();
        }

        public List<Message> SearchMessages(string hashtag)
        {
            var query = "p_post.GetPostsByHashtag";
            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("p_hashtag", hashtag)
                };

            var res = Database.ExecuteReader(query, parameters);
            return res?.Select(GetEntityFromRecord).ToList();
        }

        public bool AddFileContribution(FileContribution fileContribution, int postID)
        {
            var query = "p_post.AddFile";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                new OracleParameter("p_accountId", fileContribution.UserID),
                new OracleParameter("p_bijdrageId", postID),
                new OracleParameter("p_path", fileContribution.Filepath),
                new OracleParameter("p_size", fileContribution.Filesize)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public FileContribution GetFile(int postId)
        {
            var query = "p_post.GetFile";
            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("p_postId", postId)
                };

            var res = Database.ExecuteReader(query, parameters);
            return res?.Select(GetFileContributionFromRecord).FirstOrDefault();
        }

        protected override Message GetEntityFromRecord(List<string> record)
        {
            return record == null
                ? null
                : new Message(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), DateTime.Parse(record[2]),
                    record[5], record[6]);
        }

        private FileContribution GetFileContributionFromRecord(List<string> record)
        {
            //0     1           2       3         4             5             6                 7
            //id    account_id  datum   soort     bijdrage_id   categorie_id  bestandslocatie   grootte
            return new FileContribution(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]),
                DateTime.Parse(record[2]), Convert.ToInt32(record[5]), record[6], Convert.ToInt64(record[7]));
        }
    }
}
