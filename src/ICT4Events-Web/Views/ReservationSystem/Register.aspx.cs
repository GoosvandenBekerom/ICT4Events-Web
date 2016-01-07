using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
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

            if (!Page.IsPostBack)
            {
                drpListOfPlaces.Items.Clear();
                foreach (
                    var item in
                        LogicCollection.PlaceLogic.GetAllPlaces()
                            .Select(place => new ListItem("Pleknummer: " + place.ID, place.ID.ToString())))
                {
                    drpListOfPlaces.Items.Add(item);
                }
            }

            LoadPlace(Convert.ToInt32(drpListOfPlaces.SelectedValue));
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

            // Counting field of reservations
            count = CheckEmptyEmailCount(Email1, count);
            count = CheckEmptyEmailCount(Email2, count);
            count = CheckEmptyEmailCount(Email3, count);
            count = CheckEmptyEmailCount(Email4, count);
            count = CheckEmptyEmailCount(Email5, count);

            // Leader information
            var lFirstname = leader_first_name.Text;
            var lSurname = leader_last_name.Text;
            var lAddress = leader_address.Text;
            var lCity = leader_city.Text;
            var lUsername = leader_Username.Text;
            var lIban = leader_iban.Text;
            var lEmail = leader_Email.Text;
            var lPass = LogicCollection.UserLogic.GetHashedPassword(leader_Password.Text);

            var placeId = Convert.ToInt32(drpListOfPlaces.SelectedValue);
            var reservationOnPlace = LogicCollection.ReservationLogic.GetCountReservationOfPlace(placeId);

            if ((count + 1 + reservationOnPlace) > LogicCollection.PlaceLogic.GetPlaceByID(placeId).Capacity)
            {
                return; // Too much people on that place
            }

            // Making person of leader
            var person = new Person(0, lFirstname, lSurname, lAddress, lCity, lIban); // local person
            //if (!LogicCollection.PersonLogic.Insert(person)) {return;} // insert person
            person = LogicCollection.PersonLogic.GetLastAdded(); // get person out of database

            // Register leader
            var lhash = Membership.GeneratePassword(8, 2);
            var leaderUser = new User(0, lUsername, lEmail, lhash, false, lPass);
            //if (!LogicCollection.UserLogic.RegisterUser(leaderUser)) {return;}
            leaderUser = LogicCollection.UserLogic.GetLastAdded();

            // Making reservation
            var reservation = new Reservation(0, person.ID, StartDate.SelectedDate, EndDate.SelectedDate, false); // local reservation
            //if (!LogicCollection.ReservationLogic.Insert(reservation)){return;} // insert reservation
            reservation = LogicCollection.ReservationLogic.GetLastAdded(); // get reservation out of database

            // Making reservation_account
            var reservationAccount = new ReservationAccount(0, reservation.ID, leaderUser.ID, placeId);
            //if (!LogicCollection.ReservationLogic.InsertReservationAccount(reservationAccount)) { return; }
            

            #region checking reservations emailadresses & Reservations of users
            
            // Listof Textboxes 
            var listOfEmailReservation = new List<TextBox>()
            {
                Email1, Email2, Email3, Email4, Email5
            };

            var reservationsOfNewUser = new List<User>();
            // Checking Emailadres if not empty 
            foreach (var email in listOfEmailReservation.Where(email => CheckEmptyEmailStatus(email)))
            {
                if (!LogicCollection.UserLogic.IsValidEmail(email.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalide emailadressen.";
                    return;
                }

                var result = Regex.Match(email.Text, @"^.*?(?=@)").Value;

                var hash = Membership.GeneratePassword(8 , 0);
                reservationsOfNewUser.Add(new User(0, result, email.Text, hash, false, null));
            }

            // Adding user to database and make reservation 
            foreach (var user in reservationsOfNewUser)
            {
                // checking if users is not null send email and insert into database
                if (user != null)
                {
                    //send email and insert into database and make reservationAccount
                    var password = Membership.GeneratePassword(10, 2);
                    var register = LogicCollection.UserLogic.RegisterUser(user, true, password);
                    var userLast = LogicCollection.UserLogic.GetLastAdded();
                    if (register)
                    {
                        var res = new ReservationAccount(0, reservation.ID, userLast.ID, placeId);
                        if (!LogicCollection.ReservationLogic.InsertReservationAccount(res)) { return; }
                    }
                }
            }
            #endregion

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
                "<br />Meerdere reserveerders: " + count +
                "<br />PlaceID: " + placeId +
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


        public void LoadPlace(int id)
        {
            var place = LogicCollection.PlaceLogic.GetPlaceByID(Convert.ToInt32(id));
            lblPlaceId.Text = place.ID.ToString();
            lblCap.Text = LogicCollection.ReservationLogic.GetCountReservationOfPlace(id)+" - "+place.Capacity.ToString();
            lblPrice.Text = place.Price.ToString("C0");
            lblHandicap.Text = place.Handicap.ToString();
            lblWater.Text = place.TapWater.ToString();
            lblSize.Text = place.Size.ToString();
        }
    }
}