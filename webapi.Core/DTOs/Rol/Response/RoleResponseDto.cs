namespace webapi.Core.DTOs.Rol.Response
{
    public class RoleResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
