using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events_Web.Views.SocialMediaSystem.Controls;
using Microsoft.Ajax.Utilities;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem
{
    public partial class Reports : System.Web.UI.Page
    {
        //private Message _selectedMessage;
        protected int _messageId;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated && !SiteMaster.CurrentUser().Admin)
            {
                Response.Redirect("~", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var messages = LogicCollection.PostLogic.GetAllReportedPosts();

            // Setting the data source for the listbox
            lbReportedPosts.DataSource = messages;
            lbReportedPosts.DataValueField = "ID";
            lbReportedPosts.DataTextField = "Content";
            lbReportedPosts.DataBind();

            if (string.IsNullOrWhiteSpace(lbReportedPosts.SelectedValue)) return;

            int id;

            if (int.TryParse(lbReportedPosts.SelectedValue, out id))
            {
                lbReportedPosts.SelectedValue = id.ToString();
            }
        }

        protected void lbReportedPosts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lbReportedPosts.SelectedValue)) return;
            
            // Get selected post id
            _messageId = Convert.ToInt32(lbReportedPosts.SelectedValue);

            // Clear previous selected post
            phPost.Controls.Clear();

            // Add selected post as postControl
            var control = (PostControl)LoadControl("Controls/PostControl.ascx");
            var message = LogicCollection.PostLogic.GetPostById(_messageId);
            control.Post = message;
            phPost.Controls.Add(control);
        }

        [WebMethod(true)]
        public static string RemoveReports(int postId)
        {
            var context = HttpContext.Current;
            if (context == null || !context.User.Identity.IsAuthenticated)
            {
                return "Not authorized";
            }

            var result = LogicCollection.PostLogic.RemoveReports(postId);

            return HttpContext.Current != null && result
                ? "succeeded"
                : "Not authorized";
        }
    }
}