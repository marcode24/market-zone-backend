using Application.Core.Responses;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommand : IRequest<Response>, IBaseCommand { }

public interface ICommand<TResponse> : IRequest<Response<TResponse>>, IBaseCommand { }

public interface IBaseCommand { }
