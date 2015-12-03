using System.Drawing;

namespace SharedModels.Models
{
    public class Location
    {
        public int ID { get; }
        public int EventID { get; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public Point Coordinates { get; set; }

        public Location(int id, int eventID, string name, int capacity, decimal price, Point coords)
        {
            ID = id;
            EventID = eventID;
            Name = name;
            Capacity = capacity;
            Price = price;
            Coordinates = coords;
        }

        public override string ToString()
        {
            return $"{Name} - {Price.ToString("C")}";
        }
    }
}