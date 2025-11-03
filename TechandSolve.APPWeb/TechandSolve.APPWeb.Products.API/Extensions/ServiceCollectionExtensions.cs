using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TechAndSolve.APPWeb.Products.Application.Products.Services;
using TechAndSolve.APPWeb.Products.Application.Products.Validators;
using TechAndSolve.APPWeb.Products.Domain.Products;
using TechAndSolve.APPWeb.Products.Infrastructure.Persistence;
using TechAndSolve.APPWeb.Products.Infrastructure.Persistence.Repositories.Products;

namespace TechAndSolve.APPWeb.Products.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scanner => scanner
            .FromAssemblies(
                typeof(ProductsRepository).Assembly)
            .AddClasses()
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddServices (this IServiceCollection services)
    {
        services.Scan(scanner => scanner
            .FromAssemblies(
                typeof(IProductsService).Assembly)
            .AddClasses()
            .AsMatchingInterface()
            .WithTransientLifetime());

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.Scan(scanner => scanner
            .FromAssemblies(
                typeof(ProductRegisterValidator).Assembly)
            .AddClasses(c =>
                c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }
}