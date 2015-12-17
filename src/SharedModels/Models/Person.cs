using System;
using System.Linq;
using System.Text.RegularExpressions;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Person
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Street => Address.Replace(" " + Regex.Match(Address, @"\d+ ?\w*"), "");
        public string HouseNr => Regex.Match(Address, @"\d+ ?\w*").ToString();
        public string City { get; set; }
        public string IBAN { get; set; }

        public Person(int id, string name, string surname, string address, string city, string iban)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Address = address;
            City = city;
            IBAN = iban;
        }
    }
}
