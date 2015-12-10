using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Item : Product
    {
        public int ID { get; }
        public int Serial { get; set; }
        public int Barcode { get; set; }

        public Item(int id, int categoryId, string brand, string serie, int typeNumber, decimal price, int serial, int barcode) : base(id, categoryId, brand, serie, typeNumber, price)
        {
            ID = id;
            Serial = serial;
            Barcode = barcode;
        }

        public override string ToString()
        {
            return $"ID: {ID} | {Brand} {Serie} {TypeNumber} | {Price.ToString("C")}";
        }
    }
}
