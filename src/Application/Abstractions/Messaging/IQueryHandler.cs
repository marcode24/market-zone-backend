using Application.Core.Responses;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<IQuery, TResponse>
  : IRequestHandler<IQuery, Response<TResponse>>
  where IQuery : IQuery<TResponse>
{ }
