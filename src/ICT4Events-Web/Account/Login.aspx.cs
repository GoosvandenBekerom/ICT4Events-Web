using System;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ProofOfConceptWebtechnieken.Models;

namespace ICT4Events_Web.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            // ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            // OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (!IsValid) return;
            string adPath = "LDAP://DC=event,DC=local"; //Path to your LDAP directory server
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (true == adAuth.IsAuthenticated("event.local", Email.Text, Password.Text))
                {
                    string groups = adAuth.GetGroups();

                    //Create the ticket, and add the groups.
                    bool isCookiePersistent = RememberMe.Checked;
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                              Email.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                    //Encrypt the ticket.
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    //Create a cookie, and then add the encrypted ticket to the cookie as data.
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    if (true == isCookiePersistent)
                        authCookie.Expires = authTicket.Expiration;

                    //Add the cookie to the outgoing cookies collection.
                    Response.Cookies.Add(authCookie);

                    //You can redirect now.
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(Email.Text, false));
                    //Label5.Text = "User is in Active Directory";
                    //Label5.ForeColor = Color.Green;
                }
                else
                {
                    errorLabel.Text = "Authentication did not succeed. Check user name and password.";
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                //Label5.Text = "User is NOT in Active Directory";
                //Label5.ForeColor = Color.Red;
            }

            //// Validate the user password
            //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            //// This doen't count login failures towards account lockout
            //// To enable password failures to trigger lockout, change to shouldLockout: true
            //var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //        break;
            //    case SignInStatus.LockedOut:
            //        Response.Redirect("/Account/Lockout");
            //        break;
            //    case SignInStatus.RequiresVerification:
            //        Response.Redirect(
            //            string.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
            //                Request.QueryString["ReturnUrl"],
            //                RememberMe.Checked),
            //            true);
            //        break;
            //    case SignInStatus.Failure:
            //    default:
            //        FailureText.Text = "Invalid login attempt";
            //        ErrorMessage.Visible = true;
            //        break;
            //}
        }
    }
}