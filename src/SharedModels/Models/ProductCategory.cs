using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class ProductCategory
    {
        public int ID { get; }
        public int MainCategoryID { get; }
        public string Name { get; set; }

        public ProductCategory(int id, int mainCategoryId, string name)
        {
            ID = id;
            MainCategoryID = mainCategoryId;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
