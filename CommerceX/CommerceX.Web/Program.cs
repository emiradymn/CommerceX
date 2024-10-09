using CommerceX.Data.Abstract;
using CommerceX.Data.Concrete;
using CommerceX.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<ComDbContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("mysql_connection");
    var version = new MySqlServerVersion(new Version(8, 0, 39));

    options.UseMySql(connectionString, version,
        mysqlOptions => mysqlOptions.MigrationsAssembly("CommerceX.Web"));
});


builder.Services.AddScoped<ICommerceRepository, EFCommerceRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sc => SessionCart.GetCart(sc));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

// kategori ürün listesi
app.MapControllerRoute("products_in_category", "products/{category?}", new { controller = "Home", action = "Index" });

//ürün detay
app.MapControllerRoute("product_details", "{name}", new { controller = "Home", action = "Details" });


app.MapDefaultControllerRoute();
app.MapRazorPages();


app.Run();
