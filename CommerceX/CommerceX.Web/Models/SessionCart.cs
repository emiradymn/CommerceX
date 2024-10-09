using System;
using System.Text.Json.Serialization;
using CommerceX.Data.Concrete;
using CommerceX.Web.Heplers;

namespace CommerceX.Web.Models;

public class SessionCart : Cart
{

    public static Cart GetCart(IServiceProvider service)
    {
        ISession? session = service.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }
    override public void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        Session?.SetJson("Cart", this);
    }

    override public void RemoveItem(Product product)
    {
        base.RemoveItem(product);
        Session?.SetJson("Cart", this);
    }

    override public void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }



}
