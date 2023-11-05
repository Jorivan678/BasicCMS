using webapi.Core.Exceptions;
using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.DTOs.Usuario.Response;
using webapi.Core.Interfaces.Services.Base;
using webapi.Core.StaticData;

namespace webapi.Core.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UserResponseDto>
    {
        /// <summary>
        /// Registers a new user in the application.
        /// </summary>
        /// <param name="request">Contains add request info.</param>
        /// <returns>The new user id.</returns>
        /// <exception cref="ConflictException"></exception>
        Task<int> RegisterAsync(UserAddRequestDto request);

        /// <summary>
        /// Updates user information like username, name and last names.
        /// </summary>
        /// <param name="request">Contains update request info.</param>
        /// <exception cref="ForbiddenException"></exception>
        /// <exception cref="ConflictException"></exception>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task UpdateAsync(UserUpdRequestDto request);

        /// <summary>
        /// Gets paginated users. (This is for user management).
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="UserResponseDto"/>.</returns>
        Task<IEnumerable<UserResponseDto>> GetUsersAsync(int page, int quantity);

        /// <summary>
        /// Gets users who have <see cref="Roles.Editor"/> as a role.
        /// </summary>
        /// <returns>A list of <see cref="UserResponseDto"/>.</returns>
        Task<IEnumerable<UserResponseDto>> GetUsersAuthorsAsync();

        /// <summary>
        /// Gets total users count in the database.
        /// </summary>
        /// <returns>Users count.</returns>
        Task<int> CountUsersAsync();
    }
}