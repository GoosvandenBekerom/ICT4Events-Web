using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem.Controls
{
    using SharedModels.Logic;

    public partial class PostControl : UserControl
    {
        public Message Post { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PreRender += OnPreRender;
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            Username.InnerText = LogicCollection.UserLogic.GetById(Post.UserID).Username;

            var likes = LogicCollection.PostLogic.GetLikesByPost(Post);
            likes.Add(45);

            if (likes.Any())
            {
                like.InnerHtml += " " + likes.Count;
            }

            if(likes.Any(x => x == ((SiteMaster)Page.Master).CurrentUser().ID))
            {
                like.Attributes.Add("class", like.Attributes["class"] + " liked");
            }
        }
    }
}