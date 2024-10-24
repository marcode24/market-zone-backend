namespace Application.Services.Pagination;

using Application.Abstractions.Services;
using AutoMapper;
using Domain.Entities.Permissions;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

public class PaginationService<TEntity, TResponse>
  : IPaginationService<TEntity, TResponse>
{
  private readonly IMapper _mapper;

  public PaginationService(IMapper mapper)
  {
    _mapper = mapper;
  }

  public async Task<PagedResults<TResponse>> GetAllAsync(
    IQueryable<TEntity> query,
    CancellationToken cancellationToken)
  {
    var entities = await query.ToListAsync(cancellationToken);

    var mappedResults = MapEntities(entities);
    var pagedResults = CreatePagedResults(
      pageNumber: 1,
      pageSize: entities.Count,
      totalRecords: entities.Count,
      mappedResults: mappedResults);

    return pagedResults;
  }

  public async Task<PagedResults<TResponse>> GetPagedAsync(
    IQueryable<TEntity> query,
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken)
  {
    var totalRecords = await query.CountAsync(cancellationToken);

    var entities = await query
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);

    var mappedResults = MapEntities(entities);
    var pagedResults = CreatePagedResults(
      pageNumber: pageNumber,
      pageSize: pageSize,
      totalRecords: totalRecords,
      mappedResults: mappedResults);

    return pagedResults;
  }

  private List<TResponse> MapEntities(List<TEntity> entities) =>
    _mapper.Map<List<TResponse>>(entities);

  private static PagedResults<TResponse> CreatePagedResults(
    int pageNumber,
    int pageSize,
    int totalRecords,
    List<TResponse> mappedResults
  )
  {
    var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

    return new PagedResults<TResponse>
    {
      PageNumber = pageNumber,
      PageSize = pageSize,
      TotalPages = totalPages,
      TotalRecords = totalRecords,
      Results = mappedResults
    };
  }
}
