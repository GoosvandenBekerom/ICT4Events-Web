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

        public Guest(int id, string username, string password, string name, string passId, bool paid, int eventID,
            bool present, DateTime startDate, DateTime endDate, int locationID, DateTime regDate = new DateTime(),
            PermissionType permission = PermissionType.User, string surname = "", Country country = Country.Nederland,
            string city = "",
            string postal = "", string address = "", string telephone = "", int leaderID = 0)
            : base(id, username, password, name, surname, country, city, postal, address, telephone, regDate, permission)
        {
            PassID = passId;
            Paid = paid;
            EventID = eventID;
            Present = present;
            StartDate = startDate;
            EndDate = endDate;
            LocationID = locationID;
            LeaderID = leaderID == 0 ? id : leaderID;
        }

        
    }
}
