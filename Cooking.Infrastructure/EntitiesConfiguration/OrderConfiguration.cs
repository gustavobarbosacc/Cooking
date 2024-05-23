using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Cooking.Domain.Carts;
using Cooking.Domain.Orders;
using Cooking.Domain.Products;
using Cooking.Domain.Stores;

namespace Cooking.Infrastructure.EntitiesConfiguration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Items)
            .IsRequired()
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<Item>>(v)!)
            .Metadata.SetValueComparer(GetValueComparer<List<Item>>());

        builder.Property(x => x.Delivery)
            .IsRequired()
            .HasConversion<short>();

        builder.Property(x => x.Address)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Address>(v)!)
            .Metadata.SetValueComparer(GetValueComparer<Address>());

        builder.Property(x => x.Payment)
            .IsRequired()
            .HasConversion<short>();

        builder.Property(x => x.ChangeOfmoney);

        builder.Property(product => product.Accepted)
            .IsRequired();

        builder.Property(e => e.CreatedOnUtc).IsRequired();
        builder.Property(e => e.CancelOnUtc);
        builder.Property(e => e.UpdatedOnUtc);
        builder.Property(e => e.RemoveOnUtc);
    }

    private static ValueComparer<T> GetValueComparer<T>()
        => new(
            (c1, c2) => JsonConvert.SerializeObject(c1) == JsonConvert.SerializeObject(c2),
            c => c!.GetHashCode(),
            c => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(c))!);
}
