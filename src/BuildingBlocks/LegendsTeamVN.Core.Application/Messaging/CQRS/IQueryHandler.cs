using MediatR;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.Core.Application.Messaging.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
