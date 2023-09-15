using Spectre.Console;

namespace CoffeeShop.POS
{
    public static class ProductController
    {
        internal static void AddProduct(string name)
        {
            using var db = new ProductsContext();
            db.Add(new Product { Name = name });
            db.SaveChanges();
        }

        internal static void DeleteProduct()
        {
            using var db = new ProductsContext();
        }

        internal static Product GetProductById(int id)
        {
            using var db = new ProductsContext();
            var product = db.Products.SingleOrDefault(x => x.Id == id);

            return product;
        }

        internal static List<Product> GetProducts()
        {
            using var db = new ProductsContext();
            var products = db.Products.ToList();

            return products;
        }

        internal static void UpdateProduct()
        {
            throw new NotImplementedException();
        }
    }
}
