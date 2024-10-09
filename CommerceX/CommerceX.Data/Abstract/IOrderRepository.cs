using CommerceX.Data.Concrete;

public interface IOrderRepository
{
    IQueryable<Order> Orders { get; }

    void SaveOrder(Order order);
}