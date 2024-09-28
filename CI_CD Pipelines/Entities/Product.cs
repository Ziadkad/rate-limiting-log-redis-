using System.Text.Json.Serialization;

namespace CI_CD_Pipelines.Entities;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}