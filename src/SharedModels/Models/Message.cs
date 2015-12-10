using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Message : Contribution
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Message(int id, int userId, DateTime date, string title, string content)
            : base(id, userId, date, ContributionType.Message, false, false)
        {
            Title = title;
            Content = content;
        }
    }
}
