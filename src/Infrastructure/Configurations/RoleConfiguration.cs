namespace Infrastructure.Configurations;

using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Shared.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Role configuration class.
/// </summary>
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  /// <summary>
  /// Configure role entity.
  /// </summary>
  /// <param name="builder">Entity type builder.</param>
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.ToTable(Tables.Roles, Schemas.Market);

    builder.HasKey(role => role.Id);

    builder.Property(role => role.Id)
      .HasConversion(id => id!.Value, value => new RoleId(value))
      .ValueGeneratedOnAdd();

    builder.Property(role => role.Name)
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(role => role!.Value, value => new Name(value));

    builder.Property(user => user.CreatedAt)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder.Property(user => user.UpdatedAt)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnUpdate();

    builder.HasMany(role => role.Permissions)
      .WithMany(permission => permission.Roles)
      .UsingEntity<RolePermission>(
        j => j
          .HasOne(p => p.Permission)
          .WithMany(p => p.RolePermissions)
          .HasForeignKey(j => j.PermissionId),
        j => j
          .HasOne(p => p.Role)
          .WithMany(p => p.RolePermissions)
          .HasForeignKey(p => p.RoleId));
  }
}
