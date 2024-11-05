namespace Application.Modules.Permissions.Queries.GetPermissionReportExcel;

using Application.Abstractions.Messaging;

public record GetPermissionReportExcelQuery
: IQuery<byte[]>
{ }
