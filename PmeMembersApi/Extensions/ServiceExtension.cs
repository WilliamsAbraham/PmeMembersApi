using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using LoggerManager;
using System.Runtime.CompilerServices;
using Repository;

namespace PmeMembersApi.Extensions
{
    public static class ServiceExtension
    {

        public static void ConfigureCore(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("CoresPolicy", buider => buider.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
        }


        public static void ConfigureIISOptions(this IServiceCollection service) 
        {
            service.Configure<IISOptions>(option =>
            {

            });
        }

        public static void ConfigureSqlConnection(this IServiceCollection service, IConfiguration config) 
        {
            var connection = config.GetConnectionString("SqlConnection");
            service.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlServer(connection);
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerService>();
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)=>services.AddScoped<IRepositoryWrapper,RepositoryWrappers>();
    }
}
