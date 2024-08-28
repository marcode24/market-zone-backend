namespace Infrastructure.Configurations;

using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Domain.Shared.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// User configuration class.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  /// <summary>
  /// Configure user entity.
  /// </summary>
  /// <param name="builder">Entity type builder.</param>
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable(Tables.Users, Schemas.Market);

    builder.HasKey(user => user.Id);

    builder.Property(user => user.Id)
      .HasConversion(id => id!.Value, value => new UserId(value))
      .ValueGeneratedOnAdd();

    builder.Property(user => user.Name)
      .HasColumnName("name")
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Name(value));

    builder.Property(user => user.LastName)
      .HasColumnName("last_name")
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new LastName(value));

    builder.Property(user => user.Email)
      .HasColumnName("email")
      .HasMaxLength(100)
      .HasColumnType("varchar(100)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Email(value));

    builder.Property(user => user.Password)
      .HasColumnName("password")
      .HasMaxLength(100)
      .HasColumnType("varchar(100)")
      .IsRequired()
      .HasConversion(user => user!.Value, value => new Password(value));

    builder.Property(user => user.Photo)
      .HasColumnName("photo")
      .HasMaxLength(100)
      .HasColumnType("varchar(100)")
      .HasDefaultValue(null)
      .HasConversion(user => user!.Value, value => new Photo(value));

    builder.Property(user => user.CreatedAt)
      .HasColumnName("created_at")
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder.Property(user => user.UpdatedAt)
      .HasColumnName("updated_at")
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnUpdate();

    builder.HasIndex(user => user.Email)
      .IsUnique();

    builder.HasOne(user => user.Role)
      .WithMany(role => role.Users)
      .HasForeignKey(user => user.RoleId);
  }
}
