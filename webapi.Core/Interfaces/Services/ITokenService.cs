using webapi.Core.Entities;

namespace webapi.Core.Interfaces.Services
{
    /// <summary>
    /// A contract interface to implement a token creator service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a token based on user info and its roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles">The roles of the user.</param>
        /// <returns>A created token.</returns>
        string CreateToken(Usuario user, IEnumerable<string> roles);

        /// <summary>
        /// An asynchronous version of <see cref="CreateToken(Usuario, IEnumerable{string})"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles">The roles of the user.</param>
        /// <returns>A created token.</returns>
        Task<string> CreateTokenAsync(Usuario user, IEnumerable<string> roles);
    }
}
