using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public static class SwaggerGenConfigExtensions
    {
        /// <summary>
        /// Sets the api documentation.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        /// <param name="description"></param>
        /// <returns>A reference of the same instance.</returns>
        public static SwaggerGenOptions SetApiDoc(this SwaggerGenOptions options, string title, string version, string description = "")
        {
            options.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version, Description = description });
            options.ResolveConflictingActions(apiDes => apiDes.FirstOrDefault());

            return options;
        }

        /// <summary>
        /// Sets the configuration of security definition for a JWT Bearer.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>A reference of the same instance.</returns>
        public static SwaggerGenOptions SetBearerSecDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Token de Autorización. Este debe estar en el header y se obtiene mediante el inicio de sesión con usuario y contraseña válidos.",
                Type = SecuritySchemeType.Http,
                Name = "Authorization",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new List<string>()
                }
            });

            return options;
        }
    }
}
