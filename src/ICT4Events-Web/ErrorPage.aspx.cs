using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Debug;

namespace ICT4Events_Web
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create safe error messages
            var generalErrorMsg = "Er heeft zich een probleem afgespeeld op de website. Probeer het later opnieuw." +
                              Environment.NewLine +
                              "Als het probleem vaker voorkomt, neem s.v.p. contact op met afdeling support.";
            var httpErrorMsg = "Pagina niet gevonden. Probeer het later opnieuw.";
            var unhandledErrorMsg = "De fout was niet afgehandeld in de applicatiecode.";

            // Display safe error message
            FriendlyErrorMsg.Text = generalErrorMsg;

            // Determine where error was handled
            var errorHandler = Request.QueryString["handler"] ?? "Errorpagina";

            // Get the last error from the server
            var ex = Server.GetLastError();

            // Get the error number passed as a querystring value
            var errorMsg = Request.QueryString["msg"];
            if (errorMsg == "404")
            {
                ex = new HttpException(404, httpErrorMsg, ex);
                FriendlyErrorMsg.Text = ex.Message;
            }

            // If the exception no longer exists, create a generic exception
            if (ex == null)
            {
                ex = new Exception(unhandledErrorMsg);
            }

            // Show error details if viewed on the local developer machine
            if (Request.IsLocal)
            {
                // Detailed Error Message.
                ErrorDetailedMsg.Text = ex.Message;

                // Show where the error was handled.
                ErrorHandler.Text = errorHandler;

                // Show local access details.
                DetailedErrorPanel.Visible = true;

                if (ex.InnerException != null)
                {
                    InnerMessage.Text = ex.GetType() + "<br/>" +
                        ex.InnerException.Message;
                    InnerTrace.Text = ex.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = ex.GetType().ToString();
                    if (ex.StackTrace != null)
                    {
                        InnerTrace.Text = ex.StackTrace.TrimStart();
                    }
                }
            }

            // Log the exception.
            Logger.Write(ex.Message);

            // Clear the error from the server.
            Server.ClearError();
        }
    }
}