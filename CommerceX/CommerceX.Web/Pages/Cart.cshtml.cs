using System.Windows.Input;
using CommerceX.Data.Abstract;
using CommerceX.Web.Heplers;
using CommerceX.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceX.Web.Pages
{
    public class CartModel : PageModel
    {
        private ICommerceRepository _repository;

        public CartModel(ICommerceRepository repository, Cart cartService)
        {
            _repository = repository;
            Cart = cartService;
        }

        public Cart? Cart { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int Id)
        {
            var product = _repository.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                Cart?.AddItem(product, 1);
            }
            return RedirectToPage("/");
        }

        public IActionResult OnPostRemove(int Id)
        {
            Cart?.RemoveItem(Cart.Items.First(p => p.Product.Id == Id).Product);
            return RedirectToPage("/Cart");

        }
    }
}
