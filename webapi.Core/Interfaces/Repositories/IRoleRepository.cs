using webapi.Core.Entities;

namespace webapi.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Gets roles in the application (by default, there are just 3).
        /// </summary>
        /// <returns>A list of roles.</returns>
        Task<IEnumerable<Rol>> GetRolesAsync();

        /// <summary>
        /// Gets a role by <paramref name="roleId"/>.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns>An instance of <see cref="Rol"/> if exists, otherwise, it'll be <see langword="null"/>.</returns>
        Task<Rol?> FindAsync(int roleId);

        /// <summary>
        /// Updates an existing role in the database (just the description).
        /// </summary>
        /// <param name="rol"></param>
        /// <returns><see langword="true"/> if operation was successful; otherwise will be <see langword="false"/>.</returns>
        Task<bool> UpdateAsync(Rol rol);
    }
}
