namespace Infrastructure.Persistence.Configurations;

using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
  public void Configure(EntityTypeBuilder<OutboxMessage> builder)
  {
    builder.ToTable(Tables.OutboxMessages, Schemas.Outbox);
    builder.HasKey(e => e.Id);

    builder.Property(o => o.Content)
      .HasColumnType("jsonb");

    builder.Property(om => om.OccurredOnUtc)
        .IsRequired();

    builder.Property(om => om.Type)
        .HasMaxLength(200)
        .IsRequired(false);

    builder.Property(om => om.Content)
        .HasColumnType("text")
        .IsRequired(false);

    builder.Property(om => om.ProcessedOnUtc)
        .IsRequired();

    builder.HasData(
      new { Id = Guid.NewGuid(), OccurredOnUtc = DateTime.UtcNow, Type = "Type", Content = "Content", ProcessedOnUtc = DateTime.UtcNow }
    );
  }
}
