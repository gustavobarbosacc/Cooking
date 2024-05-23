using Cooking.Domain.Abstraction;
using Cooking.Domain.Products;

namespace Cooking.Domain.Ingredients;

public class Ingredient : Entity
{
    public string Name { get; set; }
    public Product Product { get; set; }
    public Measure Measure { get; set; }

    public Ingredient(Guid id, string name, Product product, Measure measure) : base(id)
    {
        Name = name;
        Product = product;
        Measure = measure;
    }

    public static Ingredient Create(string name, Product product, Measure measure)
        => new(Guid.NewGuid(), name, product, measure);   
}
