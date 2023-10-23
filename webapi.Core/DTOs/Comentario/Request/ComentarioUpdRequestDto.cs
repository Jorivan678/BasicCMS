namespace webapi.Core.DTOs.Comentario.Request
{
    public class ComentarioUpdRequestDto
    {
        public int Id { get; set; }

        public string Texto { get; set; } = null!;

        public int AutorId { get; set; }
    }
}
