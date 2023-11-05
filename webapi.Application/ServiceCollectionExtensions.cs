using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using webapi.Application.Services;
using webapi.Application.Tools.Options;
using webapi.Core.Interfaces.Services;
using webapi.Application.Tools.Implementations;
using webapi.Application.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace webapi.Application
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds token service and configure the <see cref="IConfiguration"/> section that will take for create a token.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddTokenService(this IServiceCollection services, Action<TokenServiceOptions> options)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            services.AddOptions<TokenServiceOptions>().Configure(options);
           return services.AddScoped<ITokenService, TokenService>();
        }

        /// <summary>
        /// Adds all application services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            return services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly)
                .AddServices().AddHttpContextAccessor().AddScoped<IAuthValidator, AuthValidator>();
        }

        /// <summary>
        /// Adds JWT Bearer configuration for user authentication.
        /// </summary>
        /// <param name="services">The collection of services to extend.</param>
        /// <param name="requireHttps">Sets if HTTPS is needed to authenticate.</param>
        /// <param name="authority">The internet domain issuing the token.</param>
        /// <param name="valParameters">Token validation parameters.</param>
        /// <returns>A reference to the same service collection.</returns>
        public static IServiceCollection AddAuthJwtBearer(this IServiceCollection services, Action<TokenValidationParameters> valParameters, bool requireHttps = true, string? authority = null)
        {
            ArgumentNullException.ThrowIfNull(valParameters, nameof(valParameters));

            var parameters = new TokenValidationParameters();
            valParameters(parameters);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = authority;
                o.RequireHttpsMetadata = requireHttps;
                o.SaveToken = true;
                o.TokenValidationParameters = parameters;
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<IArticuloService, ArticuloService>().AddScoped<IAuthService, AuthService>()
            .AddScoped<ICategoriaService, CategoriaService>().AddScoped<IComentarioService, ComentarioService>()
            .AddScoped<IRoleService, RoleService>().AddScoped<IUsuarioService, UsuarioService>();
    }
}