using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Material
    {
        public int ID { get; }
        public string Name { get; set; }
        public int EventID { get; }
        public int TypeID { get; set; }
        public int? GuestID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Material(int id, string name, int ev, int type)
        {
            ID = id;
            Name = name;
            EventID = ev;
            TypeID = type;
        }

        public Material(int id, string name, int ev, int type, DateTime start, DateTime end, int guestID)
            : this(id, name, ev, type)
        {
            GuestID = guestID;
            StartDate = start;
            EventID = ev;
            EndDate = end;
        }

        public Material(int id, string name, int ev, int type, DateTime start, DateTime end)
            : this(id, name, ev, type)
        {
            StartDate = start;
            EventID = ev;
            EndDate = end;
        }

        //Empty Constructor
        public Material()
        {
        }

        public Material(int id, string name, int ev)
        {
            ID = id;
            Name = name;
            EventID = ev;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
