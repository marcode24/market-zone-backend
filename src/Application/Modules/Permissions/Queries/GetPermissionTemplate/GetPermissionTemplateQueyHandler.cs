using Application.Abstractions.Messaging;
using Application.Modules.Permissions.FileTemplates;
using Domain.Abstractions;

namespace Application.Modules.Permissions.Queries.GetPermissionTemplate;

internal sealed class GetPermissionTemplateQueryHandler
: IQueryHandler<GetPermissionTemplateQuery, byte[]>
{
  public async Task<Result<byte[]>> Handle(GetPermissionTemplateQuery request, CancellationToken cancellationToken)
  {
    return await Task.FromResult(PermissionTemplateGenerator.CreatePermissionTemplateGenerator());
  }
}
