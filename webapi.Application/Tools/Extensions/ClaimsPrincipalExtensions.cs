using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Checks if session user has the id (only accepts integer structs: <see cref="short"/> or <see cref="int"/> or <see cref="long"/>).
        /// </summary>
        /// <typeparam name="T">Id type.</typeparam>
        /// <param name="pr">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <param name="id">The id.</param>
        /// <returns><see langword="true"/> if user has the id, otherwise is <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool HasId<T>(this ClaimsPrincipal pr, T id) where T : struct
        {
            ArgumentNullException.ThrowIfNull(pr, nameof(pr));

            if (typeof(T) != typeof(short) && typeof(T) != typeof(int) && typeof(T) != typeof(long))
                throw new ArgumentException("An integer type was expected.", nameof(id));

            return pr.HasId(id.ToString());
        }

        /// <summary>
        /// Checks if session user has the id.
        /// </summary>
        /// <typeparam name="T">Id type.</typeparam>
        /// <param name="pr">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <param name="id">The id.</param>
        /// <returns><see langword="true"/> if user has the id, otherwise is <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool HasId(this ClaimsPrincipal pr, [NotNull]string? id)
        {
            ArgumentNullException.ThrowIfNull(pr, nameof(pr));
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            return pr.Claims.Any() && pr.Claims.First(x => x.Properties.Any(p => p.Value == JwtRegisteredClaimNames.NameId)).Value == id;
        }
    }
}
