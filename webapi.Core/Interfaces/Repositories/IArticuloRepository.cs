using webapi.Core.Entities;

namespace webapi.Core.Interfaces.Repositories
{
    public interface IArticuloRepository
    {
        /// <summary>
        /// Gets paginated articles.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity);

        /// <summary>
        /// Gets paginated articles, written by a specific author.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="autorId">The author of the articles.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, int authorId);
    }
}
