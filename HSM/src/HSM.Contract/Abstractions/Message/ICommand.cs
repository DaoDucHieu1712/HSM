using HSM.Contract.Abstractions.Shared;
using MediatR;

namespace HSM.Contract.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
