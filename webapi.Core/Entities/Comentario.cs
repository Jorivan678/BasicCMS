using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Comentario : BaseEntity
    {
        public string Texto { get; set; } = null!;

        public DateTimeOffset Fecha_Pub { get; set; }

        public int AutorId { get; set; }

        public int ArticuloId { get; set; }

        public Usuario Autor { get; set; } = null!;

        public Articulo Articulo { get; set; } = null!;
    }
}
