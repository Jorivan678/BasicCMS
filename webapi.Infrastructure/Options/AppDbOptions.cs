using System.Diagnostics.CodeAnalysis;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Options
{
    /// <summary>
    /// <see cref="AppDbContext"/> options.
    /// </summary>
    public sealed class AppDbOptions
    {
        public string ConnectionString { get; private set; } = null!;

        /// <summary>
        /// Sets the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetConnString([NotNull]string? connectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty("Connection string cannot be empty.", nameof(connectionString));

            ConnectionString = connectionString!;
        }
    }
}
