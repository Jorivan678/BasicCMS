using Dapper;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Repositories
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(AppDbContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Rol?> FindAsync(int roleId)
        {
            try
            {
                const string sql = "SELECT * FROM Roles WHERE id = @roleId";

                return await _context.Connection.QueryFirstOrDefaultAsync<Rol>(sql, new { roleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a role by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the role.", ex);
            }
        }

        public async Task<IEnumerable<Rol>> GetRolesAsync()
        {
            try
            {
                const string sql = "SELECT * FROM Roles";

                return await _context.Connection.QueryAsync<Rol>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of roles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of roles.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Rol rol)
        {
            try
            {
                const string sql = "UPDATE Roles SET descripcion = @Descripcion WHERE id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, rol);

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the role description update in the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the role description update.", ex);
            }
        }
    }
}
