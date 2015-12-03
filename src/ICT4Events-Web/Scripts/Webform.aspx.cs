using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProofOfConceptWebtechnieken
{
    public partial class Webform : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // stuff
            }
            else
            {
                // stuff
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            feedbackPanel.Visible = true;

            if (!IsValid)
            {
                Label1.Text = "Invalide registratie!";
                Label1.CssClass = "text-danger";
                return;
            }

            var firstname = first_name.Text;
            var surname = last_name.Text;
            var telephone = phone_number.Text;
            var ib = iban.Text;

            Label1.Visible = true;
            Label1.Text = "Voornaam: "+ firstname + "<br />Achternaam: " + surname + "<br />Telefoonnummer: " + telephone +"<br />IBAN: "+ib;
        }
    }
}