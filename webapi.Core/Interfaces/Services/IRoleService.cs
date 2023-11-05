using webapi.Core.Exceptions;
using webapi.Core.DTOs.Rol.Request;
using webapi.Core.DTOs.Rol.Response;

namespace webapi.Core.Interfaces.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Gets roles in the application (by default, there are just 3).
        /// </summary>
        /// <returns>A list of roles.</returns>
        Task<IEnumerable<RoleResponseDto>> GetRolesAsync();

        /// <summary>
        /// Updates the description of a role.
        /// </summary>
        /// <param name="request">Contains update request info.</param>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task UpdateAsync(RoleUpdRequestDto request);

        /// <summary>
        /// Gets a role by <paramref name="roleId"/>.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns>An instance of <see cref="RoleResponseDto"/>.</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<RoleResponseDto> GetRoleAsync(int roleId);
    }
}
