namespace webapi.Core.DTOs.Imagen.Request
{
    /// <summary>
    /// This Dto is special, it uses a generic type to declare the object that contains
    /// the client request file.
    /// </summary>
    /// <typeparam name="TFile">The type to receive a file from a request.</typeparam>
    public class ImageAddRequestDto<TFile> where TFile : class
    {
        public string Titulo { get; set; } = null!;

        public TFile RequestFile { get; set; } = null!;

        public int UsuarioId { get; set; }
    }
}
