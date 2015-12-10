using System;

namespace SharedModels.Models
{
    public class Event
    {
        public int ID { get; }
        public int LocationID { get; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }

        public Event(int id, int locationId, string name, DateTime start, DateTime end, int capacity = 100)
        {
            ID = id;
            LocationID = locationId;
            Name = name;
            StartDate = start;
            EndDate = end;
            Capacity = capacity;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}