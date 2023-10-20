using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Rol : BaseEntity
    {
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; } 

        public ICollection<Usuario> Usuarios { get; set; } = null!;
    }
}
