namespace webapi.Core.Interfaces.Services.Base
{
    /// <summary>
    /// Base contract with common methods used in almost all services.
    /// </summary>
    public interface IBaseService<TResponse> where TResponse : class
    {
        /// <summary>
        /// Deletes an existing entity (based from the interface that inherits this interface).
        /// </summary>
        /// <param name="entityId">The entity identificator.</param>
        Task DeleteAsync(int entityId);

        /// <summary>
        /// Gets an existing entity mapped to <typeparamref name="TResponse"/>.
        /// </summary>
        /// <param name="entityId">The entity identificator.</param>
        /// <returns>A <typeparamref name="TResponse"/> instance.</returns>
        Task<TResponse> GetAsync(int entityId);
    }
}
