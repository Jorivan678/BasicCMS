using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Data.Common;
using webapi.Infrastructure.Options;

namespace webapi.Infrastructure.Data
{
    /// <summary>
    /// The database context of this application.
    /// </summary>
    internal class AppDbContext : IAsyncDisposable
    {
        private readonly NpgsqlConnection _connection;

        public AppDbContext(IOptions<AppDbOptions> options)
        {
            _connection = new(options.Value.ConnectionString);
        }

        /// <summary>
        /// The database connection. Don't dispose it manually.
        /// </summary>
        public DbConnection Connection => _connection;

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        { 
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
}
