namespace webapi.Core.DTOs.Usuario.Request
{
    public class UserAddRequestDto
    {
        public string Nombre { get; set; } = null!;

        public string ApellidoP { get; set; } = null!;

        public string ApellidoM { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
