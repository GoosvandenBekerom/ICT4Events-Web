using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Newtonsoft.Json;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (!IsValid) return;

            var email = Email.Text;


            var password = LogicCollection.UserLogic.GetHashedPassword(Password.Text);

            if (!LogicCollection.UserLogic.IsValidEmail(email))
            {
                errorLabel.Text = "Uw heeft een ongeldig emailadres ingevuld.";
                return;
            }

            var currentUser = LogicCollection.UserLogic.AuthenticateUser(email, password);
            if (currentUser == null)
            {
                errorLabel.Text = "Uw inloggegevens komen niet overeen met een bestaand account.";
                return;
            }

            //contains user object in JSON format
             var ticket = new FormsAuthenticationTicket(1, currentUser.Email, DateTime.Now,
             DateTime.Now.AddMinutes(30), RememberMe.Checked, JsonConvert.SerializeObject(currentUser));

            // cookie containing copy of ticket
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                Expires = ticket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };

            Response.Cookies.Add(cookie);

            //FormsAuthentication.SetAuthCookie(currentUser.Email, true);
            //FormsAuthentication.RedirectFromLoginPage(currentUser.Email, RememberMe.Checked);
            Response.Redirect("/Default.aspx?login=1", true);
        }
    }
}