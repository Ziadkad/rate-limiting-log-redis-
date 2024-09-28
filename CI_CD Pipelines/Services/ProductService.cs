using CI_CD_Pipelines.Entities;
using CI_CD_Pipelines.Exceptions;
using CI_CD_Pipelines.Services.Interfaces;

namespace CI_CD_Pipelines.Services;

public class ProductService : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        await Task.Delay(30000);
        return Data.Data.Products;
    }
    public Product? GetOneProduct(int id)
    {
        return Data.Data.Products.Find(p=>p.Id == id);
    }
    public Product AddProduct(Product product)
    {
        if (Data.Data.Products.Any())
        {
            int maxId = Data.Data.Products.Max(p => p.Id);
            product.Id = maxId + 1;
        }
        else
        { 
            product.Id = 1;
        }
        Data.Data.Products.Add(product);
        return product;
    }
    public void DeleteProduct(Product product)
    {
        Data.Data.Products.Remove(product);
    }
    public Product? UpdateProduct(Product product)
    {
        int index = Data.Data.Products.FindIndex(p => p.Id == product.Id);
    
        if (index != -1)
        {
            Data.Data.Products[index] = product;
            return product;
        }
        throw new ProductNotFoundException(product.Id);
    }
}