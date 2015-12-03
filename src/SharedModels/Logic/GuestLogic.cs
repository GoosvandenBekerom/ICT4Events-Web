using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Enums;
using SharedModels.FTP;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class GuestLogic
    {
        private IGuestContext _context;

        public GuestLogic(IGuestContext context)
        {
            _context = context;
        }

        public Guest GetGuestByEvent(Event ev, int userID)
        {
            return _context.GetGuestByEvent(ev, userID);
        }

        public Guest GetByRfid(string rfid, Event ev)
        {
            return _context.GetByRfid(rfid, ev);
        }

        public int GetGuestCountByEvent(Event ev)
        {
            return _context.GetGuestCountByEvent(ev);
        }

        public List<Guest> GetGuestsByEvent(Event ev)
        {
            return _context.GetAllByEvent(ev);
        }

        public List<Guest> GetGuestsByGroup(Event ev, int leaderID)
        {
            return _context.GetGuestsByGroup(ev, leaderID);
        }

        public List<Guest> GetGuestsByUser(User user)
        {
            return _context.GetGuestsByUser(user);
        }

        public int GetGuestCountByLocation(Location location)
        {
            return _context.GetGuestCountByLocation(location);
        } 

        public bool UpdateGuest(Guest guest)
        {
            return _context.Update(guest);
        }

        public Guest RegisterUserForEvent(User user, Event ev, Location location, DateTime start, DateTime end, int leaderID = 0)
        {
            var existingGuest = _context.GetGuestByEvent(ev, user.ID); // Checks if user is already registered for an event
            if (existingGuest != null) return existingGuest;

            var guest = new Guest(user.ID, user.Username, user.Password, user.Name, "", false, ev.ID, false, start, end,
                location.ID, user.RegistrationDate, user.Permission, user.Surname, user.Country, user.City, user.Postal,
                user.Address, user.Telephone, leaderID);
            
            SendConfirmationEmail(user, ev, location, start, end);

            FtpHelper.CreateDirectory($"{ev.ID}/{guest.ID}");

            return _context.Insert(guest);
        }

        public List<Guest> RegisterUsersForEvent(List<string> usernames, Event ev, Location location, DateTime start, DateTime end, int leaderID)
        {
            var res = new List<Guest>();

            foreach (var username in usernames)
            {
                var user = LogicCollection.UserLogic.GetByUsername(username);
                var guest = (user != null
                    ? RegisterUserForEvent(user, ev, location, start, end)
                    : RegisterNewUserForEvent(username, ev, location, start, end, leaderID));

                res.Add(guest);
            }

            return res;
        }

        public Guest RegisterNewUserForEvent(string username, Event ev, Location location, DateTime start, DateTime end, int leaderID)
        {
            var password = Membership.GeneratePassword(10, 2);

            var user = new User(0, username, LogicCollection.UserLogic.GetHashedPassword(password), "new user");
            user = LogicCollection.UserLogic.RegisterUser(user, true, password);
            
            SendConfirmationEmail(user, ev, location, start, end);

            return RegisterUserForEvent(user, ev, location, start, end, leaderID);
        }

        /// <summary>
        /// Sends a confirmation email to given user
        /// </summary>
        /// <param name="user">user to send confirmation email to</param>
        /// <returns>true if mail was successfully send, throws exception if sending mail fails</returns>
        private static bool SendConfirmationEmail(User user, Event ev, Location location, DateTime start, DateTime end)
        {
            var fromAddress = new MailAddress(Properties.Settings.Default.Email, "ICT4Events");
            var toAddress = new MailAddress(user.Username, user.Name);
            var fromPassword = Properties.Settings.Default.EmailPassword;

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Confirmation of your new user account for ICT4Events",
                Body =
                    "Hello " + user.Name + ",\r\n\r\n" +
                    $"Your have been registered to participate in event {ev.Name}!\r\n" +
                    $"The location you entered is {location.Name}.\r\n" +
                    $"We will be expecting to see you on {start.Date} until {end.Date}.\r\n" +
                    $"Your user ID is: {user.ID}. Make sure to remember this for your check-in!" +
                    "\r\n\r\nHave a nice day!"
            };

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                smtp.Send(message);
                return true;
            }
            catch (SmtpException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
