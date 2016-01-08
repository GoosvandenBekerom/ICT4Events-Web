using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Debug;
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
            var user = SiteMaster.CurrentUser();
            if (user == null) return;

            var newMessage = LogicCollection.PostLogic.AddPost(
                new Message(0, user.ID, DateTime.Now, txtTitle.Text, txtMessage.Text)
            );
            
            try
            {
                if (FileUpload.HasFile)
                {
                    var file = FileUpload.PostedFile;
                    var trailingPath = Path.GetFileName(file.FileName);
                    var fullPath = Path.Combine(HostingEnvironment.MapPath("/Files/"), trailingPath);

                    FileUpload.SaveAs(fullPath);
                    
                    LogicCollection.PostLogic.AddFileContribution(
                        new FileContribution(
                            0, user.ID, DateTime.Now, 1, 
                            "/Files/" + file.FileName, 
                            FileUpload.PostedFile.ContentLength
                        ),
                        newMessage.ID
                    );
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }

            Page.Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}