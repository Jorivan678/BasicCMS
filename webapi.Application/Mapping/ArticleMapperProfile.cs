using AutoMapper;
using webapi.Core.DTOs.Articulo.Request;
using webapi.Core.DTOs.Articulo.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class ArticleMapperProfile : Profile
    {
        public ArticleMapperProfile()
        {
            CreateMap<Articulo, ArticuloResponseDto>();

            CreateMap<ArticuloAddRequestDto, Articulo>()
                .AfterMap((src, dst) => dst.Fecha_Pub = DateTimeOffset.UtcNow);

            CreateMap<ArticuloUpdRequestDto, Articulo>();
        }
    }
}
