namespace Infrastructure.Database;

using Application.Abstractions.Clock;
using Domain.Abstractions;
using Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

/// <summary>
/// Represents the application database context.
/// </summary>
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
  /// <summary>
  /// Gets the JSON serializer settings.
  /// </summary>
  public static readonly JsonSerializerSettings JsonSerializerSettings = new()
  {
    TypeNameHandling = TypeNameHandling.All,
  };

  private readonly IDateTimeProvider dateTimeProvider;

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
  /// </summary>
  /// <param name="options">The options.</param>
  /// <param name="DateTimeProvider">The date time provider.</param>
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeProvider DateTimeProvider)
    : base(options)
  {
    dateTimeProvider = DateTimeProvider;
  }

  /// <summary>
  /// Saves the changes.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task that represents the asynchronous save operation.</returns>
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

  /// <summary>
  /// Configures the model.
  /// </summary>
  /// <param name="modelBuilder">The model builder.</param>
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
