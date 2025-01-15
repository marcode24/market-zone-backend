namespace Application.Abstractions.Messaging;

using Application.Core.Responses;
using Domain.Abstractions;
using MediatR;

public interface IQuery<TResponse>
: IRequest<Response<TResponse>>, IBaseQuery
{ }

public interface IBaseQuery { }
