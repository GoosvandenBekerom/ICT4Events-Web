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
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (var message in _messages.OrderByDescending(x => x.Date))
            {
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
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "Not authorized";
            }
            var user = SiteMaster.CurrentUser();
            var result = LogicCollection.PostLogic.AddReply(user, postId, message);

            if (result == 0) return "false";
            return $"<div class=\"reply post well well-sm\"><div class=\"PostHeader\"><span class=\"Username\">{user.Username}</span><span class=\"PostDate\"> {DateTime.Now.ToShortDateString()}</span></div><div class=\"PostContent\"><p>{message}</p></div><div class=\"PostFooter\"><button type=\"button\" class=\"btn btn-sm btn-default reportButton\" value=\"{result}\"><span class=\"glyphicon glyphicon-ban-circle\" aria-hidden=\"true\"></span></button><button type=\"button\" class=\"btn btn-sm btn-default likeButton\" value=\"{result}\"><span class=\"glyphicon glyphicon-thumbs-up\" aria-hidden=\"true\"></span></button></div></div>";
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