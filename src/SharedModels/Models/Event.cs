using System;

namespace SharedModels.Models
{
    public class Event
    {
        public int ID { get; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public Event(int id, string name, DateTime start, DateTime end, string location = "", int capacity = 100)
        {
            ID = id;
            Name = name;
            StartDate = start;
            EndDate = end;
            Location = location;
            Capacity = capacity;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}