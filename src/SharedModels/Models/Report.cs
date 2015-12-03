using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Report
    {
        public int GuestID { get; set; }
        public int PostID { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; } // Is a status type enum actually necessary?

        public Report(int guestID, int postID, string reason, DateTime date, bool status = false)
        {
            GuestID = guestID;
            PostID = postID;
            Reason = reason;
            Date = date;
            Status = status;
        }
    }
}
