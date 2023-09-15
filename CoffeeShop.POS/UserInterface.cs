using CoffeeShop.POS.Models;
using Spectre.Console;

namespace CoffeeShop.POS;

public static class UserInterface
{
    internal static void MainMenu()
    {
        var isAppRunning = true;

        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        Enums.MenuOptions.AddProduct,
                        Enums.MenuOptions.DeleteProduct,
                        Enums.MenuOptions.UpdateProduct,
                        Enums.MenuOptions.ViewProduct,
                        Enums.MenuOptions.ViewAllProducts,
                        Enums.MenuOptions.Quit
                    )
            );

            switch (option)
            {
                case Enums.MenuOptions.AddProduct:
                    ProductService.InsertProduct();
                    break;
                case Enums.MenuOptions.DeleteProduct:
                    ProductService.DeleteProduct();
                    break;
                case Enums.MenuOptions.UpdateProduct:
                    ProductService.UpdateProduct();
                    break;
                case Enums.MenuOptions.ViewProduct:
                    ProductService.GetProduct();
                    break;
                case Enums.MenuOptions.ViewAllProducts:
                    ProductService.GetProducts();
                    break;
                case Enums.MenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }

    internal static void ShowProductTable(List<Product> products)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Price");

        foreach (var product in products)
        {
            table.AddRow(product.ProductId.ToString(), product.Name, product.Price.ToString("C"));
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowProduct(Product product)
    {
        var panel = new Panel(
            $@"Id: {product.ProductId}
Name: {product.Name}"
        );
        panel.Header = new PanelHeader("Product Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }
}
