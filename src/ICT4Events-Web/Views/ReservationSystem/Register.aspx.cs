using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web.Views.ReservationSystem
{
    public partial class Webform : Page
    {
        public Event CurEvent;
        public int Count;
        public int PlaceId;

        protected void Page_Init(object sender, EventArgs e)
        {
            drpListOfPlaces.DataSource = LogicCollection.PlaceLogic.GetAllPlaces();
            drpListOfPlaces.DataValueField = "ID";
            drpListOfPlaces.DataTextField = "Name";
            drpListOfPlaces.DataBind();
            drpListOfPlaces.Items.Insert(0, "Selecteer een plek");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            CurEvent = LogicCollection.EventLogic.GetByID(2);

            StartDate.TodaysDate = CurEvent.StartDate;
            EndDate.TodaysDate = CurEvent.StartDate;
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
            Count = 0;

            // Counting field of reservations
            Count = CheckEmptyEmailCount(Email1, Count);
            Count = CheckEmptyEmailCount(Email2, Count);
            Count = CheckEmptyEmailCount(Email3, Count);
            Count = CheckEmptyEmailCount(Email4, Count);
            Count = CheckEmptyEmailCount(Email5, Count);

            // Leader information
            var lFirstname = leader_first_name.Text;
            var lSurname = leader_last_name.Text;
            var lAddress = leader_address.Text;
            var lCity = leader_city.Text;
            var lUsername = leader_Username.Text;
            var lIban = leader_iban.Text;
            var lEmail = leader_Email.Text;
            var lPass = LogicCollection.UserLogic.GetHashedPassword(leader_Password.Text);

            PlaceId = Convert.ToInt32(drpListOfPlaces.SelectedValue);
            var reservationOnPlace = LogicCollection.ReservationLogic.GetCountReservationOfPlace(PlaceId);

            if ((Count + 1 + reservationOnPlace) > LogicCollection.PlaceLogic.GetPlaceByID(PlaceId).Capacity)
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

            // sending reservation mail to leader
            //try
            //{
            //    LogicCollection.ReservationLogic.ReservationMail(leaderUser, CurEvent,
            //        LogicCollection.PlaceLogic.GetPlaceByID(PlaceId), reservation.DateStart, reservation.DateEnd);
            //}
            //catch (Exception)
            //{
            //    return;
            //}

            // Making reservation_account
            var reservationAccount = new ReservationAccount(0, reservation.ID, leaderUser.ID, PlaceId);
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
                if (user == null) continue;

                //send email and insert into database and make reservationAccount
                var password = Membership.GeneratePassword(10, 2);
                var register = LogicCollection.UserLogic.RegisterUser(user, true, password);
                var userLast = LogicCollection.UserLogic.GetLastAdded();
                if (!register) continue;
                var res = new ReservationAccount(0, reservation.ID, userLast.ID, PlaceId);
                //if (!LogicCollection.ReservationLogic.InsertReservationAccount(res)) { return; }

                // sending reservation mail to newUser
                //try
                //{
                //    LogicCollection.ReservationLogic.ReservationMail(userLast, CurEvent,
                //        LogicCollection.PlaceLogic.GetPlaceByID(PlaceId), reservation.DateStart, reservation.DateEnd);
                //}
                //catch (Exception)
                //{
                //    return;
                //}
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
                "<br />Meerdere reserveerders: " + Count +
                "<br />PlaceID: " + PlaceId +
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
            if (e.Day.Date >= CurEvent.StartDate && e.Day.Date <= CurEvent.EndDate)
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
            if (e.Day.Date >= CurEvent.StartDate && e.Day.Date <= CurEvent.EndDate)
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

        [WebMethod]
        public static string LoadPlace(int id)
        {
            var place = LogicCollection.PlaceLogic.GetPlaceByID(id);

            if (place == null) return "false";
            var result = $@"<strong > Informatie over plek {place.Name}:</strong>
                        <p> Capaciteit: {LogicCollection.ReservationLogic.GetCountReservationOfPlace(id)} / {place.Capacity}</p>
                        <p> Prijs: {place.Price.ToString("C")}</p>
                        <p> Grootte: {place.Size}</p>
                        <p> Handicap: {(place.Handicap ? "Ja" : "Nee")}</p>
                        <p> Comfortplek: {(place.Comfortable ? "Ja" : "Nee")}</p>
                        <p> Water: {(place.TapWater ? "Ja" : "Nee")}</p>";
            return result.Replace("\r\n", "");
        }
    }
}