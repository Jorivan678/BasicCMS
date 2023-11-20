using webapi.Core.Exceptions;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.DTOs.Imagen.Response;
using webapi.Core.Interfaces.Base;

namespace webapi.Core.Interfaces.Services
{
    /// <summary>
    /// Image service contract.
    /// </summary>
    /// <typeparam name="TFile">The type to receive a file from a request.</typeparam>
    public interface IImagenService<TFile> : IBaseService<ImagenResponseDto> where TFile : class
    {
        /// <summary>
        /// Updates an existing registered image.
        /// </summary>
        /// <param name="request">Contains the update request info.</param>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task UpdateAsync(ImageUpdRequestDto request);

        /// <summary>
        /// Adds a new image record.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The id of the new image record.</returns>
        Task<int> AddAsync(ImageAddRequestDto<TFile> request);

        /// <summary>
        /// Gets paginated registered images.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="ImagenResponseDto"/>.</returns>
        Task<IEnumerable<ImagenResponseDto>> GetImagesAsync(int page, int quantity);

        /// <summary>
        /// Gets total registered images count in the database.
        /// </summary>
        /// <returns>Images count.</returns>
        Task<int> CountImagesAsync();
    }
}
