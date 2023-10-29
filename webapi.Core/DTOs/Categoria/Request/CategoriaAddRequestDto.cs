namespace webapi.Core.DTOs.Categoria.Request
{
    public class CategoriaAddRequestDto
    {
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
