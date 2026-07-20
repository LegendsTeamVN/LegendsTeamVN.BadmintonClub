using MediatR;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.Core.Application.Messaging.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
