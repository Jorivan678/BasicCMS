using webapi.Core.Exceptions;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.Interfaces.Base;

namespace webapi.Core.Interfaces.Services
{
    public interface ICategoriaService : IBaseService<CategoriaResponseDto>
    {
        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="request">Contains the add request info.</param>
        /// <returns>The new category id.</returns>
        /// <exception cref="ConflictException"></exception>
        Task<int> CreateAsync(CategoriaAddRequestDto request);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="request">Contains the update request info.</param>
        /// <exception cref="ConflictException"></exception>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task UpdateAsync(CategoriaUpdRequestDto request);

        /// <summary>
        /// Gets paginated categories.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of categories.</returns>
        Task<IEnumerable<CategoriaResponseDto>> GetCategoriesAsync(int page, int quantity);

        /// <summary>
        /// Gets total categories count in the database.
        /// </summary>
        /// <returns>Categories count.</returns>
        Task<int> CountCategoriesAsync();
    }
}
