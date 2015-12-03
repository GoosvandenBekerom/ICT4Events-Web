using SharedModels.Enums;

namespace SharedModels.Models
{
    public class Media
    {
        public int ID { get; }
        public int UserID { get; }
        public int EventID { get; }
        public MediaType Type { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public string Path { get; set; }

        public Media(int id, int userId, int eventId, MediaType type, string name, string fileExtension, string path)
        {
            ID = id;
            UserID = userId;
            EventID = eventId;
            Type = type;
            Name = name;
            FileExtension = fileExtension;
            Path = path;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Name: {Name}.{FileExtension}";
        }
    }
}