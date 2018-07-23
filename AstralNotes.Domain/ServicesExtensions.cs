using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Domain
{
    /// <summary>
    /// Методы расширения для регистрации сервисов управления
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Регистрация конечной реализации сервисов управления
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов с добавленными сервисами менеджмента</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUniqueImageService, DicebearImageService>();
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}