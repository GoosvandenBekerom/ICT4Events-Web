using System;
using System.Text.RegularExpressions;
using SharedModels.Enums;

namespace SharedModels.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// User is primarily used for authentication and identification purposes.
    /// </summary>
    public class User
    {
        public int ID { get; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActivationHash { get; set; }
        public bool Activated { get; set; }
        public PermissionType Permission { get; set; }

        public User(int id, string username, string email, string hash, bool activated)
        {
            ID = id;
            Username = username;
            Email = email;
            ActivationHash = hash;
            Activated = activated;
        }

        /// <summary>
        /// Checks if given string is a correctly formatted Email address.
        /// Assumes the RFC 2822 Format
        /// </summary>
        /// <param name="email">Email string to check</param>
        /// <returns>Returns true if specified string is a correctly formatted email address, returns false if not</returns>
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return $"{ID} | {Username}";
        }
    }
}
