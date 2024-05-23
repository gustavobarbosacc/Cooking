using Cooking.Domain.Abstractions;
using MediatR;
namespace Cooking.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }