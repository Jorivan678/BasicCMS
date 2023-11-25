using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Imagen : BaseEntity
    {
        public string Titulo_Imagen { get; set; } = null!;

        public string Ruta { get; set; } = null!;

        public short Alto { get; set; }

        public short Ancho { get; set; }

        public DateTimeOffset Fecha_Subida { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public ICollection<Articulo> Articulos { get; set; } = null!;
    }
}
