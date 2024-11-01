using Application.Abstractions.Messaging;
using Application.Core.Responses;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Repositories.Permissions;
using Domain.Shared.ValueObjects;
using NPOI.XSSF.UserModel;

namespace Application.Modules.Permissions.Commands.BulkCreatePermission;

internal class BulkCreatePermissionCommandHandler
  : ICommandHandler<BulkCreatePermissionCommand, CreateResponseBulk>
{
  private readonly IPermissionRepository _permissionRepository;
  private readonly IUnitOfWork _unitOfWork;
  public BulkCreatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork
  )
  {
    _permissionRepository = permissionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result<CreateResponseBulk>> Handle(
    BulkCreatePermissionCommand request,
    CancellationToken cancellationToken)
  {
    var permissionsToAdd = new List<Permission>();

    using var stream = request.ExcelFile.OpenReadStream();
    var workbook = new XSSFWorkbook(stream);
    var sheet = workbook.GetSheetAt(0);
    int totalRows = sheet.LastRowNum;

    int minBatchSize = 100;
    int maxBatchSize = 1000;
    int minBatches = 5;
    int maxBatches = 20;

    int batchCount = Math.Clamp(totalRows / maxBatchSize, minBatches, maxBatches);
    int batchSize = Math.Clamp(totalRows / batchCount, minBatchSize, maxBatchSize);

    int proccessedRows = 0;
    using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

    try
    {
      for (int row = 1; row <= totalRows; row++)
      {
        var currentRow = sheet.GetRow(row);
        if (currentRow is null) continue;

        var namePermission = currentRow.GetCell(0)?.ToString() ?? string.Empty;
        var typePermission = currentRow.GetCell(1)?.ToString() ?? string.Empty;

        var permission = Permission.Create(
          new Name(namePermission),
          new TypePermission(typePermission)
        );

        permissionsToAdd.Add(permission);

        if (permissionsToAdd.Count >= batchSize)
        {
          _permissionRepository.AddRange(permissionsToAdd);
          await _unitOfWork.SaveChangesAsync(cancellationToken);
          proccessedRows += permissionsToAdd.Count;
          permissionsToAdd.Clear();
        }
      }

      if (permissionsToAdd.Count != 0)
      {
        _permissionRepository.AddRange(permissionsToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        proccessedRows += permissionsToAdd.Count;
        permissionsToAdd.Clear();
      }

      await transaction.CommitAsync(cancellationToken);

      var result = CreateResponseBulk.Success(
        proccessedRows,
        PermissionMessages.BulkCreated.Message
      );

      return Result.Success(result);
    }
    catch (Exception)
    {
      await transaction.RollbackAsync(cancellationToken);
      return Result.Failure<CreateResponseBulk>(PermissionErrors.ErrorCreatingBulk);
    }
    finally
    {
      await transaction.DisposeAsync();
    }
  }
}
