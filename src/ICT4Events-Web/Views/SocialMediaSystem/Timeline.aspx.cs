using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events_Web.Views.SocialMediaSystem.Controls;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem
{
    public partial class Timeline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var message = new Message(i, i, DateTime.Now, "TestTitle", "TestContent aaaaaaaa");
                var control = (PostControl) LoadControl("Controls/PostControl.ascx");
                control.Post = message;

                Posts.Controls.Add(control);
            }
        }
    }
}