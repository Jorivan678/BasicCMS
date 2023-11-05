using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Options
{
    /// <summary>
    /// <see cref="AppDbContext"/> options.
    /// </summary>
    public sealed class AppDbOptions
    {
        private string _connStr = null!;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString => _connStr;

        /// <summary>
        /// Sets the connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetConnectionString([NotNull]string? connectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            _connStr = connectionString;
        }
    }
}
