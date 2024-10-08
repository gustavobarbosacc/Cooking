﻿using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Ingredients;

namespace Cooking.Domain.Recipes;

public class Recipe(
    Guid id,
    Guid userId,
    Guid categoryId,
    string title,
    string preparationMethod,
    int level,
    List<Ingredient> ingredients,
    Rating rating,
    int preparationTime,
    DateTime CreatedOnUtc) : Entity(id)
{
    public Guid UserId { get; set; } = userId;
    public Guid CategoryId { get; set; } = categoryId;
    public string Title { get; set; } = title;
    public string PreparationMethod { get; set; } = preparationMethod;
    public int Level { get; set; } = level;
    public List<Ingredient> Ingredients { get; set; } = ingredients;
    public Rating Rating { get; set; } = rating;
    public int PreparationTime { get; set; } = preparationTime;

    public DateTime CreatedOnUtc { get; internal set; } = CreatedOnUtc;
    public DateTime? UpdatedOnUtc { get; internal set; }
    public DateTime? RemoveOnUtc { get; internal set; }

    public static Recipe Create(
        Guid userId,
        Category category,
        string title,
        string preparationMethod,
        int level,
        List<Ingredient> ingredients,
        Rating rating,
        int preparationTime,
         DateTime utcNow) => new (
            Guid.NewGuid(),
            userId,
            category.Id,
            title,
            preparationMethod,
            level,
            ingredients,
            rating,
            preparationTime,
            utcNow);

    public Result RemoveOn(DateTime utcNow)
    {
        if (RemoveOnUtc is not null)
        {
            return Result.Failure(RecipeErrors.NotFound);
        }

        RemoveOnUtc = utcNow;

        return Result.Success();
    }
}
