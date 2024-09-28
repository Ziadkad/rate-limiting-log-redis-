using CI_CD_Pipelines.Entities;
using CI_CD_Pipelines.Exceptions;
using CI_CD_Pipelines.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CI_CD_Pipelines.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly HttpClient _client;
    private readonly IDatabase _redis;
    private const string ProductCacheKey = "Product_";
    private const string AllProductsCacheKey = "AllProducts";
    public ProductController(IProductService productService, HttpClient client, IConnectionMultiplexer muxer)
    {
        _productService = productService;
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TestApp", "1.0"));
        _redis = muxer.GetDatabase();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
   
        string? cachedProducts = _redis.StringGet(AllProductsCacheKey);
        if (!string.IsNullOrEmpty(cachedProducts))
        {
            // Deserialize and return cached data
            return Ok(JsonSerializer.Deserialize<List<Product>>(cachedProducts));
        }

        // If not in cache, get from the service
        List<Product> products = await _productService.GetAllProducts();

        // Cache the data with an expiration time of 5 minutes
        _redis.StringSet(AllProductsCacheKey, JsonSerializer.Serialize(products), TimeSpan.FromMinutes(5));

        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        string? cachedProduct = _redis.StringGet(ProductCacheKey + id);
        if (!string.IsNullOrEmpty(cachedProduct))
        {
            // Deserialize and return cached data
            return Ok(JsonSerializer.Deserialize<Product>(cachedProduct));
        }

        // If not in cache, get from the service
        Product? product = _productService.GetOneProduct(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with ID {id} not found." });
        }

        // Cache the data with an expiration time of 5 minutes
        _redis.StringSet(ProductCacheKey + id, JsonSerializer.Serialize(product), TimeSpan.FromMinutes(5));

        return Ok(product);
    }

    
    [HttpPost]
    public ActionResult<Product> AddProduct([FromBody] Product product)
    {
        if (product.Id != 0)
        {
            return BadRequest("When Adding A Product, Id should be 0");
        }
        Product addedProduct = _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
    }
    
    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest(new { Message = "Product ID mismatch." });
        }
        try
        {
            Product? updatedProduct = _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var product = _productService.GetOneProduct(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with ID {id} not found." });
        }
        
        _productService.DeleteProduct(product);
        return NoContent();
    }
    
    
}