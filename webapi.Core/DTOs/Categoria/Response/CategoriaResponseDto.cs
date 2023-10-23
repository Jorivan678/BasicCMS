namespace webapi.Core.DTOs.Categoria.Response
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
