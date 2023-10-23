using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.DTOs.Usuario.Response;
using webapi.Core.Interfaces.Services.Base;

namespace webapi.Core.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<UserResponseDto>
    {
        /// <summary>
        /// Registers a new user in the application.
        /// </summary>
        /// <param name="request">Contains add request info.</param>
        /// <returns>The new user id.</returns>
        Task<int> RegisterAsync(UserAddRequestDto request);

        /// <summary>
        /// Gets paginated users. (This is for user management).
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="UserResponseDto"/>.</returns>
        Task<IEnumerable<UserResponseDto>> GetUsersAsync(int page, int quantity);
    }
}