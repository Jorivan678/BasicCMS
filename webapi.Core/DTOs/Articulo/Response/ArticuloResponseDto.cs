using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.DTOs.Usuario.Response;

namespace webapi.Core.DTOs.Articulo.Response
{
    public class ArticuloResponseDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTimeOffset Fecha_Pub { get; set; }

        public int AutorId { get; set; }

        public UserResponseDto Autor { get; set; } = null!;

        public ICollection<CategoriaResponseDto> Categorias { get; set; } = null!;
    }
}
