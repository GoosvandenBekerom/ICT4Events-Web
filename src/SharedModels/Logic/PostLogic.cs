﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
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
        public Message GetPostById(int id) => _context.GetById(id);
        public List<Message> GetRepliesByPost(Message message) => _context.GetRepliesByPost(message);

        public List<Message> SearchPostsByHashtag(string hashtag) => _context.SearchMessages(hashtag);
        public List<Message> GetMediaPostsByUser(User user) => _context.GetMediaPostsByUser(user);
        public List<Message> GetAllReportedPosts() => _context.GetAllReportedPosts();
        public bool RemoveReports(int messageId) => _context.RemoveReports(messageId);

        public Message AddPost(Message message) => _context.InsertReturnMessage(message);
        public bool DeletePost(Message post) => _context.Delete(post);

        public List<int> GetLikesByPost(Message message) => _context.GetLikesByMessage(message);
        public List<int> GetReportsByPost(Message message) => _context.GetReportsByMessage(message);

        public int AddReply(User user, int postId, string content)
        {
            var message = _context.GetById(postId);
            return message != null ? _context.AddReply(user, message, content) : 0;
        }

        public bool LikePost(User user, int postId)
        {
            var post = _context.GetById(postId);
            return post != null && _context.LikeMessage(user, post);
        }

        public bool ReportPost(User user, int postId)
        {
            var post = _context.GetById(postId);
            return post != null && _context.ReportMessage(user, post);
        }

        public bool AddFileContribution(FileContribution fileContribution, int postID)
        {
            return _context.AddFileContribution(fileContribution, postID);
        }

        public FileContribution GetFile(int postId)
        {
            return _context.GetFile(postId);
        }

        public static bool IsImage(string path)
        {
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            return Path.GetExtension(path)?.ToLower() == ".jpg" || Path.GetExtension(path)?.ToLower() == ".png" ||
                   Path.GetExtension(path)?.ToLower() == ".gif" || Path.GetExtension(path)?.ToLower() == ".jpeg";
        }
    }
}
