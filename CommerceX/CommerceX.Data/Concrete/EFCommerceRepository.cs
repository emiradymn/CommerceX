using CommerceX.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CommerceX.Data.Concrete;

public class EFCommerceRepository : ICommerceRepository
{
    private ComDbContext _context;

    public EFCommerceRepository(ComDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Category> Categories => _context.Categories;
    public void CreateProduct(Product entity)
    {
        throw new NotImplementedException();
    }

    public int GetProductCount(string category)
    {
        return category == null
         ? Products.Count()
         : Products.Include(p => p.Categories)
          .Where(p => p.Categories.Any(a => a.Url == category)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
    {
        var products = Products;

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == category));
        }

        return products.Skip((page - 1) * pageSize).Take(pageSize);


    }
}