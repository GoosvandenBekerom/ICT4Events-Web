using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem.Controls
{
    public partial class PostControl : System.Web.UI.UserControl
    {
        public Message Post { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PreRender += OnPreRender;
            //
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            Username.InnerText = Post.UserID.ToString();
            Content.InnerText = Post.Content;
            //Username.Text = Post.UserID.ToString();
            //Content.Text = Post.Content;
        }
    }
}