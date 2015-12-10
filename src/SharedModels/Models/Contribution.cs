using System;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Contribution
    {
        public int ID { get; }
        public int UserID { get; }
        public DateTime Date { get; set; }
        public ContributionType Type { get; set; }
        public bool Like { get; set; }
        public bool Report { get; set; }

        public Contribution(int id, int userId, DateTime date, ContributionType type = ContributionType.None, bool like = false, bool report = false)
        {
            ID = id;
            UserID = userId;
            Date = date;
            Type = type;
            Like = like;
            Report = report;
        }
    }
}
