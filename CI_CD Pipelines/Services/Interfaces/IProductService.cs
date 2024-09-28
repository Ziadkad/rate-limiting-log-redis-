using CI_CD_Pipelines.Entities;

namespace CI_CD_Pipelines.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Product? GetOneProduct(int id);
    Product AddProduct(Product product);
    void DeleteProduct(Product product);
    Product? UpdateProduct(Product product);
}