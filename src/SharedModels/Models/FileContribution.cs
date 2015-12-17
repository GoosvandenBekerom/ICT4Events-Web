using System;
using SharedModels.Enums;

namespace SharedModels.Models
{
    public class FileContribution : Contribution
    {
        public int CategoryID { get; }
        public string Filepath { get; set; }
        public long Filesize { get; set; }

        public FileContribution(int id, int userId, DateTime date, int categoryId, string filepath, long filesize)
            : base(id, userId, date, ContributionType.File, false, false)
        {
            Filepath = filepath;
            Filesize = filesize;
        }
    }
}
