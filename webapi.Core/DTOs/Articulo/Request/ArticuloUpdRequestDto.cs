using webapi.Core.DTOs.Categoria.Request;

namespace webapi.Core.DTOs.Articulo.Request
{
    public class ArticuloUpdRequestDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public int AutorId { get; set; }

        public ICollection<CatARCRequestDto> Categorias { get; set; } = null!;
    }
}
