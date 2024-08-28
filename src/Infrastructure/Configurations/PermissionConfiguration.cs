namespace Infrastructure.Configurations;

using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Permission configuration class.
/// </summary>
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  /// <summary>
  /// Configure permission entity.
  /// </summary>
  /// <param name="builder">Entity type builder.</param>
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.ToTable(Tables.Permissions, Schemas.Market);

    builder.HasKey(permission => permission.Id);

    builder.Property(permission => permission.Name)
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(permission => permission!.Value, value => new Name(value));

    builder.Property(permission => permission.Type)
      .HasMaxLength(50)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .HasConversion(permission => permission!.Value, value => new TypePermission(value));

    builder.Property(user => user.CreatedAt)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder.Property(user => user.UpdatedAt)
      .HasColumnType("timestamp")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnUpdate();

    builder.HasMany(permission => permission.Roles)
      .WithMany(role => role.Permissions)
      .UsingEntity<RolePermission>(
        j => j
          .HasOne(p => p.Role)
          .WithMany(p => p.RolePermissions)
          .HasForeignKey(j => j.RoleId),
        j => j
          .HasOne(p => p.Permission)
          .WithMany(p => p.RolePermissions)
          .HasForeignKey(p => p.PermissionId));
  }
}
