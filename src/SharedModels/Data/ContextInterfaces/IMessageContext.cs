﻿using System.Collections.Generic;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IMessageContext : IRepositoryContext<Message>
    {
        bool LikeMessage(User user, Message message);
        bool ReportMessage(User user, Message message);

        List<Message> GetRepliesByPost(Message message);
        int AddReply(User user, Message message, string content);

        List<int> GetLikesByMessage(Message message);
        List<int> GetReportsByMessage(Message message);

        List<Message> SearchMessages(string hashtag);
        List<Message> GetMediaPostsByUser(User user);
        List<Message> GetAllReportedPosts();
        bool RemoveReports(int postId);

        Message InsertReturnMessage(Message entity);
        bool AddFileContribution(FileContribution fileContribution, int postID);
        FileContribution GetFile(int postId);
    }
}
