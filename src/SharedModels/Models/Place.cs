using System.Drawing;

namespace SharedModels.Models
{
    public class Place
    {
        public int ID { get; }
        public int LocationID { get; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public Point Coordinates { get; set; }
        public bool TapWater { get; }
        public bool Comfortable { get; }
        public bool Handicap { get; }
        public int Size { get; }

        public Place(int id, int locationId, string name, int capacity, decimal price, Point coordinates, bool tapWater, bool comfortable, bool handicap, int size)
        {
            ID = id;
            LocationID = locationId;
            Name = name;
            Capacity = capacity;
            Price = price;
            Coordinates = coordinates;
            TapWater = tapWater;
            Comfortable = comfortable;
            Handicap = handicap;
            Size = size;
        }

        public override string ToString()
        {
            return $"{Name} - {Price.ToString("C")}";
        }
    }
}