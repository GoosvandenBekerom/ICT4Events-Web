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
            postReplyValidator.ValidationGroup = "postReplyValidator" + Post.ID;
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            Username.InnerText = LogicCollection.UserLogic.GetById(Post.UserID).Username;

            delete.Attributes.Add("value", Post.ID.ToString());
            like.Attributes.Add("value", Post.ID.ToString());
            report.Attributes.Add("value", Post.ID.ToString());

            // Only the owners of the main posts an admins are allowed to remove posts
            var user = SiteMaster.CurrentUser();
            delete.Visible = (Post.UserID == user.ID || user.Admin);

            var likes = LogicCollection.PostLogic.GetLikesByPost(Post);

            if (IsPostBack)
            {
                like.Visible = false;
                report.Visible = false;
            }

            // Adds the amount of likes to the posts, if any
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

            // Changes the button depending on whether or not the current user has reported it
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

            // Regular expression that extracts every hashtag
            var regex = new Regex(@"#(?<content>[^/\s]+)", RegexOptions.IgnoreCase);
            var list = regex.Matches(Post.Content).Cast<Match>().Select(m => m.Value).ToList();

            // Wrapping every hashtag with a hyperlink tag to search for it
            foreach (var match in list)
            {
                Post.Content = Post.Content.Replace(match, $@"<a href='/Timeline?q={match.Replace("#", "")}'>{match}</a>");
            }

            if (Post.File == null) return;

            File = Post.File;
            postThumbnail.ImageUrl = File.Filepath;
        }
    }
}