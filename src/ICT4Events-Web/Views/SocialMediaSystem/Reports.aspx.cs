using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events_Web.Views.SocialMediaSystem.Controls;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.SocialMediaSystem
{
    public partial class Reports : System.Web.UI.Page
    {
        private List<Message> _messages = new List<Message>();

        protected void Page_Init(object sender, EventArgs e)
        {
            _messages = LogicCollection.PostLogic.GetAllReportedPosts();

            lbReportedPosts.DataSource = _messages;
            lbReportedPosts.DataValueField = "ID";
            lbReportedPosts.DataTextField = "Content";
            lbReportedPosts.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lbReportedPosts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected post id
            var messageId = Convert.ToInt32(lbReportedPosts.SelectedValue);

            //clear previous selected post
            phPost.Controls.Clear();

            //add selected post as postControl
            var control = (PostControl)LoadControl("Controls/PostControl.ascx");
            var message = LogicCollection.PostLogic.GetPostById(messageId);
            control.Post = message;
            phPost.Controls.Add(control);
        }
    }
}