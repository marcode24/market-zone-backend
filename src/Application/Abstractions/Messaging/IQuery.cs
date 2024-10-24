namespace Application.Abstractions.Messaging;

using Domain.Abstractions;
using MediatR;

public interface IQuery<TResponse>
: IRequest<Result<TResponse>>, IBaseQuery
{ }

public interface IBaseQuery { }
