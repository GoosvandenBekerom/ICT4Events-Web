using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Reply : Post
    {
        public int MainPostID { get; }

        public Reply(int id, int guestId, int eventId, int mediaId, int mainPostId, DateTime date, bool visible, string content) : base(id, guestId, eventId, mediaId, date, visible, content)
        {
            MainPostID = mainPostId;
        }
    }
}
