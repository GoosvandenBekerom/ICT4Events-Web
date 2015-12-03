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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public DateTime RegistrationDate { get; }
        public PermissionType Permission { get; set; }

        public User(int id, string username, string password, string name, string surname = "", Country country = Country.Nederland, string city = "", string postal = "",
            string address = "", string telephone = "", DateTime regDate = new DateTime(),
            PermissionType permission = PermissionType.User)
        {
            if (!IsValidEmail(username))
            {
                throw new FormatException("Username doesn't contain a valid email address");
            }

            ID = id;
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Country = country;
            City = city;
            Postal = postal;
            Address = address;
            Telephone = telephone;
            RegistrationDate = regDate;
            Permission = permission;
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
            return $"{ID} | {Name} {Surname} | {Username} | {City}, {Country} | {Telephone}";
        }
    }
}
