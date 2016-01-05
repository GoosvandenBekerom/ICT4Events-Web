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
    using SharedModels.Data.OracleContexts;

    public partial class Timeline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This is temporary
            var postCon = new PostOracleContext();

            var messages = postCon.GetAll();

            // adding some extra dummy posts
            for (var i = 0; i < 10; i++)
            {
                var message = new Message(i, 1, DateTime.Now, "TestTitle", "TestContent aaaaaaaa");
                messages.Add(message);
            }

            
            foreach (var message in messages.OrderByDescending(x => x.Date))
            {
                var control = (PostControl)LoadControl("Controls/PostControl.ascx");
                control.Post = message;

                Posts.Controls.Add(control);
            }
        }
    }
}