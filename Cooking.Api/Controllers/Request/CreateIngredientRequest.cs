using Cooking.Domain.Ingredients;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cooking.Api.Controllers.Request;

public class CreateIngredientRequest
{
    public Guid ProductId { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Measure measure { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }    
}