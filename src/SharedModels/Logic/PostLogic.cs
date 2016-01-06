using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class PostLogic
    {
        private readonly IMessageContext _context;

        public PostLogic()
        {
            _context = new PostOracleContext();
        }

        public PostLogic(IMessageContext context)
        {
            _context = context;
        }

        public List<Message> GetAllMainPosts() => _context.GetAll();
        public List<Message> GetRepliesByPost(Message message) => _context.GetRepliesByPost(message);

        public List<Message> SearchPostsByHashtag(string hashtag) => _context.SearchMessages(hashtag);
    }
}
