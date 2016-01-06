using System;
using System.Text.RegularExpressions;

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
        public bool Admin { get; set; }

        public User(int id, string username, string email, string hash, bool activated, string password, bool permission = false)
        {
            ID = id;
            Username = username;
            Email = email;
            Password = password;
            ActivationHash = hash;
            Activated = activated;
            Admin = permission;
        }

        public override string ToString()
        {
            return $"{ID} | {Username}";
        }
    }
}
