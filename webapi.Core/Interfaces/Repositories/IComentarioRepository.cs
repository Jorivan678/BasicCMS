using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories.Base;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for comments repository implementation.
    /// </summary>
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
        /// <summary>
        /// Gets comments by article.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="articleId">The article that owns those comments.</param>
        /// <returns>A list of <see cref="Comentario"/>.</returns>
        Task<IEnumerable<Comentario>> GetComentariosAsync(int page, int quantity, int articleId);
    }
}
