using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem.Controls
{
    using SharedModels.Logic;

    public partial class PostControl : UserControl
    {
        public Message Post { get; set; }
        public FileContribution File { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PreRender += OnPreRender;
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            Username.InnerText = LogicCollection.UserLogic.GetById(Post.UserID).Username;

            delete.Attributes.Add("value", Post.ID.ToString());
            like.Attributes.Add("value", Post.ID.ToString());
            report.Attributes.Add("value", Post.ID.ToString());

            var user = SiteMaster.CurrentUser();
            delete.Visible = (Post.UserID == user.ID || user.Admin);

            var likes = LogicCollection.PostLogic.GetLikesByPost(Post);

            if (IsPostBack)
            {
                like.Visible = false;
                report.Visible = false;
            }

            if (likes.Any())
            {
                like.InnerHtml += "<span> " + likes.Count + "</span>";
            }
            else
            {
                like.InnerHtml += "<span></span>";
            }

            if(likes.Any(x => x == user.ID))
            {
                like.Attributes.Add("class", like.Attributes["class"] + " liked");
            }

            var reports = LogicCollection.PostLogic.GetReportsByPost(Post);

            if (reports.Any(x => x == user.ID))
            {
                report.Attributes.Add("class", like.Attributes["class"] + " reported");
                report.InnerHtml += "<span>Gerapporteerd</span>";
            }
            else
            {
                report.InnerHtml += "<span>Rapporteren</span>";
            }
            
            if (Post.File == null) return;

            File = Post.File;
            postThumbnail.ImageUrl = File.Filepath;

            var regex = new Regex(@"#(?<content>[^/\s]+)", RegexOptions.IgnoreCase);
            var list = regex.Matches(Post.Content).Cast<Match>().Select(m => m.Value).ToList();

            foreach (var match in list)
            {
                Post.Content = Post.Content.Replace(match, @"<a href='/Timeline?q="+ match.Replace("#", "") +"'>"+ match +"</a>");
            }
        }
    }
}