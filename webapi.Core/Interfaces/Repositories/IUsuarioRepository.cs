using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories.Base;

namespace webapi.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contract for users repository implementation.
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        /// <summary>
        /// Gets an user by its username.
        /// </summary>
        /// <param name="username">The user's name.</param>
        /// <returns>A User if exists, otherwise, it'll be <see langword="null"/>.</returns>
        Task<Usuario?> FindAsync(string username);

        /// <summary>
        /// Gets paginated users. (This is for user management).
        /// </summary>
        /// <param name="page">Requested page number.</param>
        /// <param name="quantity">Quantity of elements per page.</param>
        /// <returns>A list of <see cref="Usuario"/>.</returns>
        Task<IEnumerable<Usuario>> GetUsersAsync(int page, int quantity);

        /// <summary>
        /// Checks if user name already exists in the database.
        /// </summary>
        /// <param name="username">The user's name to check.</param>
        /// <returns><see langword="true"/> if exists, otherwise, <see langword="false"/>.</returns>
        Task<bool> UserNameExistsAsync(string username);

        /// <summary>
        /// Checks if user name already exists in the database.
        /// </summary>
        /// <param name="username">The user's name to check.</param>
        /// <param name="userId">To verify that it exists and is not from the user who requested the update.</param>
        /// <returns><see langword="true"/> if exists, otherwise, <see langword="false"/>.</returns>
        Task<bool> UserNameExistsAsync(string username, int userId);
    }
}
