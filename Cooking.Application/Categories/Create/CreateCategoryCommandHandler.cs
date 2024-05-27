using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;

namespace Cooking.Application.Categories.Create;

internal class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public readonly ICategoryRepository _categoryRepository = categoryRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(
            request.UserId,
            request.Name,
            _dateTimeProvider.UtcNow);

        _categoryRepository.Add(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
