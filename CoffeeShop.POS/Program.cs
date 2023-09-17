using CoffeeShop.POS;

var context = new ProductsContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

UserInterface.MainMenu();
