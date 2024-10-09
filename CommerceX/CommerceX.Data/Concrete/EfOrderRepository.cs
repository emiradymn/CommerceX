
namespace CommerceX.Data.Concrete;

public class EfOrderRepository : IOrderRepository
{

    private ComDbContext _context;
    public EfOrderRepository(ComDbContext context)
    {
        _context = context;
    }
    public IQueryable<Order> Orders => _context.Orders;

    public void SaveOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
}
