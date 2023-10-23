namespace webapi.Core.DTOs.Categoria.Request
{
    public class CategoriaUpdRequestDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
