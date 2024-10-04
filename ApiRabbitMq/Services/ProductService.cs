using ApiRabbitMq.Context;
using ApiRabbitMq.Models;

namespace ApiRabbitMq.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext dbContext)
    {
        _context = dbContext;
    }
    public IEnumerable<Product> GetProductList()
    {
        return _context.Products.ToList();
    }
    public Product GetProductById(int id)
    {
        return _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
    }
    public Product AddProduct(Product product)
    {
        var result = _context.Products.Add(product);
        _context.SaveChanges();
        return result.Entity;
    }
    public Product UpdateProduct(Product product)
    {
        var result = _context.Products.Update(product);
        _context.SaveChanges();
        return result.Entity;
    }
    public bool DeleteProduct(int Id)
    {
        var filteredData = _context.Products.Where(x => x.ProductId == Id).FirstOrDefault();
        var result = _context.Remove(filteredData);
        _context.SaveChanges();
        return result != null ? true : false;
    }
}
