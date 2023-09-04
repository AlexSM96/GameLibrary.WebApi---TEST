using GameLibrary.Application.Interfaces;
using GameLibrary.Application.Services.Base;
using GameLibrary.Application.Services.GameEnityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.DAL.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSSQL");
            services.AddDbContext<GameLibraryDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            return services
                .AddScoped<IBaseDbContext, GameLibraryDbContext>()
                .AddScoped<IGameService, GameService>();
        }
    }
}
