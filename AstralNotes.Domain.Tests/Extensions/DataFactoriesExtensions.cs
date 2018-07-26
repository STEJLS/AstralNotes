using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Domain.Tests.Extensions
{
    /// <summary>
    /// Методы расширения для регистрации DataFactories
    /// </summary>
    public static class DataFactoriesExtensions
    {
        /// <summary>
        /// Регистрация DataFactories
        /// </summary>
        /// <param name="services" cref="IServiceCollection" /> 
        /// <returns cref="IServiceCollection" />
        public static IServiceCollection AddDataFactories(this IServiceCollection services)
        { 
            services.AddScoped<UserDataFactory>();
            services.AddScoped<NoteDataFactory>();

            return services;
        }
    }
}