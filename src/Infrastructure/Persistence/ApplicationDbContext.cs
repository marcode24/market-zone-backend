namespace Infrastructure.Persistence;

using Application.Abstractions.Clock;
using Domain.Abstractions;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
  public static readonly JsonSerializerSettings JsonSerializerSettings = new()
  {
    TypeNameHandling = TypeNameHandling.All,
  };
  private readonly IDateTimeProvider dateTimeProvider;
  private IDbContextTransaction? _transaction;

  public ApplicationDbContext(DbContextOptions options, IDateTimeProvider DateTimeProvider)
    : base(options)
  {
    dateTimeProvider = DateTimeProvider;
  }

  public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_transaction is not null)
      throw new InvalidOperationException("A transaction is already in progress.");

    _transaction = await Database.BeginTransactionAsync(cancellationToken);

    return _transaction;
  }

  public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_transaction is null)
      throw new InvalidOperationException("No transaction to commit.");

    await _transaction.CommitAsync(cancellationToken);
  }

  public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (_transaction is null)
      throw new InvalidOperationException("No transaction to rollback.");

    try
    {
      await _transaction.RollbackAsync(cancellationToken);
    }
    finally
    {
      _transaction.Dispose();
      _transaction = null!;
    }

  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    try
    {
      AddDomainEventsToOutboxMessages();
      var result = await base.SaveChangesAsync(cancellationToken);

      return result;
    }
    catch (DbUpdateConcurrencyException ex)
    {
      throw new Exception("A concurrency error occurred while saving the data.", ex);
    }
  }



  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }

  private void AddDomainEventsToOutboxMessages()
  {
    var outboxMessages = ChangeTracker
      .Entries<IEntity>()
      .Select(e => e.Entity)
      .SelectMany(e =>
      {
        var domainEvents = e.GetDomainEvents();
        e.ClearDomainEvents();
        return domainEvents;
      })
      .Select(domainEvent => new OutboxMessage(
        Guid.NewGuid(),
        dateTimeProvider.CurrentTime,
        domainEvent.GetType().Name,
        JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
      .ToList();

    AddRange(outboxMessages);
  }
}
