using CoffeeShop.POS.Controllers;
using CoffeeShop.POS.Models;
using Spectre.Console;

namespace CoffeeShop.POS.Services;

public class CategoryService
{
    internal static void InsertCategory()
    {
        var category = new Category();
        category.Name = AnsiConsole.Ask<string>("What is the name of the category?");

        CategoryController.AddCategory(category);
    }

    internal static void GetCategories()
    {
        var categories = CategoryController.GetCategories();
        UserInterface.ShowCategoryTable(categories);
    }
}
