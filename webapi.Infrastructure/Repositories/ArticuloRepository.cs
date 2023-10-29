using Dapper;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;
using System.Data;
using System.Data.Common;

namespace webapi.Infrastructure.Repositories
{
    internal class ArticuloRepository : IArticuloRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ArticuloRepository> _logger;

        public ArticuloRepository(AppDbContext context, ILogger<ArticuloRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CreateAsync(Articulo entity)
        {
            try
            {
                const string sql = @"INSERT INTO Articulos(titulo, contenido, fecha_pub, autorid)
                    VALUES(@Titulo, @Contenido, @Fecha_Pub, @AutorId) RETURNING ID";

                var newId = await _context.Connection.ExecuteScalarAsync<int>(sql, entity);

                await AddRelationsAsync(newId);

                return newId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the add of an article to the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the add of the article.", ex);
            }

            async Task AddRelationsAsync(int artId)
            {
                if (entity.Categorias.Count > 0)
                {
                    const string sql = "INSERT INTO articulos_categorias(articuloid, categoriaid) VALUES(@artId, @catId)";

                    await using var transaction = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
                    try
                    {
                        await _context.Connection.ExecuteAsync(sql, entity.Categorias.Select(x => new { artId, catId = x.Id }), transaction: transaction);
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }

                    await transaction.CommitAsync();
                }
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            try
            {
                const string sql = "DELETE FROM Articulos WHERE id = @entityId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { entityId });

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred during the article deletion operation from the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during the article deletion operation.", ex);
            }
        }

        public async Task<Articulo?> FindAsync(int entityId)
        {
            try
            {
                const string sql = @"SELECT AR.*, false as split, u.id, '' as nombre, '' as apellidop, '' as apellidom, u.nombreusuario, false as split, C.* 
                        FROM Articulos AS AR 
                        LEFT JOIN Usuarios AS U ON U.id = AR.autorid
                        LEFT JOIN articulos_categorias AS AC ON AR.id = AC.articuloid
                        LEFT JOIN categorias AS C ON C.id = AC.categoriaid
                        WHERE AR.id = @entityId";

                var lookup = new Dictionary<int, Articulo>();

                var articulo = (await _context.Connection.QueryAsync<Articulo, Usuario, Categoria, Articulo>(sql, (ar, u, c) => 
                {
                    if (!lookup.TryGetValue(ar.Id, out var articulo))
                    {
                        lookup[ar.Id] = articulo = ar;
                        articulo.Autor = u;
                        articulo.Categorias = new List<Categoria>();
                    }

                    if (c != null)
                    {
                        articulo.Categorias.Add(c);
                    }

                    return articulo;
                }, new { entityId }, splitOn: "split")).FirstOrDefault();

                lookup.Clear();

                return articulo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a user by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the user.", ex);
            }
        }

        public async Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity)
        {
            try
            {
                const string sql = "SELECT * FROM public.obtenerarticulospaginados(@page, @quantity)";

                var articulos = await _context.Connection.QueryAsync<Articulo, Usuario, Articulo>(sql, (ar, u) =>
                {
                    u.Id = ar.AutorId;
                    ar.Autor = u;
                    ar.Categorias = new List<Categoria>();
                    return ar;
                }, new { page, quantity }, splitOn: "split");

                foreach (var articulo in articulos)
                {
                    var categorias = await GetCategoriasAsync(articulo.Id);

                    (articulo.Categorias as List<Categoria>)!.AddRange(categorias);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of articles.", ex);
            }
        }

        public async Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, int authorId)
        {
            try
            {
                const string sql = "SELECT * FROM public.obtenerarticulospaginados(@page, @quantity, @authorId)";

                var articulos = await _context.Connection.QueryAsync<Articulo, Usuario, Articulo>(sql, (ar, u) =>
                {
                    u.Id = ar.AutorId;
                    ar.Autor = u;
                    ar.Categorias = new List<Categoria>();
                    return ar;
                }, new { page, quantity, authorId }, splitOn: "split");

                foreach (var articulo in articulos)
                {
                    var categorias = await GetCategoriasAsync(articulo.Id);

                    (articulo.Categorias as List<Categoria>)!.AddRange(categorias);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of articles.", ex);
            }
        }

        public async Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, string[] categories)
        {
            try
            {
                const string sql = "SELECT * FROM public.obtenerarticulospaginados(@page, @quantity, 0, UPPER(@categories))";

                var articulos = await _context.Connection.QueryAsync<Articulo, Usuario, Articulo>(sql, (ar, u) =>
                {
                    u.Id = ar.AutorId;
                    ar.Autor = u;
                    ar.Categorias = new List<Categoria>();
                    return ar;
                }, new { page, quantity, categories }, splitOn: "split");

                foreach (var articulo in articulos)
                {
                    var categorias = await GetCategoriasAsync(articulo.Id);

                    (articulo.Categorias as List<Categoria>)!.AddRange(categorias);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of articles.", ex);
            }
        }

        public async Task<IEnumerable<Articulo>> GetArticulosAsync(int page, int quantity, string[] categories, int authorId)
        {
            try
            {
                const string sql = "SELECT * FROM public.obtenerarticulospaginados(@page, @quantity, @authorId, UPPER(@categories))";

                var articulos = await _context.Connection.QueryAsync<Articulo, Usuario, Articulo>(sql, (ar, u) =>
                {
                    u.Id = ar.AutorId;
                    ar.Autor = u;
                    ar.Categorias = new List<Categoria>();
                    return ar;
                }, new { page, quantity, authorId, categories }, splitOn: "split");

                foreach (var articulo in articulos)
                {
                    var categorias = await GetCategoriasAsync(articulo.Id);

                    (articulo.Categorias as List<Categoria>)!.AddRange(categorias);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of articles.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Articulo entity)
        {
            await using var transaction = await _context.Connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                const string sql = @"UPDATE Articulos SET titulo = @Titulo, contenido = @Contenido WHERE id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, entity, transaction);

                if (rows <= 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // Ahora, actualiza las categorías del artículo
                await UpdateCategoriasAsync(transaction);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred during the article update operation: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the article update operation.", ex);
            }

            async Task UpdateCategoriasAsync(DbTransaction transaction)
            {
                // Obtén las categorías actuales del artículo
                var currentCategorias = await GetCategoriasAsync(entity.Id);

                // Encuentra las categorías que deben ser agregadas
                var categoriasToAdd = entity.Categorias.Except(currentCategorias);

                // Encuentra las categorías que deben ser eliminadas
                var categoriasToRemove = currentCategorias.Except(entity.Categorias);

                // Agrega nuevas categorías
                if (categoriasToAdd.Any())
                {
                    const string insertCategoriaSql = @"INSERT INTO articulos_categorias(articuloid, categoriaid) VALUES (@ArticuloId, @CategoriaId)";
                    await _context.Connection.ExecuteAsync(insertCategoriaSql, categoriasToAdd.Select(c => new { ArticuloId = entity.Id, CategoriaId = c.Id }), transaction);
                }

                // Elimina categorías obsoletas
                if (categoriasToRemove.Any())
                {
                    const string deleteCategoriaSql = @"DELETE FROM articulos_categorias WHERE articuloid = @ArticuloId AND categoriaid = @CategoriaId";
                    await _context.Connection.ExecuteAsync(deleteCategoriaSql, categoriasToRemove.Select(c => new { ArticuloId = entity.Id, CategoriaId = c.Id }), transaction);
                }
            }
        }

        public async Task<int> CountArticulosAsync()
        {
            try
            {
                const string sql = "SELECT * FROM ObtenerCantidadArticulos()";

                return await _context.Connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of articles count.", ex);
            }
        }

        public async Task<int> CountArticulosAsync(int authorId)
        {
            try
            {
                const string sql = "SELECT * FROM ObtenerCantidadArticulos(@authorId)";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, new { authorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of articles count.", ex);
            }
        }

        public async Task<int> CountArticulosAsync(string[] categories)
        {
            try
            {
                const string sql = "SELECT * FROM ObtenerCantidadArticulos(0, UPPER(@categories))";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, new { categories });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of articles count.", ex);
            }
        }

        public async Task<int> CountArticulosAsync(string[] categories, int authorId)
        {
            try
            {
                const string sql = "SELECT * FROM ObtenerCantidadArticulos(@authorId, UPPER(@categories))";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, new { authorId, categories });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of articles from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of articles count.", ex);
            }
        }

        /// <summary>
        /// Obtains categories related with an article.
        /// </summary>
        /// <param name="articuloId">Article related.</param>
        /// <returns>A list of categories.</returns>
        private async Task<IEnumerable<Categoria>> GetCategoriasAsync(int articuloId)
        {
            const string sql = "SELECT * FROM ObtenerCategoriasPorArticuloId(@articuloId)";

            return await _context.Connection.QueryAsync<Categoria>(sql, new { articuloId });
        }
    }
}
