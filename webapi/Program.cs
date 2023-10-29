using webapi.Infrastructure;
using webapi.Infrastructure.Filters;

namespace webapi
{
    internal static class Program
    {
        private static WebApplication ConfigureBuilder(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<InputValidationFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAppDbContext(x => x.SetConnStrKey("DbConnection"));
            builder.Services.AddInfrastructureServices();

            return builder.Build();
        }

        private static void Main(string[] args)
        {
            var app = ConfigureBuilder(WebApplication.CreateBuilder(args));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
