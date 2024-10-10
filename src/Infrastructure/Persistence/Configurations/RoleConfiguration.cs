namespace Infrastructure.Persistence.Configurations;

using Application.Configurations.Roles;
using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Domain.Shared.ValueObjects;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder
      .ToTable(Tables.Roles, Schemas.Market);

    builder
      .HasKey(role => role.Id);

    builder
      .Property(role => role.Id)
      .ValueGeneratedOnAdd()
      .HasConversion(
        id => id!.Value,
        value => new RoleId(value)
      );

    builder
      .Property(role => role.Name)
      .HasMaxLength(RoleConfigurations.NameMaxLength)
      .HasColumnType($"varchar({RoleConfigurations.NameMaxLength})")
      .IsRequired()
      .HasConversion(
        role => role!.Value.ToUpper(),
        value => new Name(value)
      );

    builder
      .Property(role => role.CreatedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnAdd();

    builder
      .Property(role => role.UpdatedAt)
      .HasColumnType("timestamptz")
      .HasDefaultValueSql("now()")
      .ValueGeneratedOnUpdate();

    builder
      .HasMany(role => role.Permissions)
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
