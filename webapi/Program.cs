using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using webapi.Application;
using webapi.Infrastructure;
using webapi.Infrastructure.Filters;

namespace webapi
{
    internal static class Program
    {
        private static readonly string _version = "1.0";
        private static readonly string _apiName = "Basic CMS";

        private static WebApplication ConfigureBuilder(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<InputValidationFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o => o.SetApiDoc(_apiName, _version, "A very basic cms just for an api demonstration.")
                .SetBearerSecDefinition());
            //Infrastructure services
            builder.Services.AddAppDbContext(o 
                => o.SetConnectionString(builder.Configuration.GetConnectionString("DbConnection")));
            builder.Services.AddInfrastructureServices();
            //Application services
            builder.Services.AddTokenService(o => o.SetSectionName("JWT"));
            builder.Services.AddAuthJwtBearer(tokenVal =>
            {
                tokenVal.ValidateIssuer = builder.Configuration.GetValue<bool>("JWT:ValidateIssuer");
                tokenVal.ValidIssuer = builder.Configuration["JWT:Issuer"];
                tokenVal.ValidateAudience = false;
                tokenVal.ValidateLifetime = true;
                tokenVal.ValidateIssuerSigningKey = true;
                tokenVal.IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(builder.Configuration["JWT:SecretKey"]!));
            }, false, builder.Configuration["JWT:Authority"]);
            builder.Services.AddApplicationServices();

            return builder.Build();
        }

        private static void Main(string[] args)
        {
            var app = ConfigureBuilder(WebApplication.CreateBuilder(args));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint($"{_version}/swagger.json", $"{_apiName} (API) {_version}"));
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
