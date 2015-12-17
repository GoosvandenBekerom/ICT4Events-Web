namespace SharedModels.Models
{
    
    public class Location
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Location(int id, string name, string address, int number, string postalCode, string city)
        {
            ID = id;
            Name = name;
            Address = address;
            Number = number;
            PostalCode = postalCode;
            City = city;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
