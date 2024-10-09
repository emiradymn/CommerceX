using CommerceX.Data.Abstract;
using CommerceX.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceX.Web.Components;

public class CategoriesListViewComponent : ViewComponent
{
    private ICommerceRepository _commerceRepository;

    public CategoriesListViewComponent(ICommerceRepository commerceRepository)
    {
        _commerceRepository = commerceRepository;
    }
    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedCategory = RouteData?.Values["category"];

        return View(_commerceRepository.Categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Url = c.Url
        }).ToList());
    }
}
