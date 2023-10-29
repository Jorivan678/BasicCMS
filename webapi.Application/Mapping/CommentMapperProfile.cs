using AutoMapper;
using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.DTOs.Comentario.Response;
using webapi.Core.Entities;

namespace webapi.Application.Mapping
{
    internal class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comentario, ComentarioResponseDto>();

            CreateMap<ComentarioAddRequestDto, Comentario>()
                .AfterMap((src, dst) => dst.Fecha_Pub = DateTimeOffset.UtcNow);

            CreateMap<ComentarioUpdRequestDto, Comentario>();
        }
    }
}
