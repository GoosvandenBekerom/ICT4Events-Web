using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events_Web.Views.SocialMediaSystem.Controls;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem
{
    using SharedModels.Data.OracleContexts;

    public partial class Timeline : System.Web.UI.Page
    {
        private List<Message> _messages = new List<Message>();

        protected void Page_Init(object sender, EventArgs e)
        {
            if ((RouteData.Values["Catalog"]?.ToString() ?? string.Empty) == "Catalog")
            {
                var user = (SiteMaster.CurrentUser());
                if (user == null) return;
                _messages = LogicCollection.PostLogic.GetMediaPostsByUser(user);
                SearchBox.Visible = false;
                SearchButton.Visible = false;
                CreatePost.Visible = false;
                Title = "Catalogus";

                warning.Visible = !_messages.Any();
            }
            else
            {
                var searchQuery = Request.QueryString["q"];

                if (searchQuery == null)
                {
                    _messages = LogicCollection.PostLogic.GetAllMainPosts();
                }
                else
                {
                    _messages = LogicCollection.PostLogic.SearchPostsByHashtag(searchQuery);
                    SearchBox.Text = searchQuery;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (var message in _messages.OrderByDescending(x => x.Date))
            {
                if (LogicCollection.PostLogic.GetReportsByPost(message).Count >= 5) continue; // invisble post after 5 reports
                var control = (PostControl) LoadControl("Controls/PostControl.ascx");
                control.Post = message;
                Posts.Controls.Add(control);
            }
        }

        [WebMethod(enableSession: true)]
        public static string LikePost(int postId)
        {
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "Not authorized";
            }
            var user = SiteMaster.CurrentUser();
            var result = LogicCollection.PostLogic.LikePost(user, postId);

            return HttpContext.Current != null && result
                ? "succeeded"
                : "Not authorized";
        }

        [WebMethod(enableSession: true)]
        public static string ReportPost(int postId)
        {
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "Not authorized";
            }
            var user = SiteMaster.CurrentUser();
            var result = LogicCollection.PostLogic.ReportPost(user, postId);

            return HttpContext.Current != null && result
                ? "succeeded"
                : "Not authorized";
        }

        [WebMethod(true)]
        public static string AddReply(int postId, string message)
        {

            if(string.IsNullOrWhiteSpace(message))
            {
                return "false";
            }

            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "Not authorized";
            }
            var user = SiteMaster.CurrentUser();
            var result = LogicCollection.PostLogic.AddReply(user, postId, message);

            if (result == 0) return "false";
            return DisplayReply(user, new Message(result, user.ID, DateTime.Now, "", message));
        }

        [WebMethod(true)]
        public static string LoadReplies(int postId)
        {
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "false";
            }

            var replies = LogicCollection.PostLogic.GetRepliesByPost(new Message(postId, 0, DateTime.Today, "", ""));
            if ((replies == null || !replies.Any())) return "false";
            var result = "";
            foreach (var reply in replies)
            {
                var user = LogicCollection.UserLogic.GetById(reply.UserID);
                result += DisplayReply(user, reply);
            }

            return result;
        }

        [WebMethod(true)]
        public static string DeletePost(int postId)
        {
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "false";
            }

            var user = SiteMaster.CurrentUser();
            var post = LogicCollection.PostLogic.GetPostById(postId);

            if (user != null && post != null && (post.UserID == user.ID || user.Admin))
            {
                return LogicCollection.PostLogic.DeletePost(post)
                    ? "true"
                    : "false";
            }

            return "false";
        }

        public static string DisplayReply(User user, Message message)
        {
            var likes = LogicCollection.PostLogic.GetLikesByPost(message);
            var reports = LogicCollection.PostLogic.GetReportsByPost(message);

            if (likes == null || reports == null){return "false";}

            return
                $@"<div class=""reply post well well-sm"">
                        <div class=""PostHeader"">
                            <span class=""Username"">{user.Username}</span>
                            <span class=""PostDate""> {message.Date.ToShortDateString()}</span>
                        </div>
                        <div class=""PostContent"">
                            <p>{message.Content}</p>
                        </div>
                        <div class=""PostFooter"">
                            <button type=""button"" class=""btn btn-sm btn-default reportButton {(reports.Any() && reports.Contains(user.ID) ? "reported" : "")}"" value=""{message.ID}"">
                                <span class=""glyphicon glyphicon-ban-circle"" aria-hidden=""true""></span>
                            </button>
                            <button type=""button"" class=""btn btn-sm btn-default likeButton {(likes.Any() && likes.Contains(user.ID) ? "liked" : "")}"" value=""{message.ID}"">
                                <span class=""glyphicon glyphicon-thumbs-up"" aria-hidden=""true""></span>
                                <span>{(likes.Any() ? likes.Count.ToString() : "")}</span>
                            </button>
                        </div>
                    </div>";
        }

        protected void SearchButton_OnServerClick(object sender, EventArgs e)
        {
            var query = SearchBox.Text;
            if (!string.IsNullOrWhiteSpace(query))
            {
                Response.Redirect($"/Timeline?q={Server.UrlEncode(query)}");
            }
        }
    }
}
