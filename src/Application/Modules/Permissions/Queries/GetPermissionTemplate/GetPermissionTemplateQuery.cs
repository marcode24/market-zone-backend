using Application.Abstractions.Messaging;

namespace Application.Modules.Permissions.Queries.GetPermissionTemplate;

public record GetPermissionTemplateQuery
: IQuery<byte[]>
{ }
