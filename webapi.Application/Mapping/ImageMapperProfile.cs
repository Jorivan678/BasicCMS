using AutoMapper;
using Microsoft.AspNetCore.Http;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.DTOs.Imagen.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class ImageMapperProfile : Profile
    {
        public ImageMapperProfile()
        {
            CreateMap<Imagen, ImagenResponseDto>();

            CreateMap<ImageAddRequestDto<IFormFile>, Imagen>();

            CreateMap<ImageUpdRequestDto, Imagen>();
        }
    }
}
