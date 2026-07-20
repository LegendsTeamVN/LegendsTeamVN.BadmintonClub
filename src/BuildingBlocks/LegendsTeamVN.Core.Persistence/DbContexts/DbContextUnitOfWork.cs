using System.Data;
using LegendsTeamVN.Core.Domain.Aggregates;
using LegendsTeamVN.Core.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LegendsTeamVN.Core.Persistence.DbContexts;

public class DbContextUnitOfWork<TDbContext>(DbContextOptions<TDbContext> options, IPublisher publisher) : DbContext(options), IUnitOfWork
    where TDbContext : DbContext
{
    private IDbContextTransaction? _dbContextTransaction;
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction != null)
        {
            return _dbContextTransaction;
        }

        _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return _dbContextTransaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        await _dbContextTransaction.CommitAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
        _dbContextTransaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        await _dbContextTransaction.RollbackAsync(cancellationToken);
        await _dbContextTransaction.DisposeAsync();
        _dbContextTransaction = null;
    }
}
