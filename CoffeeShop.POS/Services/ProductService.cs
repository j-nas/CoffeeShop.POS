using CoffeeShop.POS.Controllers;
using CoffeeShop.POS.Models;
using Spectre.Console;

namespace CoffeeShop.POS.Services;

public class ProductService
{
    internal static void InsertProduct()
    {
        var product = new Product();
        product.Name = AnsiConsole.Ask<string>("What is the name of the product?");
        product.Price = AnsiConsole.Ask<decimal>("What is the new price of the product?");

        ProductController.AddProduct(product);
    }

    internal static void DeleteProduct()
    {
        var product = GetProductOptionInput();
        ProductController.DeleteProduct(product);
    }

    internal static void GetProducts()
    {
        var products = ProductController.GetProducts();
        UserInterface.ShowProductTable(products);
    }

    internal static void GetProduct()
    {
        var product = GetProductOptionInput();
        UserInterface.ShowProduct(product);
    }

    internal static void UpdateProduct()
    {
        var product = GetProductOptionInput();

        product.Name = AnsiConsole.Confirm("Would you like to update the name of the product?")
            ? AnsiConsole.Ask<string>("What is the new name of the product?")
            : product.Name;
        product.Price = AnsiConsole.Confirm("Would you like to update the price of the product?")
            ? AnsiConsole.Ask<decimal>("What is the new price of the product?")
            : product.Price;
        ProductController.UpdateProduct(product);
    }

    private static Product GetProductOptionInput()
    {
        var products = ProductController.GetProducts();
        var productsArray = products.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Choose Product").AddChoices(productsArray)
        );

        var id = products.Single(x => x.Name == option).ProductId;
        var product = ProductController.GetProductById(id);

        return product;
    }
}
