using FluentValidation;
using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Application.Products.Validators;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateValidator(IProductsRepository productsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("El id debe ser mayor a cero")
            .MustAsync(async (id, _) => await productsRepository.ExistsAsync(id))
            .WithMessage("El producto no existe");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre no puede exceder los 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("La descripción es requerida")
            .MaximumLength(500)
            .WithMessage("La descripción no puede exceder los 500 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("El precio debe ser mayor a cero");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El stock debe ser mayor o igual a cero");
    }
}