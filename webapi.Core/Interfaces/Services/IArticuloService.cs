using webapi.Core.Exceptions;
using webapi.Core.DTOs.Articulo.Request;
using webapi.Core.DTOs.Articulo.Response;
using webapi.Core.Interfaces.Base;

namespace webapi.Core.Interfaces.Services
{
    public interface IArticuloService : IBaseService<ArticuloResponseDto>
    {
        /// <summary>
        /// Adds a new article.
        /// </summary>
        /// <param name="request">Contains the add request info.</param>
        /// <returns>The new article id.</returns>
        Task<int> CreateAsync(ArticuloAddRequestDto request);

        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="request">Contains the update request info.</param>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task UpdateAsync(ArticuloUpdRequestDto request);

        /// <summary>
        /// Gets a list of articles optionally based on <paramref name="categories"/> or/and <paramref name="authorId"/>.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="categories">Different categories that must match with articles. If array length is zero, won't be taken into consideration.</param>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>A list of articles.</returns>
        Task<IEnumerable<ArticuloResponseDto>> GetArticlesAsync(int page, int quantity, string[] categories, int authorId);

        /// <summary>
        /// Counts articles optionally based on <paramref name="categories"/> or/and <paramref name="authorId"/>.
        /// </summary>
        /// <param name="categories">Different categories that must match with articles. If array length is zero, won't be taken into consideration.</param>
        /// <param name="authorId">The author of the articles.</param>
        /// <returns>Articles count.</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<int> CountArticlesAsync(string[] categories, int authorId);
    }
}
