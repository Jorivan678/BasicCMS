using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories.Base;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for articles repository implementation.
    /// </summary>
    public interface IArticuloRepository : IBaseRepository<Articulo>
    {
        /// <summary>
        /// Gets paginated articles.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity);

        /// <summary>
        /// Gets total articles count in the database.
        /// </summary>
        /// <returns>Articles count.</returns>
        Task<int> CountArticulosAsync();

        /// <summary>
        /// Gets paginated articles, written by a specific author.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, int authorId);

        /// <summary>
        /// Gets total articles count in the database by an author.
        /// </summary>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>Articles count.</returns>
        Task<int> CountArticulosAsync(int authorId);

        /// <summary>
        /// Gets paginated articles, by different categories.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="categories">Different categories that must match with articles.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, string[] categories);

        /// <summary>
        /// Gets total articles count in the database by categories.
        /// </summary>
        /// <param name="categories">Different categories that must match with articles.</param>
        /// <returns>Articles count.</returns>
        Task<int> CountArticulosAsync(string[] categories);

        /// <summary>
        /// Gets paginated articles, by different categories and written by a specific author.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="categories">Different categories that must match with articles.</param>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>A list of <see cref="Articulo"/>.</returns>
        Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, string[] categories, int authorId);

        /// <summary>
        /// Gets total articles count in the database by an author and categories.
        /// </summary>
        /// <param name="categories">Different categories that must match with articles.</param>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>Articles count.</returns>
        Task<int> CountArticulosAsync(string[] categories, int authorId);
    }
}
