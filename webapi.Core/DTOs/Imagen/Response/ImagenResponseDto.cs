using webapi.Core.DTOs.Usuario.Response;

namespace webapi.Core.DTOs.Imagen.Response
{
    public class ImagenResponseDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Ruta { get; set; } = null!;

        public short Alto { get; set; }

        public short Ancho { get; set; }

        public DateTimeOffset Fecha_Subida { get; set; }

        public int UsuarioId { get; set; }

        public UserResponseDto Usuario { get; set; } = null!;
    }
}
