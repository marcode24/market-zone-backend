using Domain.Abstractions;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<IQuery, TResponse>
  : IRequestHandler<IQuery, Result<TResponse>>
  where IQuery : IQuery<TResponse>
{ }
