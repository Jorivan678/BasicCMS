using webapi.Core.DTOs;
using webapi.Core.DTOs.Usuario;

namespace webapi.Core.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Creates a session token for a user who has requested to log in.
        /// </summary>
        /// <param name="request">Contains log in request info.</param>
        /// <returns>A session token.</returns>
        Task<TokenResponseDto> LogInAsync(UserLoginRequestDto request);

        /// <summary>
        /// Updates a user password.
        /// </summary>
        /// <param name="request">Contains password change request info.</param>
        Task UpdatePasswordAsync(UserPassUpdateDto request);

        /// <summary>
        /// Assigns  a role to a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        Task AssignRoleAsync(int userId, int roleId);

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        Task RemoveRoleAsync(int userId, int roleId);
    }
}
