using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Identity.DependencyInjection
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация провайдера для типа
        /// </summary>
        /// <param name="services"> <see cref="IServiceCollection"/> </param>
        /// <typeparam name="TType"> Тип объектов отдаваемых провайдером </typeparam>
        /// <typeparam name="TProvider"> Тип провайдера </typeparam>
        /// <returns> <see cref="IServiceCollection"/> </returns>
        public static IServiceCollection AddProvider<TType, TProvider>(this IServiceCollection services)
            where TProvider : class, IProvider<TType> where TType : class
        {
            services.AddScoped<TProvider>();
            services.AddScoped(a => a.GetService<TProvider>().Get());

            return services;
        }
    }
}