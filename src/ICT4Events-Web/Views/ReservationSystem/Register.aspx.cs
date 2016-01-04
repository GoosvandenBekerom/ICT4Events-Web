using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Data.OracleContexts;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web
{
    public partial class Webform : Page
    {
        PersonOracleContext personContext = new PersonOracleContext();

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
                lblError.Visible = true;
                lblError.Text = "Invalide registratie!";
                lblError.CssClass = "text-danger";
                return;
            }
            // variables
            var count = 0;

            // Leader information
            var lFirstname = leader_first_name.Text;
            var lSurname = leader_last_name.Text;
            var lAddress = leader_address.Text;
            var lCity = leader_city.Text;
            var lIban = leader_iban.Text;
            var lEmail = leader_Email.Text;
            var lPass = leader_Password.Text;

            // checking field of reservations
            count = CheckEmptyEmailCount(Email1, count);
            count = CheckEmptyEmailCount(Email2, count);
            count = CheckEmptyEmailCount(Email3, count);
            count = CheckEmptyEmailCount(Email4, count);
            count = CheckEmptyEmailCount(Email5, count);

            if (CheckEmptyEmailStatus(Email1))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email1.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                var user1 = new User(0, null, Email1.Text, null, false, null);
            }

            // Making person of leader
            //var person = new Person(0, firstname, surname, pAddress, pCity, ib);
            //if (!personContext.Insert(person)) {return;}

            lblError.Visible = true;
            lblError.Text =
                (IsValid ? "Succesvol!" : "Onsuccesvol") +
                "<br />Voornaam: " + lFirstname +
                "<br />Achternaam: " + lSurname +
                "<br />Adres: " + lAddress +
                "<br />Woonplaats: " + lCity +
                "<br />IBAN: " + lIban +
                "<br />Email: " + lEmail +
                "<br />Pass: " + lPass +
                "<br />Count: " + count;
        }

        private static int CheckEmptyEmailCount(ITextControl txtbox, int count)
        {
            if (txtbox.Text != string.Empty || (txtbox.Text.Trim()) != string.Empty) { count += 1; } else { txtbox.Text = string.Empty; }
            return count;
        }

        private static bool CheckEmptyEmailStatus(ITextControl txtbox)
        {
            return txtbox.Text != string.Empty || (txtbox.Text.Trim()) != string.Empty;
        }
    }
}