namespace webapi.Application.Tools
{
    public interface IAuthValidator
    {
        /// <summary>
        /// Checks if current session user has the id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see langword="true"/> if user has the id, otherwise is <see langword="false"/>.</returns>
        bool HasId(string id);

        /// <summary>
        /// Checks if current session user has the id (only accepts integer structs: <see cref="short"/>, <see cref="int"/> or <see cref="long"/>).
        /// </summary>
        /// <typeparam name="T">Id type.</typeparam>
        /// <param name="id">The id.</param>
        /// <returns><see langword="true"/> if user has the id, otherwise is <see langword="false"/>.</returns>
        bool HasId<T>(T id) where T : struct;

        /// <summary>
        /// Checks if current session user has the role.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns><see langword="true"/> if user has the role, otherwise is <see langword="false"/>.</returns>
        bool HasRole(string roleName);
    }
}
