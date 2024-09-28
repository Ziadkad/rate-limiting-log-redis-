using CI_CD_Pipelines.Entities;

namespace CI_CD_Pipelines.Data;

public static class Data
{
    public static List<Product> Products { get; } = new()
    {
        new Product { Id = 1, ProductName = "Apple iPhone 13", Price = 999 },
        new Product { Id = 2, ProductName = "Samsung Galaxy S21", Price = 799 },
        new Product { Id = 3, ProductName = "Google Pixel 6", Price = 599 },
        new Product { Id = 4, ProductName = "OnePlus 9", Price = 729 },
        new Product { Id = 5, ProductName = "Sony WH-1000XM4", Price = 349 },
        new Product { Id = 6, ProductName = "Apple MacBook Pro", Price = 1299 },
        new Product { Id = 7, ProductName = "Dell XPS 13", Price = 999 },
        new Product { Id = 8, ProductName = "HP Spectre x360", Price = 1199 },
        new Product { Id = 9, ProductName = "Microsoft Surface Laptop 4", Price = 999 },
        new Product { Id = 10, ProductName = "Nintendo Switch", Price = 299 },
        new Product { Id = 11, ProductName = "Asus ROG Strix", Price = 1499 },
        new Product { Id = 12, ProductName = "Sony PlayStation 5", Price = 499 },
        new Product { Id = 13, ProductName = "Xbox Series X", Price = 499 },
        new Product { Id = 14, ProductName = "Apple iPad Pro", Price = 799 },
        new Product { Id = 15, ProductName = "Samsung Galaxy Tab S7", Price = 649 },
        new Product { Id = 16, ProductName = "Kindle Paperwhite", Price = 129 },
        new Product { Id = 17, ProductName = "Fitbit Charge 5", Price = 179 },
        new Product { Id = 18, ProductName = "Garmin Fenix 6", Price = 699 },
        new Product { Id = 19, ProductName = "Bose QuietComfort 35 II", Price = 299 },
        new Product { Id = 20, ProductName = "JBL Flip 5", Price = 119 }
    };
}