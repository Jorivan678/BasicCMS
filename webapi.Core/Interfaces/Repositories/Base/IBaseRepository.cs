﻿using webapi.Core.Entities.Base;

namespace webapi.Core.Interfaces.Repositories.Base
{
    /// <summary>
    /// Base contract with common methods used in all repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity used by the repository that inherits from this interface.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Creates a new record of <typeparamref name="TEntity"/> in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The new record id.</returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        /// Deletes a record of <typeparamref name="TEntity"/> in the database.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns><see langword="true"/> if operation was successful; otherwise will be <see langword="false"/>.</returns>
        Task<bool> DeleteAsync(int entityId);

        /// <summary>
        /// Gets an entity based in its <paramref name="entityId"/>.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        Task<TEntity?> FindAsync(int entityId);
    }
}
