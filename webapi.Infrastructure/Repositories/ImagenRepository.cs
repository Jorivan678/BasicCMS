using Dapper;
using Microsoft.Extensions.Logging;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;

namespace webapi.Infrastructure.Repositories
{
    internal class ImagenRepository : IImagenRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ImagenRepository> _logger;

        public ImagenRepository(AppDbContext context, ILogger<ImagenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CountImagesAsync()
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM Imagenes";

                return await _context.Connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the count of images from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of images count.", ex);
            }
        }

        public async Task<int> CreateAsync(Imagen entity)
        {
            try
            {
                const string sql = @"INSERT INTO Imagenes(titulo, ruta, alto, ancho, fecha_subida, usuarioid)
                            VALUES(@Titulo, @Ruta, @Alto, @Ancho, @Fecha_Subida, @UsuarioId) RETURNING ID";

                return await _context.Connection.ExecuteScalarAsync<int>(sql, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the addition of a image to the database: \n Date and time: {fecha} \n", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the image addition operation.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            try
            {
                const string sql = "DELETE FROM Images WHERE id = @entityId";

                int rows = await _context.Connection.ExecuteAsync(sql, new { entityId });

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred during the image deletion operation from the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error ocurred during the image deletion operation.", ex);
            }
        }

        public async Task<Imagen?> FindAsync(int entityId)
        {
            try
            {
                const string sql = @"SELECT I.*, false as split, U.id, '' as nombre, '' as apellidop, '' as apellidom, U.nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Imagenes AS I
                    LEFT JOIN Usuarios AS U ON U.id = I.usuarioid
                    WHERE id = @entityId";

                return (await _context.Connection.QueryAsync<Imagen, Usuario, Imagen>(sql, (img, user) =>
                {
                    img.Usuario = user;
                    return img;
                }, new { entityId }, splitOn: "split")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of a image by id: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during searching the image.", ex);
            }
        }

        public async Task<IEnumerable<Imagen>> GetImagesAsync(int page, int quantity)
        {
            try
            {
                const string sql = @"SELECT I.*, false as split, U.id, U.nombre, U.apellidop, U.apellidom, U.nombreusuario, '' as passwordhash, '' as passwordsalt
                    FROM Imagenes AS I
                    LEFT JOIN Usuarios AS U ON U.id = I.usuarioid";

                return await _context.Connection.QueryAsync<Imagen, Usuario, Imagen>(sql, (img, user) =>
                {
                    img.Usuario = user;
                    return img;
                }, splitOn: "split");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the query of images from database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the request of a list of images.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Imagen entity)
        {
            try
            {
                const string sql = "UPDATE Images SET titulo = @Titulo WHERE id = @Id";

                int rows = await _context.Connection.ExecuteAsync(sql, entity);

                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the update of a image title in the database: \n Date and time: {fecha}", DateTimeOffset.UtcNow.ToString("G"));
                throw new Exception("An error occurred during the update of the image title.", ex);
            }
        }
    }
}
