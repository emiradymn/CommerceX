using CommerceX.Data.Concrete;

namespace CommerceX.Data.Abstract;

public interface ICommerceRepository
{
    IQueryable<Product> Products { get; }
    IQueryable<Category> Categories { get; }

    void CreateProduct(Product entity);
    int GetProductCount(string category);

    IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize);

}