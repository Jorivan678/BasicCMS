using Dapper;
using System.Data;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Repositories
{
    internal class ComentarioRepository : IComentarioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComentarioRepository> _logger;

        public ComentarioRepository(AppDbContext context, ILogger<ComentarioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CountComentariosAsync(int articleId)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Comentarios WHERE articuloid = @articleId";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, new { articleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of comments from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of comments count.", ex);
            }
        }

        public async Task<int> CreateAsync(Comentario entity)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = @"INSERT INTO Comentarios(texto, fecha_pub, autorid, articuloid)
                        VALUES(@Texto, @Fecha_Pub, @AutorId, @ArticuloId) RETURNING ID";

                int newId = await _context.Connection.ExecuteScalarAsync<int>(sql, entity, transact);

                if (newId <= 0)
                {
                    await transact.RollbackAsync();
                    return newId;
                }

                await transact.CommitAsync();
                return newId;
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                _logger.LogError(ex, "An error occurred during the addition of a comment to the database: \n Date and time: {fecha} \n", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the comment addition operation.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = "DELETE FROM Comentarios WHERE id = @entityId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { entityId }, transact);

                if (rows != 1)
                {
                    await transact.RollbackAsync();
                    return false;
                }

                await transact.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                _logger.LogError(ex, "An error ocurred during the user deletion operation from the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during the user deletion operation.", ex);
            }
        }

        public async Task<Comentario?> FindAsync(int entityId)
        {
            try
            {
                const string sql = @"SELECT * FROM Comentarios WHERE id = @entityId";

                return await _context.Connection.QueryFirstOrDefaultAsync<Comentario>(sql, new { entityId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a comment by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the comment.", ex);
            }
        }

        public async Task<IEnumerable<Comentario>> GetComentariosAsync(int page, int quantity, int articleId)
        {
            try
            {
                const string sql = @"SELECT C.*, false as split, U.id, '' as nombre, '' as apellidop, '' as apellidom, U.nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Comentarios AS C
                    INNER JOIN Usuarios AS U ON U.id = C.autorid";

                return await _context.Connection.QueryAsync<Comentario>(sql, new { page, quantity, articleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of comments from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of comments.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Comentario entity)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = "UPDATE Comentarios SET texto = @Texto WHERE autorid = @AutorId AND id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, entity, transact);

                if (rows != 0)
                {
                    await transact.RollbackAsync();
                    return false;
                }

                await transact.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                _logger.LogError(ex, "An error occurred during the update of a comment in the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the update of the comment.", ex);
            }
        }
    }
}
