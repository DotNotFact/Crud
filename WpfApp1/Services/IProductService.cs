using WpfApp1.Models;

namespace WpfApp1.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByUidAsync(Guid uid);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid uid);
}
