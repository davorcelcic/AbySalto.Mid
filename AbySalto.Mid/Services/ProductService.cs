using AbySalto.Mid.WebApi.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AbySalto.Mid.WebApi.Services
{
    public class ProductService
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;

        public ProductService(IMemoryCache cache, HttpClient httpClient)
        {
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<ProductResponse> GetProductsAsync()
        {
            string cacheKey = $"products_all";
            return await GetProductsStringAsync($"https://dummyjson.com/products", cacheKey);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            string cacheKey = $"products_{productId}";
            string link = $"https://dummyjson.com/products/{productId}";

            Product? product;
            if (!_cache.TryGetValue(cacheKey, out product))
            {
                var response = await _httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var jsonDocument = JsonDocument.Parse(jsonString);
                product = JsonSerializer.Deserialize<Product>(jsonString, options) ?? new Product();

                _cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
            }
            return product;
        }

        private async Task<ProductResponse> GetProductsStringAsync(string link, string cacheKey)
        {
            ProductResponse? productResponse;
            if (!_cache.TryGetValue(cacheKey, out productResponse))
            {
                var response = await _httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var jsonDocument = JsonDocument.Parse(jsonString);
                productResponse = JsonSerializer.Deserialize<ProductResponse>(jsonString, options) ?? new ProductResponse();
                _cache.Set(cacheKey, productResponse, TimeSpan.FromMinutes(10));
            }
            return productResponse;
        }
    }
}
