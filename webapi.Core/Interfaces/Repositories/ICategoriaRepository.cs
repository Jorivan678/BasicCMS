using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories.Base;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for categories repository implementation.
    /// </summary>
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        /// <summary>
        /// Gets paginated categories.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="Categoria"/>.</returns>
        Task<IEnumerable<Categoria>> GetCategoriasAsync(int page, int quantity);

        /// <summary>
        /// Checks if category name already exists in the database.
        /// </summary>
        /// <param name="categoryName">The category name to check.</param>
        /// <returns><see langword="true"/> if exists, otherwise, <see langword="false"/>.</returns>
        Task<bool> CategoryExistsAsync(string categoryName);
    }
}
