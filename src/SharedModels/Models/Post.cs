using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedModels.Models
{
    public class Post
    {
        public int ID { get; }
        public int GuestID { get; }
        public int EventID { get; }
        public int MediaID { get; set; }
        public List<int> Likes { get; set; } 
        public DateTime Date { get; set; }
        public bool Visible { get; set; }
        public string Content { get; set; }
        public List<string> Tags => Content.Split(' ').Where(word => word.StartsWith("#")).ToList();

        public Post(int id, int guestId, int eventId, int mediaId, DateTime date, bool visible, string content)
        {
            ID = id;
            GuestID = guestId;
            EventID = eventId;
            MediaID = mediaId;
            Date = date;
            Visible = visible;
            Content = content;
        }

        public override string ToString()
        {
            return $"PostID: {ID} - Visible: {Visible}";
        }
    }

    public class PostComparer : IEqualityComparer<Post>
    {
        public bool Equals(Post x, Post y)
        {
            return x.Visible == y.Visible &&
                   x.MediaID == y.MediaID &&
                   x.Content == y.Content;
        }

        public int GetHashCode(Post obj)
        {
            return obj.GetHashCode();
        }
    }
}
