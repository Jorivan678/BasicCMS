using Dapper;
using System.Data;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;
using webapi.Core.Exceptions;
using System.Data.Common;
using webapi.Core.StaticData;

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
            await using var transact = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var parametros = new DynamicParameters(new
                {
                    nombre = entity.Nombre,
                    apellidop = entity.ApellidoP,
                    apellidom = entity.ApellidoM,
                    nombre_usuario = entity.NombreUsuario,
                    passwordnormal = entity.PasswordHash
                });
                parametros.Add("userid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _context.Connection.ExecuteAsync("CreateNewUser", parametros, transact, commandType: CommandType.StoredProcedure);

                await AssignRoleAsync(parametros.Get<int>("userid"), transact);

                await transact.CommitAsync();

                return parametros.Get<int>("userid");
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                if (ex.Message.Contains("The user with name"))
                    throw new ConflictException(ex.Message.Split(':')[1].TrimStart(), ex);

                _logger.LogError(ex, "An error occurred during the addition of a user to the database: \n Date and time: {fecha} \n", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the registration operation.", ex);
            }

            async Task AssignRoleAsync(int userid, DbTransaction transact)
            {
                const string sql = "SELECT COUNT(*) FROM Usuarios WHERE id != @userid";
                const string sqlRole = "SELECT R.id FROM Roles AS R WHERE R.nombre = @rolName";
                const string spName = "AsignarRolAUsuario";

                var rows = await _context.Connection.ExecuteScalarAsync<long>(sql, new { userid });

                if (rows > 0)
                {
                    int rolid = await _context.Connection.ExecuteScalarAsync<int>(sqlRole, new { rolName = Roles.User });

                    var parametros = new DynamicParameters(new { userid, rolid });
                    parametros.Add("rows_affected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await _context.Connection.ExecuteAsync(spName, parametros, transact, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    int rolAdminId = await _context.Connection.ExecuteScalarAsync<int>(sqlRole, new { rolName = Roles.Admin });
                    int rolUserId = await _context.Connection.ExecuteScalarAsync<int>(sqlRole, new { rolName = Roles.User });

                    foreach (var rolid in new int[] { rolAdminId, rolUserId } )
                    {
                        var parametros = new DynamicParameters(new { userid, rolid });
                        parametros.Add("rows_affected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        await _context.Connection.ExecuteAsync(spName, parametros, transact, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            await using var transact = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = "DELETE FROM Usuarios WHERE id = @entityId";

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

        public async Task<Usuario?> FindAsync(string username)
        {
            try
            {
                const string sql = @"SELECT id, nombre, apellidop, apellidom, nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Usuarios WHERE nombreusuario = @username";

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

        public async Task<IEnumerable<Usuario>> GetUsersAuthorsAsync()
        {
            try
            {
                const string sql = @"SELECT U.id, '' as nombre, '' as apellidop, '' as apellidom, U.nombreusuario, '' as passwordhash, '' as passwordsalt
                        FROM Usuarios AS U
                        INNER JOIN Usuarios_Roles AS UR ON U.id = UR.usuarioid
                        INNER JOIN Roles AS R ON R.id = UR.roleid
						WHERE R.nombre = @roleName
                        ORDER BY U.id DESC;";

                return await _context.Connection.QueryAsync<Usuario>(sql, new { roleName = Roles.Editor });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of users from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of users.", ex);
            }
        }

        public async Task<int> CountUsersAsync()
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Usuarios";
                return await _context.Connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of users from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of users count.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Usuario entity)
        {
            try
            {
                const string sql = @"UPDATE Usuario AS U
                    SET U.nombre = @Nombre, U.apellidop = @ApellidoP, U.nombreusuario = @NombreUsuario
                    WHERE U.id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, entity);

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
                _logger.LogError(ex, "An error occurred during a user comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a comprobation operation.", ex);
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
                _logger.LogError(ex, "An error occurred during a user comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a comprobation operation.", ex);
            }
        }
    }
}
