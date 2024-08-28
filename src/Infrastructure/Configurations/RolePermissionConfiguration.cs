namespace Infrastructure.Configurations;

using Domain.Entities.Permissions.ObjectValues;
using Domain.Entities.Roles;
using Domain.Entities.Roles.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Role permission configuration class.
/// </summary>
internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
  /// <summary>
  /// Configure role permission entity.
  /// </summary>
  /// <param name="builder">Entity type builder.</param>
  public void Configure(EntityTypeBuilder<RolePermission> builder)
  {
    builder.ToTable(Tables.RolesPermissions, Schemas.Market);

    builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });

    builder.Property(rolePermission => rolePermission.RoleId)
      .IsRequired()
      .HasConversion(roleId => roleId!.Value, value => new RoleId(value));

    builder.Property(rolePermission => rolePermission.PermissionId)
      .IsRequired()
      .HasConversion(permissionId => permissionId!.Value, value => new PermisssionId(value));

    builder.HasOne(rolePermission => rolePermission.Role)
      .WithMany(role => role.RolePermissions)
      .HasForeignKey(rolePermission => rolePermission.RoleId);

    builder.HasOne(rolePermission => rolePermission.Permission)
      .WithMany(permission => permission.RolePermissions)
      .HasForeignKey(rolePermission => rolePermission.PermissionId);
  }
}
