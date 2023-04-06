using BddSample.Model;
using Bogus;
using System.Reflection;
namespace BddSample.Service
{
    public class ProductService
    {
        private readonly List<Product> _products;

        // This code defines a ProductService class that generates a list of 1000 fake products upon instantiation.
        public ProductService()
        {
            // Create a new instance of the Faker library for generating realistic-looking fake data
            var faker = new Faker();

            // Initialize an empty list of Product objects
            _products = new List<Product>();

            // For each of the 1000 products we want to generate...
            for (int i = 0; i < 1000; i++)
            {
                // Generate random values for the product's name, description, and price using the Faker library
                var name = faker.Commerce.ProductName();
                var description = faker.Lorem.Paragraph();
                var price = decimal.Parse(faker.Commerce.Price(10, 1000));

                // Create a new Product object with a unique ID, using the random values generated above
                var product = new Product(Guid.NewGuid(), name, description, price);

                // Add the newly created product to the list of products
                _products.Add(product);
            }
        }


        public List<Product> GetProducts(string[] filtro, int? page, int? size, string? sort)
        {
            var products = _products;
            
            products = ApplyFilter(filtro, products);
            
            if (!string.IsNullOrEmpty(sort))
            {
                // If the sort value starts with "-", sort by descending order
                bool isDescending = sort.StartsWith("-");

                // Remove the "-" character from the sort value if it exists
                string fieldName = sort.TrimStart('-');

                // Get the corresponding property info based on the field name
                PropertyInfo propInfo = typeof(Product).GetProperty(fieldName)!;
                if (propInfo != null)
                {
                    if (isDescending)
                    {
                        products = products.OrderByDescending(p => propInfo.GetValue(p, null)).ToList();
                    }
                    else
                    {
                        products = products.OrderBy(p => propInfo.GetValue(p, null)).ToList();
                    }
                }
            }
            if (page != null && size != null)
            {
                products = products.Skip((page.Value - 1) * size.Value).Take(size.Value).ToList();
            }
            return products;
        }

        // This method calculates the number of products that match a set of filtering criteria.
        internal int GetProductCount(string[] filtro)
        {
            // Copy the list of products to a local variable
            var products = _products;

            products = ApplyFilter(filtro, products);

            // Return the final count of products that match the filters
            return products.Count();
        }

        private static List<Product> ApplyFilter(string[] filtro, List<Product> products)
        {
            IQueryable<Product> query = products.AsQueryable();
            // If there are any filter criteria provided...
            if (filtro != null && filtro.Length > 0)
            {
                // Apply each filter in turn, modifying the `products` list with each one
                foreach (var f in filtro)
                {
                    query = query.Where(p => p.Description.Contains(f));
                }
            }

            return query.ToList();
        }
    }
}