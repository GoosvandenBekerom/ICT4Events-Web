using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.ReservationSystem
{
    public partial class Webform : Page
    {
        private Event _event;

        protected void Page_Load(object sender, EventArgs e)
        {
            _event = LogicCollection.EventLogic.GetByID(2);

            StartDate.TodaysDate = _event.StartDate;
            EndDate.TodaysDate = _event.StartDate;

            foreach (var item in LogicCollection.PlaceLogic.GetAllPlaces().Select(place => new ListItem("Pleknummer: " + place.ID + "Cap: " + place.Capacity, place.ID.ToString())))
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

            if (StartDate.SelectedDate == DateTime.Parse("1-1-0001") ||
                EndDate.SelectedDate == DateTime.Parse("1-1-0001"))
            {
                lblError.Visible = true;
                lblError.Text = "Invalide datas.";
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
            var lPass = LogicCollection.UserLogic.GetHashedPassword(leader_Password.Text);

            // Making person of leader
            var person = new Person(0, lFirstname, lSurname, lAddress, lCity, lIban); // local person
            //if (!LogicCollection.PersonLogic.Insert(person)) {return;} // insert person
            person = LogicCollection.PersonLogic.GetLastAdded(); // get person out of database

            // Register leader
            var leaderUser = new User(0, null, lEmail, lPass, false, lPass);
            if (!LogicCollection.UserLogic.RegisterUser(leaderUser)) {return;}
            leaderUser = LogicCollection.UserLogic.GetLastAdded();

            // Making reservation
            var reservation = new Reservation(0, person.ID, StartDate.SelectedDate, EndDate.SelectedDate, false); // local reservation
            //if (!LogicCollection.ReservationLogic.Insert(reservation)){return;} // insert reservation
            reservation = LogicCollection.ReservationLogic.GetLastAdded(); // get reservation out of database

            // Making reservation_account
            var reservationAccount = new ReservationAccount(0, reservation.ID, leaderUser.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));


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

            #region Reservations of users

            // checking if users is not null send email and insert into database
            if (user1 != null)
            {
                //send email and insert into database and make reservationAccount
               var register = LogicCollection.UserLogic.RegisterUser(user1, true);
                user1 = LogicCollection.UserLogic.GetLastAdded();
                if (register)
                {
                    var res = new ReservationAccount(0, reservation.ID, user1.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));
                }
            }
            else if (user2 != null)
            {
                // send email and insert into database and make reservationAccount
                var register = LogicCollection.UserLogic.RegisterUser(user2, true);
                user2 = LogicCollection.UserLogic.GetLastAdded();
                if (register)
                {
                    var res = new ReservationAccount(0, reservation.ID, user2.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));
                }
            }
            else if(user3 !=null)
            {
                // send email and insert into database and make reservationAccount 
                var register = LogicCollection.UserLogic.RegisterUser(user3, true);
                user3 = LogicCollection.UserLogic.GetLastAdded();
                if (register)
                {
                    var res = new ReservationAccount(0, reservation.ID, user3.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));
                }
            }
            else if (user4 != null)
            {
                // send email and insert into database and make reservationAccount 
                var register = LogicCollection.UserLogic.RegisterUser(user4, true);
                user4 = LogicCollection.UserLogic.GetLastAdded();
                if (register)
                {
                    var res = new ReservationAccount(0, reservation.ID, user4.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));
                }
            }
            else if(user5 != null)
            {
                // send email and insert into database and make reservationAccount 
                var register = LogicCollection.UserLogic.RegisterUser(user5, true);
                user5 = LogicCollection.UserLogic.GetLastAdded();
                if (register)
                {
                    var res = new ReservationAccount(0, reservation.ID, user5.ID, Convert.ToInt32(drpListOfPlaces.SelectedValue));
                }
            }

            #endregion


            // Counting field of reservations
            count = CheckEmptyEmailCount(Email1, count);
            count = CheckEmptyEmailCount(Email2, count);
            count = CheckEmptyEmailCount(Email3, count);
            count = CheckEmptyEmailCount(Email4, count);
            count = CheckEmptyEmailCount(Email5, count);

            lblError.Visible = true;
            lblError.Text =
                (IsValid ? "Valide gegevens!" : "Invalide gegevens") +
                "<br />Voornaam: " + lFirstname +
                "<br />Achternaam: " + lSurname +
                "<br />Adres: " + lAddress +
                "<br />Woonplaats: " + lCity +
                "<br />IBAN: " + lIban +
                "<br />Email: " + lEmail +
                "<br />Pass: " + lPass +
                "<br />Count: " + count +
                "<br />Startdatum: " + StartDate.SelectedDate.ToShortDateString() +
                "<br />Einddatum: " + EndDate.SelectedDate.ToShortDateString();
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

        #region Calenders events
        protected void StartDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date >= _event.StartDate && e.Day.Date <= _event.EndDate)
            {
                e.Day.IsSelectable = true;
            }
            else
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = Color.White;
            }
        }

        protected void EndDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date >= _event.StartDate && e.Day.Date <= _event.EndDate)
            {
                e.Day.IsSelectable = true;
            }
            else
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = Color.White;
            }
        }

        protected void EndDate_SelectionChanged(object sender, EventArgs e)
        {
            if (EndDate.SelectedDate.Date <= StartDate.SelectedDate.Date)
            {
                EndDate.SelectedDate = StartDate.SelectedDate.AddDays(1);
            }
        }

        protected void StartDate_SelectionChanged(object sender, EventArgs e)
        {
            if (EndDate.SelectedDate.Date <= StartDate.SelectedDate.Date)
            {
                EndDate.SelectedDate = StartDate.SelectedDate.AddDays(1);
            }
        }
        #endregion

    }
}