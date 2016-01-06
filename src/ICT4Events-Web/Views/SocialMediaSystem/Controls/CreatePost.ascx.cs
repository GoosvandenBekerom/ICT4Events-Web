using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem.Controls
{
    public partial class CreatePost : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            var user = ((SiteMaster)Page.Master)?.CurrentUser();
            if (user == null) return;

            LogicCollection.PostLogic.AddPost(
                new Message(0, user.ID, DateTime.Now, txtTitle.Text, txtMessage.Text)
            );
            
            Page.Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}