using AutoMapper;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>();

            CreateMap<CatARCRequestDto, Categoria>();

            CreateMap<CategoriaAddRequestDto, Categoria>();

            CreateMap<CategoriaUpdRequestDto, Categoria>();
        }
    }
}
