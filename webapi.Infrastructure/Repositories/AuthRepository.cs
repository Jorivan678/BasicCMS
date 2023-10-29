using Dapper;
using System.Data;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(AppDbContext context, ILogger<AuthRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AssignRoleAsync(int userId, int roleId)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var parametros = new DynamicParameters(new { userId, rolId = roleId });
                parametros.Add("rows_affected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _context.Connection.ExecuteAsync("AsignarRolAUsuario", parametros, transact, commandType: CommandType.StoredProcedure);

                await transact.CommitAsync();

                int rows = parametros.Get<int>("rows_affected");

                return rows > 0;
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();

                if (ex.Message.Contains("The user alredy has the role"))
                    throw new ConflictException(ex.Message.Split(':')[1].TrimStart(), ex);

                _logger.LogError(ex, "An error occurred during the assign role to a user operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the assign role to a user operation.", ex);
            }
        }

        public async Task<bool> CheckPasswordAsync(int userId, string attemptedPassword)
        {
            try
            {
                const string sql = @"SELECT (crypt(@attemptedPassword, passwordsalt) = passwordhash) as IsCorrect
                        FROM Usuarios WHERE id = @userId";

                return await _context.Connection.ExecuteScalarAsync<bool>(sql, new { userId, attemptedPassword });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during a password comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a comprobation operation.", ex);
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            try
            {
                const string sql = "SELECT * FROM ObtenerRolesPorUsuarioId(@userId)";

                return (await _context.Connection.QueryAsync<Rol>(sql, new { userId })).Select(x => x.Nombre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of roles by user from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request.", ex);
            }
        }

        public async Task<bool> RemoveRoleAsync(int userId, int roleId)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = "DELETE FROM Usuarios_Roles WHERE usuarioid = @userId AND roleid = @roleId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { userId, roleId }, transact);

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
                _logger.LogError(ex, "An error occurred during the remove role from a user operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the remove role from a user operation.", ex);
            }
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        { 
            await using var transact = await _context.Connection.BeginTransactionAsync();
            try
            {
                var parametros = new DynamicParameters(new { userid = userId, newpassword = newPassword });
                parametros.Add("rows_affected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _context.Connection.ExecuteAsync("UpdateUserPassword", parametros, transact, commandType: CommandType.StoredProcedure);

                int rows = parametros.Get<int>("rows_affected");

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
                _logger.LogError(ex, "An error occurred during the update user password operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the update user password operation.", ex);
            }
        }
    }
}
