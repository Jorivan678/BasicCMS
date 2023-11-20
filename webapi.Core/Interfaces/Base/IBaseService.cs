using webapi.Core.Exceptions;

namespace webapi.Core.Interfaces.Base
{
    /// <summary>
    /// Base contract with common methods used in almost all services.
    /// </summary>
    public interface IBaseService<TResponse> where TResponse : class
    {
        /// <summary>
        /// Deletes an existing entity (based from the interface that inherits this interface).
        /// <para>Note about exceptions: Exceptions may differ in every service because
        /// not all throw a conflict or unprocessable entity exception or even may be
        /// necessary to document missing exceptions.</para>
        /// </summary>
        /// <param name="entityId">The entity identificator.</param>
        /// <exception cref="UnprocessableEntityException"></exception>
        Task DeleteAsync(int entityId);

        /// <summary>
        /// Gets an existing entity mapped to <typeparamref name="TResponse"/>.
        /// </summary>
        /// <param name="entityId">The entity identificator.</param>
        /// <returns>A <typeparamref name="TResponse"/> instance.</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<TResponse> GetAsync(int entityId);
    }
}
