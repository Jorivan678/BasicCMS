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
        private readonly IConfiguration _config;
        private string _connStrKey = null!;

        public AppDbOptions(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString { 
            get
            {
                var connStr = _config.GetConnectionString(_connStrKey);
                if (string.IsNullOrWhiteSpace(connStr)) throw new InvalidOperationException("There isn't a connection string ");

                return connStr;
            }
        }

        /// <summary>
        /// Sets the connection string key for get from the "ConnectionStrings" section in the appsettings.json file.
        /// </summary>
        /// <param name="connKey">The name of the connection string key in the specified section of appsettings.json.</param>
        /// <remarks>
        /// Here's an example of how to use the SetConnStrKey method:
        /// <code>
        /// dbOptions.SetConnStrKey("DbConnection");
        /// </code>
        /// Where "DbConnection is in:
        /// <code>
        ///   "ConnectionStrings": {
        ///      "DbConnection": "a connection string"
        ///    }
        /// </code>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetConnStrKey([NotNull]string? connKey)
        {
            if (string.IsNullOrWhiteSpace(connKey))
                throw new ArgumentNullException(nameof(connKey), "Connection string key cannot be null or empty.");

            _connStrKey = connKey;
        }
    }
}
