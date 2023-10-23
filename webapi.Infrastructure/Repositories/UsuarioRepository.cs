using Dapper;
using System.Data;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;
using webapi.Core.Exceptions;

namespace webapi.Infrastructure.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(AppDbContext context, ILogger<UsuarioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CreateAsync(Usuario entity)
        {
            try
            {
                var parametros = new DynamicParameters(new
                {
                    entity.Nombre,
                    entity.ApellidoP,
                    entity.ApellidoM,
                    entity.NombreUsuario,
                    PasswordNormal = entity.PasswordHash
                });
                parametros.Add("userid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _context.Connection.ExecuteAsync("CreateNewUser", parametros, commandType: CommandType.StoredProcedure);

                return parametros.Get<int>("userid");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The user with name"))
                    throw new ConflictException(ex.Message.Split(':')[1].TrimStart(), ex);

                _logger.LogError(ex, "An error occurred during the addition of a user to the database: \n Date and time: {fecha} \n", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the registration operation.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            try
            {
                const string sql = "DELETE FROM Usuarios WHERE id = @entityId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { entityId });

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred during the user deletion operation from the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during the user deletion operation.", ex);
            }
        }

        public async Task<Usuario?> FindAsync(string username)
        {
            try
            {
                const string sql = "SELECT * FROM Usuarios WHERE nombreusuario = @username";

                return await _context.Connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { username });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a user by name: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the user.", ex);
            }
        }

        public async Task<Usuario?> FindAsync(int entityId)
        {
            try
            {
                const string sql = @"SELECT id, nombre, apellidop, apellidom, nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Usuarios WHERE id = @entityId";

                return await _context.Connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { entityId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a user by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the user.", ex);
            }
        }

        public async Task<IEnumerable<Usuario>> GetUsersAsync(int page, int quantity)
        {
            try
            {
                const string sql = @"SELECT id, nombre, apellidop, apellidom, nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Usuarios ORDER BY id DESC
                    LIMIT @quantity
		            OFFSET (@page - 1) * @quantity";

                return await _context.Connection.QueryAsync<Usuario>(sql, new { page, quantity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of users from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of users.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Usuario entity)
        {
            try
            {
                const string sql = @"UPDATE Usuario AS U
                    SET U.nombre = @Nombre, U.apellidop = @ApellidoP, U.nombreusuario = @NombreUsuario";

                var parametros = new { entity.Nombre, entity.ApellidoP, entity.ApellidoM, entity.NombreUsuario };

                int rows = await _context.Connection.ExecuteAsync(sql, parametros);

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the update of a user in the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the update of the user.", ex);
            }
        }

        public async Task<bool> UserNameExistsAsync(string username)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Usuarios WHERE nombreusuario = @username";

                int count = await _context.Connection.ExecuteScalarAsync<int>(sql, new { username });

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during a comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a comprobation.", ex);
            }
        }

        public async Task<bool> UserNameExistsAsync(string username, int userId)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Usuarios WHERE nombreusuario = @username AND id != @userid";

                int count = await _context.Connection.ExecuteScalarAsync<int>(sql, new { username, userId });

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during a comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a comprobation.", ex);
            }
        }
    }
}
