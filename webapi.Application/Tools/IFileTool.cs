using Microsoft.AspNetCore.Http;
using webapi.Core.Exceptions;

namespace webapi.Application.Tools
{
    /// <summary>
    /// Manages files. For now, it only manages images.
    /// </summary>
    public interface IFileTool
    {
        /// <summary>
        /// Checks if the file is an image.
        /// If it turns out not to be an image, a <see cref="BusinessException"/> exception will be thrown.
        /// <para>Note: It closes the stream internally.</para>
        /// </summary>
        /// <param name="file">The file to check.</param>
        /// <exception cref="BusinessException"></exception>
        Task CheckIfIsImageAsync(Stream file);

        /// <summary>
        /// This method is used to save the image if it has passed the test of 
        /// <see cref="CheckIfIsImageAsync(Stream)"/> to determine if it is an image.
        /// </summary>
        /// <param name="archivo">The file received from the controller.</param>
        /// <returns>The (non-absolute) path where the image was saved. For example: "images/image.png".</returns>
        /// <exception cref="Exception"></exception>
        Task<string> SaveImageAsync(IFormFile archivo);

        /// <summary>
        /// This method deletes an image from the application.
        /// </summary>
        /// <param name="route">Location of the image on the server.</param>
        /// <exception cref="Exception"></exception>
        void DeleteImage(string route);

        /// <summary>
        /// Method responsible for obtaining the height and width of a saved image.
        /// </summary>
        /// <param name="route">The (relative) path of the image.</param>
        /// <returns>A tuple containing the values. The first value is the height, and the second is the width.</returns>
        /// <exception cref="Exception"></exception>
        Task<(short Height, short Width)> GetHeightAndWidthAsync(string route);
    }
}