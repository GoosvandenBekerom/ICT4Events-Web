using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Category : Contribution
    {
        public int MainCategoryID { get; }
        public string Name { get; set; }

        public Category(int id, int userId, DateTime date, int mainCategoryId, string name)
            : base(id, userId, date, ContributionType.Category, false, false)
        {
            MainCategoryID = mainCategoryId;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
