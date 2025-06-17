using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;
using WpfApp1.Data;

namespace WpfApp1.Services;

public class ProductService(ApplicationDbContext context) : IProductService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().OrderBy(p => p.CreatedDate).ToListAsync();
    }

    public async Task<Product?> GetProductByUidAsync(Guid uid)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Uid == uid);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product.Uid == Guid.Empty)
        {
            product.Uid = Guid.NewGuid();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Uid)
            ?? throw new InvalidOperationException($"Товар с UID {product.Uid} не найден");

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;

        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(Guid uid)
    {
        var product = await _context.Products.FindAsync(uid);

        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}
