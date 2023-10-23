using webapi.Core.DTOs.Usuario.Response;

namespace webapi.Core.DTOs.Comentario.Response
{
    public class ComentarioResponseDto
    {
        public int Id { get; set; }

        public string Texto { get; set; } = null!;

        public DateTimeOffset Fecha_Pub { get; set; }

        public int AutorId { get; set; }

        public int ArticuloId { get; set; }

        public UserResponseDto Autor { get; set; } = null!;
    }
}
