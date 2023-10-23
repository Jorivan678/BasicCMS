using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.DTOs.Comentario.Response;
using webapi.Core.Interfaces.Services.Base;

namespace webapi.Core.Interfaces.Services
{
    public interface IComentarioService : IBaseService<ComentarioResponseDto>
    {
        /// <summary>
        /// Adds a new comment to an article.
        /// </summary>
        /// <param name="request">Contains the add request info.</param>
        /// <returns>The new comment id.</returns>
        Task<int> CreateAsync(ComentarioAddRequestDto request);

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="request">Contains the update request info.</param>
        Task UpdateAsync(ComentarioResponseDto request);

        /// <summary>
        /// Gets comments by article.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <param name="articleId">The article that owns those comments.</param>
        /// <returns>A list of <see cref="ComentarioResponseDto"/>.</returns>
        Task<IEnumerable<ComentarioResponseDto>> GetCommentsAsync(int page, int quantity, int articleId);
    }
}
