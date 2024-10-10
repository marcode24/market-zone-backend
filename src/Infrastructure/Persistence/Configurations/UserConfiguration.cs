namespace Infrastructure.Persistence.Configurations;

using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable(Tables.Users, Schemas.Market);

    builder.HasKey(user => user.Id);

    builder.Property(user => user.Id)
      .ValueGeneratedOnAdd()
      .HasConversion(id => id!.Value, value => new UserId(value));

    builder.Property(user => user.Name)
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Name(value));

    builder.Property(user => user.LastName)
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .HasConversion(user => user!.Value, value => new LastName(value));

    builder.Property(user => user.Email)
      .HasMaxLength(100)
      .HasColumnType("varchar(100)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Email(value));

    builder.Property(user => user.Password)
      .HasMaxLength(100)
      .HasColumnType("varchar(100)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Password(value));

    builder.Property(user => user.Photo)
      .HasMaxLength(100)
      .HasColumnType("varchar(150)")
      .HasDefaultValue(null)
      .HasConversion(user => user!.Value, value => new Photo(value));

    builder.Property(user => user.IsActive)
      .HasColumnType("boolean")
      .HasDefaultValue(true);

    builder.Property(user => user.IsDeleted)
      .HasColumnType("boolean")
      .HasDefaultValue(false);

    builder.Property(user => user.CreatedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder.Property(user => user.UpdatedAt)
      .HasColumnType("timestamptz")
      .ValueGeneratedOnAddOrUpdate()
      .IsConcurrencyToken();

    builder.Property(user => user.DeletedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValue(null);

    builder.HasIndex(user => user.Email)
      .IsUnique();

    builder.HasOne(user => user.Role)
      .WithMany()
      .HasForeignKey(user => user.RoleId)
      .IsRequired();

    builder.HasQueryFilter(user => !user.IsDeleted);
  }
}
