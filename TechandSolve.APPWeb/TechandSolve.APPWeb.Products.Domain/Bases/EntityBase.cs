namespace TechAndSolve.APPWeb.Products.Domain.Bases;

public class EntityBase : IEntityBase
{
    public int Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}