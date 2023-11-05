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

        /// <summary>
        /// Asynchronously opens database connection and begins a database transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>A task whose <see cref="ValueTask{T}.Result"/> property is an object representing the new transaction.</returns>
        public async ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync(cancellationToken);

            return await _connection.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        /// <summary>
        /// Asynchronously opens database connection and begins a database transaction.
        /// </summary>
        /// <param name="cancellationToken">
        /// An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>A task whose <see cref="ValueTask{T}.Result"/> property is an object representing the new transaction.</returns>
        public ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => BeginTransactionAsync(IsolationLevel.Unspecified, cancellationToken);

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        { 
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
}
