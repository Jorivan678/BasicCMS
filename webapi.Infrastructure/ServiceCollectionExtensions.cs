using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using webapi.Core.DTOs.Usuario;
using webapi.Core.Interfaces.Repositories;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Options;
using webapi.Infrastructure.Repositories;

namespace webapi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="AppDbContext"/> to the service container and repositories that use it.
        /// </summary>
        /// <param name="services">The service container.</param>
        /// <param name="options">Db context options.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, Action<AppDbOptions> options)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            services.AddOptions<AppDbOptions>().Configure(options);

            return services.AddScoped<AppDbContext>().AddRepositories();
        }

        /// <summary>
        /// Adds all infrastructures services like: FluentValidation and all implemented repositories.
        /// </summary>
        /// <param name="services">The service container.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            return services.AddFluentValidationAutoValidation(o => o.DisableDataAnnotationsValidation = true)
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IArticuloRepository, ArticuloRepository>()
                .AddScoped<IAuthRepository, AuthRepository>().AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<IComentarioRepository, ComentarioRepository>().AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}