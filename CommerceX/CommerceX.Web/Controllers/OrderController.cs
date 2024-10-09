using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CommerceX.Data.Concrete;
using CommerceX.Web.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CommerceX.Web.Controllers;

public class OrderController : Controller
{
    private Cart cart;
    private IOrderRepository _orderRepository;

    public OrderController(Cart cartService, IOrderRepository orderRepository)
    {
        cart = cartService;
        _orderRepository = orderRepository;

    }

    public IActionResult Checkout()
    {
        return View(new OrderModel() { Cart = cart });
    }
    [HttpPost]
    public IActionResult Checkout(OrderModel model)
    {
        if (cart.Items.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
        }

        if (ModelState.IsValid)
        {
            var order = new Order
            {
                Name = model.Name,
                City = model.City,
                Phone = model.Phone,
                Email = model.Email,
                AdressLine = model.AdressLine,
                OrderData = DateTime.Now,
                OrderItems = cart.Items.Select(i => new CommerceX.Data.Concrete.OrderItem
                {
                    Id = i.Product.Id,
                    Price = (double)i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            model.Cart = cart;
            var payment = ProcessPayment(model);
            if (payment.Status == "success")
            {
                _orderRepository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Complated", new { OrderId = order.Id });
            }

            model.Cart = cart;
            return View(model);
        }

        else
        {
            model.Cart = cart;
            return View(model);
        }
    }
    private Payment ProcessPayment(OrderModel model)
    {
        Options options = new Options();
        options.ApiKey = "your_api_key";
        options.SecretKey = "your_secret_key";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = new Random().Next(111111111, 999999999).ToString();
        request.Price = model?.Cart?.CalculateTotal().ToString();
        request.PaidPrice = model?.Cart?.CalculateTotal().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model?.CartName;
        paymentCard.CardNumber = model?.CartNumber;
        paymentCard.ExpireMonth = model?.ExpirationMonth;
        paymentCard.ExpireYear = model?.ExpirationYear;
        paymentCard.Cvc = model?.Cvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "Emir";
        buyer.Surname = "Adıyaman";
        buyer.GsmNumber = "+905350000000";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = "Emir Adıyaman";
        shippingAddress.City = "";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;


        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in model?.Cart?.Items ?? Enumerable.Empty<CartItem>())
        {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = item.Product.Id.ToString();
            firstBasketItem.Name = item.Product.Name;
            firstBasketItem.Category1 = "Vitamin";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = item.Product.Price.ToString();
            basketItems.Add(firstBasketItem);
        }

        Payment payment = Payment.Create(request, options);

        return payment;
    }
}