using Dapper;
using Microsoft.Extensions.Logging;
using System.Transactions;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Repositories
{
    internal class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoriaRepository> _logger;

        public CategoriaRepository(AppDbContext context, ILogger<CategoriaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CategoryExistsAsync(string categoryName)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Categorias WHERE UPPER(nombre) = UPPER(@categoryName)";

                int count = await _context.Connection.ExecuteScalarAsync<int>(sql, new { categoryName });

                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during a category comprobation operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during a category comprobation operation.", ex);
            }
        }

        public async Task<int> CountCategoriesAsync()
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Categorias";
                return await _context.Connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of categories from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of categories count.", ex);
            }
        }

        public async Task<int> CreateAsync(Categoria entity)
        {
            try
            {
                const string sql = @"INSERT INTO Categorias(nombre, descripcion) 
                        VALUES(@Nombre, @Descripcion) RETURNING ID";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the addition of a category to the database: \n Date and time: {fecha} \n", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the addition of a category operation.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            try
            {
                const string sql = "DELETE FROM Categorias WHERE id = @entityId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { entityId });

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred during the category deletion operation from the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during the category deletion operation.", ex);
            }
        }

        public async Task<Categoria?> FindAsync(int entityId)
        {
            try
            {
                const string sql = "SELECT * FROM Categorias WHERE id = @entityId";
                return await _context.Connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { entityId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a category by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the category.", ex);
            }
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync(int page, int quantity)
        {
            try
            {
                const string sql = "SELECT * FROM Categorias ORDER BY id DESC LIMIT @quantity OFFSET (@page - 1) * @quantity";

                return await _context.Connection.QueryAsync<Categoria>(sql, new { page, quantity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of categories from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of categories.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Categoria entity)
        {
            await using var transact = await _context.Connection.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
            try
            {
                const string sql = "UPDATE Categories SET descripcion = @Descripcion WHERE id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, entity, transact);

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
                _logger.LogError(ex, "An error occurred during the category update operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the category update operation.", ex);
            }
        }
    }
}
