using Application.Abstractions.Messaging;
using Application.Modules.Permissions.DTOs.Responses;
using Domain.Shared;

namespace Application.Modules.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery
  : PaginationParams, IQuery<PagedResults<PermissionResponse>>
{ }
