using Microsoft.EntityFrameworkCore;
using BulkyBook.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BulkyBook.DataAccess;

public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options ) : base( options )
    {

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public DbSet<OrderHeader> OrderHeader { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }
}
