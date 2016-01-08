using System;
using System.Web.UI.WebControls;
using SharedModels.Logic;

namespace ICT4Events_Web.Views.AccountSystem
{
    public partial class Rights : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            LoadUsers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteMaster.CurrentUser().Admin)
            {
                // If not admin NOT AUTHORIZED
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            var selectedUser = LogicCollection.UserLogic.GetById(Convert.ToInt32(drpUsers.SelectedValue));
            if (selectedUser.Admin)
            {
                selectedUser.Admin = false;
                if (!LogicCollection.UserLogic.UpdateUser(selectedUser)) return;
                LoadUsers();
                btnChange.Enabled = false;
            }
            else
            {
                selectedUser.Admin = true;
                if (!LogicCollection.UserLogic.UpdateUser(selectedUser)) return;
                LoadUsers();
                btnChange.Enabled = false;
            }
        }

        public void LoadUsers()
        {
            if (SiteMaster.CurrentUser() == null)
            {
                Response.Redirect("Login.aspx");
            }

            drpUsers.Items.Clear();
            foreach (var user in LogicCollection.UserLogic.AllUsers)
            {
                if (user.ID == SiteMaster.CurrentUser().ID) continue;
                var item =
                    new ListItem($@"ID: {user.ID} - {user.Username} - {(user.Admin ? "Admininistrator" : "User")}",
                        user.ID.ToString());
                drpUsers.Items.Add(item);
            }
        }
    }
}