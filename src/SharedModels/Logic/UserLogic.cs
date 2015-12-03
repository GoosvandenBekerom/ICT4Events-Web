using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using SharedModels.Models;
using System.Security.Cryptography;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Debug;
using SharedModels.Enums;
using SharedModels.Exceptions;

namespace SharedModels.Logic
{
    public class UserLogic
    {
        private readonly IUserContext _context;
        private static readonly string Salt = GetHashString("saltyString");

        public List<User> AllUsers => _context.GetAll();

        public UserLogic()
        {
            _context = new UserOracleContext();
        }

        public UserLogic(IUserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sets a new password for a user
        /// </summary>
        /// <param name="user">User to change password of</param>
        /// <param name="oldPass">Current password of given user</param>
        /// <param name="newPass">New password</param>
        /// <param name="newPassAgain">New password again for security check</param>
        /// <returns>true if password was changed succesfully, false if something went wrong or given oldPass is incorrect</returns>
        public bool SetNewPassword(User user, string oldPass, string newPass, string newPassAgain)
        {
            if (user.Password != GetHashedPassword(oldPass)) return false;

            user.Password = CheckAndHashPassword(newPass, newPassAgain);
            return UpdateUser(user);
        }

        /// <summary>
        /// Hashes the password for a user if the 2 given passwords match.
        /// </summary>
        /// <param name="password">password given by user</param>
        /// <param name="passwordAgain">password again given by user</param>
        public string CheckAndHashPassword(string password, string passwordAgain)
        {
            if (string.Equals(password, passwordAgain))
                return GetHashString(password + Salt);

            throw new PasswordsDontMatchException();
        }

        /// <summary>
        /// Gets a hashed password.
        /// </summary>
        /// <param name="password">password to hash</param>
        public string GetHashedPassword(string password)
        {
            return GetHashString(password + Salt);
        }

        /// <summary>
        /// Registers a new user and sends a confirmation email to the new users emailaddress
        /// </summary>
        /// <param name="user">User created in user interface</param>
        /// <returns>a new user object with correct user id</returns>
        public User RegisterUser(User user, bool generated = false, string password = "")
        {
            var registeredUser = _context.Insert(user);

            try
            {
                SendConfirmationEmail(registeredUser, generated, password);
            }
            catch (MailWasNotSentException e)
            {
                Logger.Write(e.Message);
                throw new MailWasNotSentException();
            }
            
            return registeredUser;
        }

        /// <summary>
        /// Authenticates given username and password to check whether their credentials match with an account
        /// </summary>
        /// <param name="username">Given username (email)</param>
        /// <param name="password">Given password</param>
        /// <returns>User that matches given credentials</returns>
        public User AuthenticateUser(string username, string password)
        {
            var user = _context.AuthenticateUser(username, password);
            
            if (user != null)
            {
                return user;
            }

            throw new InvalidCredentialException("Uw inloggegevens komen niet overeen met een bestaand account.");
        }

        /// <summary>
        /// Retrieves a user object based on user ID
        /// </summary>
        /// <param name="id">User ID to match on</param>
        /// <returns>User associated with given username or null if nothing was found</returns>
        public User GetById(int id)
        {
            return _context.GetById(id);
        }

        /// <summary>
        /// Retrieves a user object based on username
        /// </summary>
        /// <param name="username">Username to match on</param>
        /// <returns>User associated with given username or null if nothing was found</returns>
        public User GetByUsername(string username)
        {
            return _context.GetByUsername(username);
        }

        /// <summary>
        /// Sends a confirmation email to given user
        /// </summary>
        /// <param name="user">user to send confirmation email to</param>
        /// <returns>true if mail was successfully send, throws exception if sending mail fails</returns>
        private static bool SendConfirmationEmail(User user, bool generated = false, string password = "")
        {
            var fromAddress = new MailAddress(Properties.Settings.Default.Email, "ICT4Events");
            var toAddress = new MailAddress(user.Username, user.Name);
            var fromPassword = Properties.Settings.Default.EmailPassword;
            const string subject = "Confirmation of your new user account for ICT4Events";

            var body = 
                "Hello " + user.Name + ",\r\n\r\n" +
                "Your new account for ICT4Events was successfully created!" +
                ((generated && !string.IsNullOrWhiteSpace(password))
                    ? $"\r\nYour generated password is {password}.\r\nYou can change your password after logging in."
                    : "") +
                "\r\n\r\nHave a nice day!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
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

        /// <summary>
        /// Changes the user accounts permission level
        /// </summary>
        /// <param name="user">user to change permission level of</param>
        /// <param name="newPermissionType">PermissionType to change permission level to</param>
        public void ChangePermissionType(User user, PermissionType newPermissionType) => user.Permission = newPermissionType;


        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <returns>Updated user</returns>
        public bool UpdateUser(User user) => _context.Update(user);

        /// <summary>
        /// Deletes a user account with given user id
        /// </summary>
        /// <param name="userId">ID of the user to be deleted</param>
        /// <returns>true if delete was succesfull</returns>
        public bool DeleteUserById(int userId) => _context.Delete(_context.GetById(userId));

        /// <summary>
        /// Deletes a user account with given user
        /// </summary>
        /// <param name="user">user to be deleted</param>
        /// <returns>true if delete was succesfull</returns>
        public bool DeleteUser(User user) => _context.Delete(user);

        /// <summary>
        /// Hashes the given string in SHA1 format.
        /// </summary>
        /// <param name="inputString">String to hash</param>
        /// <returns>an array of bytes</returns>
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// Returns a hashed string in SHA1 format;
        /// </summary>
        /// <param name="inputString">String to hash</param>
        /// <returns>The resulting hashed string</returns>
        private static string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
