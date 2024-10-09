using AutoMapper;
using CommerceX.Data.Abstract;
using CommerceX.Data.Concrete;
using CommerceX.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceX.Web.Controllers;

public class HomeController : Controller
{
    public int pageSize = 3;

    private readonly ICommerceRepository _commerceRepository;
    private readonly IMapper _mapper;
    public HomeController(ICommerceRepository commerceRepository, IMapper mapper)
    {
        _commerceRepository = commerceRepository;
        _mapper = mapper;
    }
    public IActionResult Index(string category, int page = 1)
    {

        var query = _commerceRepository.Products;

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == category));
        }

        query = query.Skip((page - 1) * pageSize);

        return View(new ProductListViewModel
        {
            Products = _commerceRepository.GetProductsByCategory(category, page, pageSize)
                .Select(product => _mapper.Map<ProductViewModel>(product)),

            PageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                CurrentPage = page,
                TotalItems = _commerceRepository.GetProductCount(category)
            }
        });
    }

}
