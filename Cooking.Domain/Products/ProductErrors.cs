using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Products;

public class ProductErrors
{
    public static readonly Error NotFound = new(
       "Product.NotFound",
       "The Product with the specified identifier was not found");
}
