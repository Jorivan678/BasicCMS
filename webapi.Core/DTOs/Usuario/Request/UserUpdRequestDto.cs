namespace webapi.Core.DTOs.Usuario.Request
{
    public class UserUpdRequestDto
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string ApellidoP { get; set; } = null!;

        public string ApellidoM { get; set; } = null!;
    }
}
