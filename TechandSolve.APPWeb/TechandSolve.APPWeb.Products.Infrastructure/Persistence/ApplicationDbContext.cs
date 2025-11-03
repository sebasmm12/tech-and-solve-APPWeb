using Microsoft.EntityFrameworkCore;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Infrastructure.Persistence;

public class ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}