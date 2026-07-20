using MediatR;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.Core.Application.Messaging.CQRS;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> 
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : ICommand<TResponse>;
