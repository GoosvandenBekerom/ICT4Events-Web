using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class ReservationLogic
    {
        private readonly IReservationContext _context;

        public ReservationLogic(IReservationContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAllReservations()
        {
            return _context.GetAll();
        }

        public Reservation GetByID(int id)
        {
            return _context.GetById(id);
        }

        public bool Insert(Reservation reservation)
        {
            return _context.Insert(reservation);
        }

        public Reservation GetLastAdded()
        {
            return _context.GetLastAdded();
        }

        public bool InsertReservationAccount(ReservationAccount reservation)
        {
            return _context.InsertReservationAccount(reservation);
        }

        public int GetCountReservationOfPlace(int id)
        {
            return _context.GetCountReservationOfPlace(id);
        }

        public bool ReservationMail(User user, Event ev, Place location, DateTime start, DateTime end)
        {
            return SendConfirmationEmail(user, ev, location, start, end);
        }

        public bool UpdateReservation(Reservation res)
        {
            return _context.Update(res);
        }

        private static bool SendConfirmationEmail(User user, Event ev, Place location, DateTime start, DateTime end)
        {
            var fromAddress = new MailAddress(Properties.Settings.Default.Email, "ICT4Events");
            var toAddress = new MailAddress(user.Email, user.Username);
            var fromPassword = Properties.Settings.Default.EmailPassword;

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Confirmation of your new user account for ICT4Events",
                Body =
                    "Hello " + user.Username + ",\r\n\r\n" +
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

