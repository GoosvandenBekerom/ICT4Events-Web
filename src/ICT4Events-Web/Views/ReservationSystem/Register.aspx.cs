using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Data.OracleContexts;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.ReservationSystem
{
    public partial class Webform : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (var item in LogicCollection.PlaceLogic.GetAllPlaces().Select(place => new ListItem("Pleknummer: " + place.ID, place.ID.ToString())))
            {
                drpListOfPlaces.Items.Add(item);
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
            User user1 = null;
            User user2 = null;
            User user3 = null;
            User user4 = null;
            User user5 = null;
            
            // Leader information
            var lFirstname = leader_first_name.Text;
            var lSurname = leader_last_name.Text;
            var lAddress = leader_address.Text;
            var lCity = leader_city.Text;
            var lIban = leader_iban.Text;
            var lEmail = leader_Email.Text;
            var lPass = leader_Password.Text;

            // Making person of leader
            var person = new Person(0, lFirstname, lSurname, lAddress, lCity, lIban); // local person
            if (!LogicCollection.PersonLogic.Insert(person)) {return;} // insert person
            person = LogicCollection.PersonLogic.GetLastAdded(); // get person out of database

            var reservation = new Reservation(0, person.ID, DateTime.Now, DateTime.Now, false); // local reservation TODO: FIX DATES

            var leaderUser = new User(0, null, lEmail, lPass, false, lPass);

            //Reservation res = new Reservation(0, );

            #region     checking reservations emailadresses
            // Checking fields of reservation 1
            if (CheckEmptyEmailStatus(Email1))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email1.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                user1 = new User(0, null, Email1.Text, null, false, null);
            }
            // Checking fields of reservation 2
            if (CheckEmptyEmailStatus(Email2))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email2.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                user2 = new User(0, null, Email2.Text, null, false, null);
            }
            // Checking fields of reservation 3
            if (CheckEmptyEmailStatus(Email3))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email3.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                user3 = new User(0, null, Email3.Text, null, false, null);
            }

            // Checking fields of reservation 4
            if (CheckEmptyEmailStatus(Email4))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email4.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                user4 = new User(0, null, Email4.Text, null, false, null);
            }

            // Checking fields of reservation 5
            if (CheckEmptyEmailStatus(Email5))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(Email5.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                user5 = new User(0, null, Email5.Text, null, false, null);
            }
#endregion

            // checking if users is not null send email and insert into database
            if (user1 != null)
            {
                // send email and insert into database and make reservationAccount 
                var register = LogicCollection.UserLogic.RegisterUser(user1);
                if (register)
                {
                    var res = new ReservationAccount(0, 0, 0, false);
                }
            }else if (user2 != null)
            {
                // send email and insert into database and make reservationAccount 
            }
            else if(user3 !=null)
            {
                // send email and insert into database and make reservationAccount 
            }
            else if (user4 != null)
            {
                // send email and insert into database and make reservationAccount 
            }
            else if(user5 != null)
            {
                // send email and insert into database and make reservationAccount 
            }

            // Counting field of reservations
            count = CheckEmptyEmailCount(Email1, count);
            count = CheckEmptyEmailCount(Email2, count);
            count = CheckEmptyEmailCount(Email3, count);
            count = CheckEmptyEmailCount(Email4, count);
            count = CheckEmptyEmailCount(Email5, count);

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