namespace Cooking.Application.Products.Find;

public class ProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedOnUtc { get; init; }
}
