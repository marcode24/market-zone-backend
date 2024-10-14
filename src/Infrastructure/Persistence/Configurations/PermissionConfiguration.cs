namespace Infrastructure.Persistence.Configurations;

using Application.Configurations.Permissions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Shared.ValueObjects;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder
      .ToTable(Tables.Permissions, Schemas.Market);

    builder
      .HasKey(permission => permission.Id);

    builder
      .Property(permission => permission.Id)
      .ValueGeneratedOnAdd()
      .HasConversion(
        permissionId => permissionId!.Value,
        value => new PermissionId(value)
      );

    builder
      .Property(permission => permission.Name)
      .HasMaxLength(PermissionValidationConfig.NameMaxLength)
      .HasColumnType($"varchar({PermissionValidationConfig.NameMaxLength})")
      .IsRequired()
      .HasConversion(
        permission => permission!.Value,
        value => new Name(value)
      );

    builder
      .Property(permission => permission.Type)
      .HasMaxLength(PermissionValidationConfig.TypeMaxLength)
      .HasColumnType($"varchar({PermissionValidationConfig.TypeMaxLength})")
      .IsRequired()
      .HasConversion(
        permission => permission!.Value.ToUpper(),
        value => new TypePermission(value)
      );

    builder
      .Property(permission => permission.IsActive)
      .HasColumnType("boolean")
      .HasDefaultValue(true);

    builder
      .Property(permission => permission.IsDeleted)
      .HasColumnType("boolean")
      .HasDefaultValue(false);

    builder
      .Property(permission => permission.CreatedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder
      .Property(permission => permission.UpdatedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValueSql("now()");

    builder
      .Property(permission => permission.DeletedAt)
      .HasColumnType("timestamptz")
      .IsRequired(false);

    builder
      .HasMany(permission => permission.Roles)
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

    builder.HasQueryFilter(permission => !permission.IsDeleted);
  }
}
