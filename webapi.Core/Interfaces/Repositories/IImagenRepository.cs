using webapi.Core.Entities;
using webapi.Core.Interfaces.Base;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for images repository implementation.
    /// </summary>
    public interface IImagenRepository : IBaseRepository<Imagen>
    {
        /// <summary>
        /// Gets paginated registered images.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="Imagen"/>.</returns>
        Task<IEnumerable<Imagen>> GetImagesAsync(int page, int quantity);

        /// <summary>
        /// Gets total registered images count in the database.
        /// </summary>
        /// <returns>Images count.</returns>
        Task<int> CountImagesAsync();
    }
}
