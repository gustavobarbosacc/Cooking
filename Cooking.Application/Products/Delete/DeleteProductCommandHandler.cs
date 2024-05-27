using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Application.Products.Delete;

internal class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteProductCommand>
{
    public readonly IProductRepository _productRepository = productRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var user =  await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        user.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
