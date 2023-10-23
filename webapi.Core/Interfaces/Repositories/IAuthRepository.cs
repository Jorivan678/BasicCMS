using webapi.Core.Interfaces.Services;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for auth methods. Can merge with user's repository in <see cref="IAuthService"/>.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Updates user password.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise will be <see langword="false"/>.</returns>
        Task<bool> UpdatePasswordAsync(int userId, string newPassword);

        /// <summary>
        /// Obtains user roles from a specific user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of strings that represents roles of a user.</returns>
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);

        /// <summary>
        /// Checks if the entered password matches the user's current password.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="attemptedPassword">The attempted password.</param>
        /// <returns><see langword="true"/> if password matches; otherwise will be <see langword="false"/>.</returns>
        Task<bool> CheckPasswordAsync(int userId, string attemptedPassword);

        /// <summary>
        /// Assigns  a role to a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise will be <see langword="false"/>.</returns>
        Task<bool> AssignRoleAsync(int userId, int roleId);

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise will be <see langword="false"/>.</returns>
        Task<bool> RemoveRoleAsync(int userId, int roleId);
    }
}
