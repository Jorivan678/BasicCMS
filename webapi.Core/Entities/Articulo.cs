using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Articulo : BaseEntity
    {
        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public DateTimeOffset Fecha_Pub { get; set; }

        public int ImagenId { get; set; }

        public int AutorId { get; set; }

        public Usuario Autor { get; set; } = null!;

        public Imagen Imagen { get; set; } = null!;

        public ICollection<Categoria> Categorias { get; set; } = null!;

        public ICollection<Comentario> Comentarios { get; set; } = null!;
    }
}
