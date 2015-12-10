using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class File : Contribution
    {
        public int CategoryID { get; }
        public string Filepath { get; set; }
        public long Filesize { get; set; }

        public File(int id, int userId, DateTime date, int categoryId, string filepath, long filesize)
            : base(id, userId, date, ContributionType.File, false, false)
        {
            Filepath = filepath;
            Filesize = filesize;
        }
    }
}
