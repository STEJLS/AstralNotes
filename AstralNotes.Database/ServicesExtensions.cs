using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Database
{
    /// <summary>
    /// Методы расширения для регистрации сервисов данных 
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Регистрация сервисов слоя доступа к данным
        /// </summary>
        /// <param name="services"> <see cref="IServiceCollection"/> </param>
        /// <param name="connectionString"> Строка подключения к БД </param>
        /// <returns> <see cref="IServiceCollection"/> </returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(connectionString, o => o.MigrationsAssembly("AstralNotes"));
            });

            return services;
        }
    }
}