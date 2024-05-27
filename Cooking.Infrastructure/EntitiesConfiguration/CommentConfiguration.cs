using Cooking.Domain.Comments;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace Cooking.Infrastructure.EntitiesConfiguration;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RecipeId)
            .IsRequired();

        builder.Property(x => x.Remark).IsRequired();

        builder.Property(e => e.CreatedOnUtc).IsRequired();
        builder.Property(e => e.UpdatedOnUtc);
        builder.Property(e => e.RemoveOnUtc);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Recipe>()
            .WithMany()
            .HasForeignKey(x => x.RecipeId);
    }
}