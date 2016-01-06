using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IMessageContext : IRepositoryContext<Message>
    {
        bool LikeMessage(User user, Message message);
        bool ReportMessage(User user, Message message);

        List<Message> GetRepliesByPost(Message message);

        List<int> GetLikesByMessage(Message message);
        List<int> GetReportsByMessage(Message message);

        List<Message> SearchMessages(string hashtag);
    }
}
