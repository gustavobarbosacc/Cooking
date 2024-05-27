using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Domain.Measures;

public class Measure(

    string unit,
    double quantity,
    DateTime CreatedOnUtc)
{ 
    public string Unit { get; set; } = unit;
    public double Quantity { get; set; } = quantity;



    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Measure Create(string unit, double quantity, DateTime CreatedOnUtc) 
        => new(unit, quantity, CreatedOnUtc);

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }

}