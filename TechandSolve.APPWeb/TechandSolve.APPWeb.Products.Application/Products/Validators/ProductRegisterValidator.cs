using FluentValidation;
using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Domain.Products;

namespace TechAndSolve.APPWeb.Products.Application.Products.Validators;

public class ProductRegisterValidator : AbstractValidator<ProductRegisterRequest>
{
    public ProductRegisterValidator(IProductsRepository productsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre no puede exceder los 100 caracteres")
            .MustAsync(async (name, _) =>
            {
                var product = await productsRepository.GetByNameAsync(name);

                return product is null;
            })
            .WithMessage("El nombre ya existe");

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