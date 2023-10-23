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
                const string sql = "UPDATE Roles SET descripcion = @Descripcion WHERE id = @RoleId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { rol.Descripcion, RoleId = rol.Id });

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
