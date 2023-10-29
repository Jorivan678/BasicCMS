using AutoMapper;
using webapi.Core.DTOs.Rol.Request;
using webapi.Core.DTOs.Rol.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Rol, RoleResponseDto>();

            CreateMap<RoleUpdRequestDto, Rol>();
        }
    }
}
