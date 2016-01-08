using System;
using System.Web.Configuration;
using System.Web.Security;
using Newtonsoft.Json;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.AccountSystem
{
    public partial class Edit : System.Web.UI.Page
    {
        // current user
        private User _currentUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            _currentUser = SiteMaster.CurrentUser();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get values
            var newUsername = txtUsername.Text;
            var newPass1 = txtwpass1.Text;
            var newPass2 = txtwpass2.Text;

            // Update only username
            if (string.IsNullOrEmpty(newPass1) || string.IsNullOrEmpty(newPass2))
            {
                if (!string.IsNullOrEmpty(newUsername))
                {
                    _currentUser.Username = newUsername;
                    if (LogicCollection.UserLogic.UpdateUser(_currentUser))
                    {
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    feedbackPanel.Visible = true;
                    feedbackPanel.InnerText = "Niet alle velden kunnen leeg zijn.";
                    return;
                }
            }else if (!string.IsNullOrEmpty(newUsername) && !string.IsNullOrEmpty(newPass1) &&
                      !string.IsNullOrEmpty(newPass2)) // update username, and password
            {
                // check both password and give back hash password
                try
                {
                    newPass1 = LogicCollection.UserLogic.CheckAndHashPassword(newPass1, newPass2);
                }
                catch (Exception)
                {
                    feedbackPanel.Visible = true;
                    feedbackPanel.InnerText = "Wachtwoorden zijn niet hetzelfde.";
                    return;
                }

                // change current details
                _currentUser.Password = newPass1;
                _currentUser.Username = newUsername;

                if (LogicCollection.UserLogic.UpdateUser(_currentUser))
                {
                    btnSave.Enabled = false;
                }
            }

            // Cookie change
            var cookie = FormsAuthentication.GetAuthCookie(_currentUser.Email, true); //TODO: username
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                ticket.Expiration, true, JsonConvert.SerializeObject(_currentUser), ticket.CookiePath);
            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            cookie.Expires = newTicket.Expiration.AddHours(24);
            Context.Response.Cookies.Set(cookie);

            // Redirect with query
            var redirect = "http://" + Request.Url.Authority + "" + Request.Url.AbsolutePath + "?succes=1";
            Page.Response.Redirect(redirect);
        }
    }
}