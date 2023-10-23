namespace webapi.Core.DTOs.Comentario.Request
{
    public class ComentarioAddRequestDto
    {
        public string Texto { get; set; } = null!;

        public int AutorId { get; set; }

        public int ArticuloId { get; set; }
    }
}
