using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Messaging;
using Cooking.Application.Users.Create;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Users;

namespace Cooking.Application.Users.Delete;

internal class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<DeleteUserCommand>
{
    public readonly IUserRepository _userRepository = userRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user =  await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        user.RemoveOn(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
