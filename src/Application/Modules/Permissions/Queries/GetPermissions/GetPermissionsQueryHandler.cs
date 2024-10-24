using System.Text;
using System.Text.RegularExpressions;
using Application.Abstractions.Messaging;
using Application.Modules.Permissions.DTOs.Responses;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities.Permissions;
using Domain.Repositories.Paginations;
using Domain.Shared;
using Domain.Specifications;

namespace Application.Modules.Permissions.Queries.GetPermissions;

internal sealed class GetPermissionsQueryHandler
  : IQueryHandler<GetPermissionsQuery, PagedResults<PermissionResponse>>
{
  private readonly IPaginationRepository<Permission, PermissionResponse> _paginationRepository;
  private readonly IMapper _mapper;

  public GetPermissionsQueryHandler(
    IPaginationRepository<Permission, PermissionResponse> permissionRepository,
    IMapper mapper)
  {
    _paginationRepository = permissionRepository;
    _mapper = mapper;
  }

  public async Task<Result<PagedResults<PermissionResponse>>> Handle(
    GetPermissionsQuery request,
    CancellationToken cancellationToken)
  {

    var propertiesResponse = typeof(PermissionResponse).GetProperties();
    var selectFields = string.Join(", ", propertiesResponse.Select(
      x => $"{ToSnakeCase(x.Name)} AS {x.Name}"
    ));

    var specification = new PermissionSpecification(
      search: request.Search,
      skip: (request.PageNumber - 1) * request.PageSize,
      take: request.PageSize,
      orderBy: request.OrderBy ?? "Name",
      orderAsc: request.OrderAsc,
      selectFields: selectFields
    );

    var results = await _paginationRepository.GetPagedResults(specification, cancellationToken);

    var resultMapped = _mapper.Map<List<PermissionResponse>>(results.Results);

    var pagedResultsMapped = new PagedResults<PermissionResponse>
    {
      Results = resultMapped,
      PageNumber = results.PageNumber,
      PageSize = results.PageSize,
      TotalPages = results.TotalPages,
      TotalRecords = results.TotalRecords
    };

    return Result.Success(pagedResultsMapped);
  }

  public static string ToSnakeCase(string input)
  {
    if (string.IsNullOrEmpty(input)) return input;

    var snakeCase = Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2");
    return snakeCase.ToLower();
  }

}

public class PermissionSpecification : BaseSpecification<Permission, PermissionResponse>
{
  public PermissionSpecification(
    string? search,
    int skip,
    int take,
    string orderBy,
    bool orderAsc,
    string selectFields)
  {
    if (!string.IsNullOrWhiteSpace(search))
    {
      AddCondition("Name ILIKE @Search", "Search", $"%{search}%");
    }

    AddOrderBy(orderBy + (orderAsc ? " ASC" : " DESC"));
    ApplyPaging(skip, take);
    SelectFields(selectFields);
  }
}

