using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class PostOracleContext : EntityOracleContext<Post>, IPostContext
    {
        public List<Post> GetAll()
        {
            var query = "SELECT * FROM post ORDER BY postid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).OrderByDescending(x => x.Date).ToList();
        }

        public Post GetByMediaId(Media media)
        {
            var query = "SELECT * FROM post WHERE mediaid = :mediaid ORDER BY postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("mediaid", media.ID)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).FirstOrDefault());
        }

        public Post GetById(object id)
        {
            var query = "SELECT * FROM post WHERE postid = :postid ORDER BY postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).FirstOrDefault());
        }

        public Post Insert(Post entity)
        {
            var query =
                "INSERT INTO post (postid, userid, eventid, mediaid, postdate, content) VALUES (seq_post.nextval, :userid, :eventid, :mediaid, :postdate, :content) RETURNING postid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", entity.GuestID),
                new OracleParameter("eventid", entity.EventID),
                new OracleParameter("postdate", entity.Date),
                new OracleParameter("content", entity.Content),
                entity.MediaID > 0
                    ? new OracleParameter("mediaid", (entity.MediaID))
                    : new OracleParameter("mediaid", (DBNull.Value)),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
        }

        public Reply Insert(Reply entity)
        {
            var query =
    "INSERT INTO post (postid, userid, eventid, mediaid, mainpostid, postdate, content) VALUES (seq_post.nextval, :userid, :eventid, :mediaid, :mainpostid, :postdate, :content) RETURNING postid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", entity.GuestID),
                new OracleParameter("eventid", entity.EventID),
                new OracleParameter("mainpostid", entity.MainPostID),
                new OracleParameter("postdate", entity.Date),
                new OracleParameter("content", entity.Content),
                entity.MediaID > 0
                    ? new OracleParameter("mediaid", (entity.MediaID))
                    : new OracleParameter("mediaid", (DBNull.Value)),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return (Reply) GetById(Convert.ToInt32(newID));
        }

        public bool Update(Post entity)
        {
            var query = "UPDATE post SET mediaid = :mediaid, postdate = :postdate, visible = :visible, content = :content WHERE postid = :postid";
            var parameters = new List<OracleParameter>
            {
                entity.MediaID > 0
                    ? new OracleParameter("mediaid", (entity.MediaID))
                    : new OracleParameter("mediaid", (DBNull.Value)),
                new OracleParameter("postdate", entity.Date) {OracleDbType = OracleDbType.Date},
                new OracleParameter("visible", Convert.ToInt32(entity.Visible)),
                new OracleParameter("content", entity.Content),
                new OracleParameter("postid", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Post entity)
        {
            var query = "DELETE FROM post WHERE postid = :postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", entity.ID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public List<Post> GetAllByEvent(Event ev)
        {
            var query = "SELECT * FROM post WHERE eventid = :eventid AND mainpostid IS NULL ORDER BY postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("eventid", ev.ID)
            };

            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetEntityFromRecord).OrderByDescending(x => x.Date).ToList();
        }

        public List<Reply> GetRepliesByPost(Post post)
        {
            var query = "SELECT * FROM post WHERE mainpostid = :postid ORDER BY postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID)
            };
            var res = Database.ExecuteReader(query, parameters);

            return res.Select(GetReplyEntityFromRecord).OrderBy(x => x.Date).ToList();
        }

        public List<int> GetAllLikes(Post post)
        {
            var query = "SELECT userid FROM likes WHERE postid = :postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID)
            };

            var res = Database.ExecuteReader(query, parameters);

            if (!res.Any()) return null;

            var result = res.Select(r => Convert.ToInt32(r[0])).ToList();
            return result;
        }

        public bool AddLikeToPost(Post post, Guest guest)
        {
            return AddLikeToPost(post, guest.ID);
        }

        public bool AddLikeToPost(Post post, int guestID)
        {
            var query = "INSERT INTO likes (postid, userid) VALUES (:postid, :userid)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID),
                new OracleParameter("userid", guestID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool RemoveLikeFromPost(Post post, int guestID)
        {
            var query = "DELETE FROM likes WHERE postid = :postid AND userid = :userid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID),
                new OracleParameter("userid", guestID)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool RemoveLikeFromPost(Post post, Guest guest)
        {
            return RemoveLikeFromPost(post, guest.ID);
        }

        public List<Post> GetPostsByTag(string tag)
        {
            var query = "SELECT p.* FROM post p INNER JOIN posttags t ON p.postid = t.postid WHERE t.tagname = :tagname";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("tagname", tag)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).OrderByDescending(x => x.Date).ToList();
        }

        public List<string> GetTagsByPost(Post post)
        {
            var query =
                "SELECT t.tagname FROM posttags t INNER JOIN post ON t.postid = p.postid WHERE p.postid = :postid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Any() ? res.Select(t => t[0]).ToList() : null;
        }

        public bool AddTagToPost(Post post, string tag)
        {
            tag = tag.Trim('#');
            var query = "INSERT INTO posttags (postid, tagname) VALUES (:postid, :tagname)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID),
                new OracleParameter("tagname", tag)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool RemoveTagFromPost(Post post, string tag)
        {
            var query = "DELETE FROM posttags WHERE postid = :postid AND tagname = :tagname)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("postid", post.ID),
                new OracleParameter("tagname", tag)
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override Post GetEntityFromRecord(List<string> record)
        {
            //postid userid eventid mediaid mainpostid postdate visible content
            //0      1      2       3       4          5        6       7

            if (!string.IsNullOrEmpty(record[4]))
            {
                return GetReplyEntityFromRecord(record);
            }

            var result = new Post(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), Convert.ToInt32(record[2]),
                Convert.ToInt32(record[3]), DateTime.Parse(record[5]), Convert.ToBoolean(Convert.ToInt32(record[6])),
                record[7]);

            result.Likes = GetAllLikes(result);

            return result;
        }

        private Reply GetReplyEntityFromRecord(List<string> record)
        {
            if (record == null) return null;


            var result = new Reply(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), Convert.ToInt32(record[2]),
                    Convert.ToInt32(record[3]), Convert.ToInt32(record[4]), DateTime.Parse(record[5]),
                    Convert.ToBoolean(Convert.ToInt32(record[6])), record[7]);

            result.Likes = GetAllLikes(result);

            return result;
        }
    }
}
