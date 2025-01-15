using Application.Core.Responses;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand>
: IRequestHandler<TCommand, Response>
  where TCommand : ICommand<Response>
{ }

public interface ICommandHandler<TCommand, TResponse>
: IRequestHandler<TCommand, Response<TResponse>>
  where TCommand : ICommand<TResponse>
{ }
