using CoffeeShop.POS.Models;
using CoffeeShop.POS.Models.DTOs;
using CoffeeShop.POS.Services;
using static CoffeeShop.POS.Enums;
using Spectre.Console;

namespace CoffeeShop.POS;

internal static class UserInterface
{
    internal static void MainMenu()
    {
        var isAppRunning = true;

        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        MainMenuOptions.ManageCategories,
                        MainMenuOptions.ManageProducts,
                        MainMenuOptions.ManageOrders,
                        MainMenuOptions.GenerateReport,
                        MainMenuOptions.Quit
                    )
            );

            switch (option)
            {
                case MainMenuOptions.ManageCategories:
                    CategoriesMenu();
                    break;
                case MainMenuOptions.ManageProducts:
                    ProductsMenu();
                    break;
                case MainMenuOptions.ManageOrders:
                    OrdersMenu();
                    break;
                case MainMenuOptions.GenerateReport:
                    ReportService.CreateReport();
                    break;
                default:
                    isAppRunning = false;
                    break;
            }
        }
    }

    private static void OrdersMenu()
    {
        var isOrderMenuRunning = true;
        while (isOrderMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<OrderMenu>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        OrderMenu.AddOrder,
                        OrderMenu.GetOrders,
                        OrderMenu.GetOrder,
                        OrderMenu.GoBack
                    )
            );

            switch (option)
            {
                case OrderMenu.AddOrder:
                    OrderService.InsertOrder();
                    break;
                case OrderMenu.GetOrders:
                    OrderService.GetOrders();
                    break;
                case OrderMenu.GetOrder:
                    OrderService.GetOrder();
                    break;
                default:
                    isOrderMenuRunning = false;
                    break;
            }
        }
    }

    internal static void CategoriesMenu()
    {
        var isCategoryMenuRunning = true;
        while (isCategoryMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<CategoryMenu>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        CategoryMenu.AddCategory,
                        CategoryMenu.DeleteCategory,
                        CategoryMenu.UpdateCategory,
                        CategoryMenu.ViewAllCategories,
                        CategoryMenu.ViewCategory,
                        CategoryMenu.GoBack
                    )
            );

            switch (option)
            {
                case CategoryMenu.AddCategory:
                    CategoryService.InsertCategory();
                    break;
                case CategoryMenu.ViewAllCategories:
                    CategoryService.GetCategories();
                    break;
                case CategoryMenu.ViewCategory:
                    CategoryService.GetCategory();
                    break;
                case CategoryMenu.UpdateCategory:
                    CategoryService.UpdateCategory();
                    break;
                case CategoryMenu.DeleteCategory:
                    CategoryService.DeleteCategory();
                    break;
                default:
                    isCategoryMenuRunning = false;
                    break;
            }
        }
    }

    internal static void ProductsMenu()
    {
        var isProductMenuRunning = true;

        while (isProductMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ProductMenu>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        ProductMenu.AddProduct,
                        ProductMenu.DeleteProduct,
                        ProductMenu.UpdateProduct,
                        ProductMenu.ViewAllProducts,
                        ProductMenu.ViewProduct,
                        ProductMenu.GoBack
                    )
            );

            switch (option)
            {
                case ProductMenu.AddProduct:
                    ProductService.InsertProduct();
                    break;
                case ProductMenu.DeleteProduct:
                    ProductService.DeleteProduct();
                    break;
                case ProductMenu.UpdateProduct:
                    ProductService.UpdateProduct();
                    break;
                case ProductMenu.ViewProduct:
                    ProductService.GetProduct();
                    break;
                case ProductMenu.ViewAllProducts:
                    ProductService.GetProducts();
                    break;
                default:
                    isProductMenuRunning = false;
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
        table.AddColumn("Category");

        foreach (var product in products)
        {
            table.AddRow(
                product.ProductId.ToString(),
                product.Name,
                product.Price.ToString("C"),
                product.Category.Name
            );
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
Name: {product.Name}
Category: {product.Category.Name}"
        )
        {
            Header = new PanelHeader("Product Info"),
            Padding = new Padding(2, 2, 2, 2)
        };

        AnsiConsole.Write(panel);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowCategoryTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var category in categories)
        {
            table.AddRow(category.CategoryId.ToString(), category.Name);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowCategory(Category category)
    {
        var panel = new Panel(
            $@"Id: {category.CategoryId}
Name: {category.Name}
Product Count: {category.Products.Count}"
        );
        panel.Header = new PanelHeader($"{category.Name}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        ShowProductTable(category.Products);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowOrderTable(List<Order> orders)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Date");
        table.AddColumn("Count");
        table.AddColumn("Total Price");

        foreach (Order order in orders)
        {
            table.AddRow(
                order.OrderId.ToString(),
                order.CreatedDate.ToString("g"),
                order.OrderProducts.Sum(x => x.Quantity).ToString(),
                order.TotalPrice.ToString("C")
            );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowOrder(Order order)
    {
        var panel = new Panel(
            $@"Id: {order.OrderId}
Date: {order.CreatedDate:g}
Product Count: {order.OrderProducts.Sum(x => x.Quantity).ToString()}"
        );
        panel.Header = new PanelHeader($"Order #{order.OrderId}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
    }

    public static void ShowProductForOrderTable(List<ProductForOrderViewDTO> products)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Category");
        table.AddColumn("Price");
        table.AddColumn("Quantity");
        table.AddColumn("Total Price");

        foreach (var product in products)
        {
            table.AddRow(
                product.Id.ToString(),
                product.Name,
                product.CategoryName,
                product.Price.ToString("C"),
                product.Quantity.ToString(),
                product.TotalPrice.ToString("C")
            );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowReportByMonth(List<MonthlyReportDTO> report)
    {
        var table = new Table();
        table.AddColumn("Month");
        table.AddColumn("Total Quantity Sold");
        table.AddColumn("Total Sales    ");

        foreach (var item in report)
        {
            table.AddRow(item.Month, item.TotalQuantity.ToString(), item.TotalPrice.ToString("C"));
        }

        AnsiConsole.Write(table);
    }
}
