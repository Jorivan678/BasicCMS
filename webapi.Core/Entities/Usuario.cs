using webapi.Core.Entities.Base;

namespace webapi.Core.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; } = null!;

        public string ApellidoP { get; set; } = null!;

        public string ApellidoM { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string PasswordSalt { get; set; } = null!;

        public ICollection<Rol> Roles { get; set; } = null!;
    }
}
