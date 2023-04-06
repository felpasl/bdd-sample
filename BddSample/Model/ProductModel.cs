namespace BddSample.Model
{
    // This is a class representing a product.
    public class Product
    {
        // This property represents the unique identifier for the product.
        public Guid Id { get; set; }

        // This property represents the name of the product.
        public string Name { get; set; }

        // This property represents the description of the product.
        public string Description { get; set; }

        // This property represents the price of the product.
        public decimal Price { get; set; }

        // This is the constructor for the Product class.
        public Product(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}