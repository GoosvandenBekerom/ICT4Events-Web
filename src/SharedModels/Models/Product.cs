namespace SharedModels.Models
{
    public class Product
    {
        public int ProductID { get; }
        public int CategoryID { get; }
        public string Brand { get; set; }
        public string Serie { get; set; }
        public int TypeNumber { get; set; }
        public decimal Price { get; set; }

        public Product(int id, int categoryId, string brand, string serie, int typeNumber, decimal price)
        {
            ProductID = id;
            CategoryID = categoryId;
            Brand = brand;
            Serie = serie;
            TypeNumber = typeNumber;
            Price = price;
        }
    }
}