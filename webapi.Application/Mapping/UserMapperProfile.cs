using AutoMapper;
using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.DTOs.Usuario.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<Usuario, UserResponseDto>();
            CreateMap<UserAddRequestDto, Usuario>()
                .AfterMap((src, dst) => dst.PasswordHash = src.Password);

            CreateMap<UserUpdRequestDto, Usuario>();
        }
    }
}
