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

    internal static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        CategoryController.DeleteCategory(category);
    }

    internal static void GetCategories()
    {
        var categories = CategoryController.GetCategories();
        UserInterface.ShowCategoryTable(categories);
    }

    internal static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        

        category.Name = AnsiConsole.Confirm("Would you like to update the name of the category?")
            ? AnsiConsole.Ask<string>("What is the new name of the category?")
            : category.Name;
        CategoryController.UpdateCategory(category);
    }

    internal static void GetCategory()
    {
        var category = GetCategoryOptionInput();
        UserInterface.ShowCategory(category);
    }

    internal static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();
        var categoriesArray = categories.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Choose category").AddChoices(categoriesArray)
        );

        var category = categories.Single(x => x.Name == option);

        return category;
    }
}
