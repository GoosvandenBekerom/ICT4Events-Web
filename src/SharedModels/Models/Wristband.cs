using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Wristband
    {
        public int ID { get; }
        public int Barcode { get; }
        public bool Active { get; set; }

        public Wristband(int id, int barcode, bool active)
        {
            ID = id;
            Barcode = barcode;
            Active = active;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Barcode: {Barcode}, Active: {Active}";
        }
    }
}
