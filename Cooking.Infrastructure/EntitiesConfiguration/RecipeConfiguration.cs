using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Cooking.Infrastructure.EntitiesConfiguration;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("recipes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.PreparationMethod)
            .IsRequired();

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired()
            .HasConversion<short>();

        builder.Property(x => x.Ingredients)
            .IsRequired()
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<Ingredient>>(v)!)
            .Metadata.SetValueComparer(GetValueComparer<List<Ingredient>>());

        builder.Property(e => e.CreatedOnUtc).IsRequired();
        builder.Property(e => e.UpdatedOnUtc);
        builder.Property(e => e.RemoveOnUtc);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId);
    }

    private static ValueComparer<T> GetValueComparer<T>()
        => new(
            (c1, c2) => JsonConvert.SerializeObject(c1) == JsonConvert.SerializeObject(c2),
            c => c!.GetHashCode(),
            c => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(c))!);
}
