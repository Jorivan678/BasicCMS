using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using webapi.Application.Services;
using webapi.Application.Tools.Options;
using webapi.Core.Interfaces.Services;

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
           return services.AddTransient<ITokenService, TokenService>();
        }
    }
}