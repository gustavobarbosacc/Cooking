using Cooking.Domain.Ingredients;
using Cooking.Domain.Products;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooking.Infrastructure.EntitiesConfiguration;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("ingredients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Measure)
            .IsRequired()
            .HasConversion<short>();

        // relacoes 

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
