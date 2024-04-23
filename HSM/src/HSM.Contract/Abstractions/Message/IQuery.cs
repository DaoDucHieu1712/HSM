using HSM.Contract.Abstractions.Shared;
using MediatR;

namespace HSM.Contract.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
