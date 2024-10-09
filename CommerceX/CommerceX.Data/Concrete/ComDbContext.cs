using CommerceX.Data.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CommerceX.Data.Concrete;

public class ComDbContext : DbContext
{

    public ComDbContext(DbContextOptions<ComDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>() //many-to-many  iki entity  arasında bir ilişki kurar 
            .HasMany(e => e.Categories)
            .WithMany(e => e.Products)
            .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()  // url i benzersiz yapmak için 
            .HasIndex(u => u.Url)
            .IsUnique();

        modelBuilder.Entity<Product>().HasData(
            new List<Product>() {
                new()  { Id = 1, Name = "Product 1", Price= 1200, Description="Önemli bir vitamin"},
                new()  { Id = 2, Name = "Product 2", Price= 200, Description="Önemli bir vitamin" },
                new()  { Id = 3, Name = "Product 3", Price= 2000, Description="Önemli bir vitamin"},
                new()  { Id = 4, Name = "Product 4", Price= 300, Description="Önemli bir vitamin" },
                new()  { Id = 5, Name = "Product 5", Price= 6000, Description="Önemli bir vitamin"},
                new()  { Id = 6, Name = "Product 6", Price= 400, Description="Önemli bir vitamin" },
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>() {
                new() { Id=1, Name="Vitamin", Url="vitamin"},
                new() { Id=2, Name="Protein Tozu", Url="protein-tozu"},
                new() { Id=3, Name="Gainer", Url="gainer"}
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>() {
                new() { ProductId = 1, CategoryId = 1 },
                new() { ProductId = 2, CategoryId = 3 },
                new() { ProductId = 3, CategoryId = 2 },
                new() { ProductId = 4, CategoryId = 1 },
                new() { ProductId = 5, CategoryId = 2 },
                new() { ProductId = 6, CategoryId = 3 },
            }
        );
    }
}