using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;

namespace Cooking.Application.Categories.Delete;

internal class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteCategoryCommand>
{
    public readonly ICategoryRepository _categoryRepository = categoryRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var user =  await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        user.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
