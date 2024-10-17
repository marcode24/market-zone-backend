namespace Application.Core.Mapping;

using Application.Modules.Permissions.DTOs.Responses;
using AutoMapper;
using Domain.Entities.Permissions;
using Domain.Entities.Permissions.ObjectValues;
using Domain.Shared.ValueObjects;

public class PermissionMappingProfile : Profile
{
  public PermissionMappingProfile()
  {
    CreateMap<PermissionId, int>()
      .ConvertUsing(src => src.Value);

    CreateMap<Name, string>()
      .ConvertUsing(src => src.Value);

    CreateMap<TypePermission, string>()
      .ConvertUsing(src => src.Value);

    CreateMap<Permission, PermissionResponse>()
      .ForMember(
        dest => dest.Id,
        opt => opt.MapFrom(src => src.Id!.Value))
      .ForMember(
        dest => dest.Name,
        opt => opt.MapFrom(src => src.Name!.Value))
      .ForMember(
        dest => dest.Type,
        opt => opt.MapFrom(src => src.Type!.Value))
      .ForMember(
        dest => dest.IsActive,
        opt => opt.MapFrom(src => src.IsActive))
      .ForMember(
        dest => dest.CreatedAt,
        opt => opt.MapFrom(src => src.CreatedAt.ToLocalTime())
      );
  }
}
