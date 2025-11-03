namespace TechAndSolve.APPWeb.Products.Domain.Bases;

public interface IEntityBase
{
    int Id { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime? UpdatedAt { get; set; }

    bool IsDeleted { get; set; }
}