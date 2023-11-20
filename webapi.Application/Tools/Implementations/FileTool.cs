using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using webapi.Core.Exceptions;

namespace webapi.Application.Tools.Implementations
{
    internal sealed class FileTool : IFileTool
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly ILogger<FileTool> _logger;
        private const string ImageRoute = "server/images";

        public FileTool(IWebHostEnvironment webHost, ILogger<FileTool> logger)
        {
            _webHost = webHost;
            _logger = logger;
        }

        public async Task CheckIfIsImageAsync(Stream stream)
        {
            try
            {
                await using (stream)
                {
                    var result = await Image.DetectFormatAsync(stream);
                    _logger.LogInformation("Date: {fecha} \n An image was received and checked has passed. \n Extension: {extension} \n Mime type: {mimetype} \n Name: {name}", DateTimeOffset.UtcNow.ToString("G"), result.FileExtensions, result.DefaultMimeType, result.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An file was received and it wasn't an image. \n Date: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new BusinessException("The file received is not a valid image.", ex);
            }
        }

        public async Task<string> SaveImageAsync(IFormFile archivo)
        {
            await CheckIfIsImageAsync(archivo.OpenReadStream());
            try
            {
                string newFileName = $"{Path.GetFileNameWithoutExtension(archivo.FileName)}_{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
                string uploadFolder = Path.Combine(_webHost.WebRootPath, ImageRoute);
                string filePath = Path.Combine(uploadFolder, newFileName);
                await using var stream = new FileStream(filePath, FileMode.CreateNew);
                await archivo.CopyToAsync(stream);
                return $"{ImageRoute}/{newFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar guardar una imagen.");
                throw new Exception("Hubo un error al intentar guardar la imagen.", ex);
            }
        }

        public void DeleteImage(string route)
        {
            try
            {
                string rutaAbsoluta = Path.Combine(_webHost.WebRootPath, route);
                if (File.Exists(rutaAbsoluta))
                    File.Delete(rutaAbsoluta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar borrar una imagen.");
                throw new Exception("Hubo un error al intentar borrar la imagen anterior.", ex);
            }
        }

        public async Task<(short Height, short Width)> GetHeightAndWidthAsync(string route)
        {
            try
            {
                string absolutePath = Path.Combine(_webHost.WebRootPath, route);
                await using var stream = File.OpenRead(absolutePath);
                var image = await Image.IdentifyAsync(stream);

                return new((short)image.Height, (short)image.Width);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hubo un problema al leer la imagen y obtener sus datos.");
                throw new Exception("Hubo un error interno durante la operación.", ex);
            }
        }
    }
}
