using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public ICollection<Articulo> Articulos { get; set; } = null!;
    }
}
