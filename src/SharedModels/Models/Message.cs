using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using SharedModels.Enums;
using SharedModels.Logic;

namespace SharedModels.Models
{
    public class Message : Contribution
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags => Content.Split(' ').Where(word => word.StartsWith("#")).ToList();

        public FileContribution File => LogicCollection.PostLogic.GetFile(this.ID);

        public Message(int id, int userId, DateTime date, string title, string content)
            : base(id, userId, date, ContributionType.Message, false, false)
        {
            Title = title;
            Content = content;
        }
    }
}
