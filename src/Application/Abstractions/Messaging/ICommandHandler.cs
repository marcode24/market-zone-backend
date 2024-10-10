using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand>
: IRequestHandler<TCommand, Result>
  where TCommand : ICommand<Result>
{ }

public interface ICommandHandler<TCommand, TResponse>
: IRequestHandler<TCommand, Result<TResponse>>
  where TCommand : ICommand<TResponse>
{ }
