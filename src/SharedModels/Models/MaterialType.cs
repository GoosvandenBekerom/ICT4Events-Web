namespace SharedModels.Models
{
    // TODO: Specify event?
    public class MaterialType
    {
        public int ID { get; }
        public string Name { get; set; }
        //public int EventID { get; }

        public MaterialType(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}