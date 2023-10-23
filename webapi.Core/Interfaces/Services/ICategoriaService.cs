﻿using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.Interfaces.Services.Base;

namespace webapi.Core.Interfaces.Services
{
    public interface ICategoriaService : IBaseService<CategoriaResponseDto>
    {
        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="request">Contains the add request info.</param>
        /// <returns>The new category id.</returns>
        Task<int> CreateAsync(CategoriaUpdRequestDto request);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="request">Contains the update request info.</param>
        Task UpdateAsync(CategoriaUpdRequestDto request);

        /// <summary>
        /// Gets paginated categories.
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of categories.</returns>
        Task<IEnumerable<CategoriaResponseDto>> GetCategoriesAsync(int page, int quantity);
    }
}
