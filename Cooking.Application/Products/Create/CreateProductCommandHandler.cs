using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Products;

namespace Cooking.Application.Products.Create;

internal class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateProductCommand, Guid>
{
    public readonly IProductRepository _productRepository = productRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            request.UserId,
            request.Name,
            _dateTimeProvider.UtcNow);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
