using System;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Guest : User
    {
        public string PassID { get; set; }
        public bool Paid { get; set; }
        public int EventID { get; }
        public bool Present { get; set; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int LocationID { get; set; }
        public int LeaderID { get; set; }

        public Guest(int id, string username, string email, string hash, bool activated, string passId, bool paid, int eventId, bool present, DateTime startDate, DateTime endDate, int locationId, int leaderId) : base(id, username, email, hash, activated)
        {
            PassID = passId;
            Paid = paid;
            EventID = eventId;
            Present = present;
            StartDate = startDate;
            EndDate = endDate;
            LocationID = locationId;
            LeaderID = leaderId;
        }
    }
}
