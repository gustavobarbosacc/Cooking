using Cooking.Domain.Abstractions;

namespace Cooking.Domain.Categories;

public class CategoryErrors
{
    public static readonly Error NotFound = new(
       "Category.NotFound",
       "The Category with the specified identifier was not found");
}
