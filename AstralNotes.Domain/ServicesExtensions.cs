using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Services;
using AstralNotes.Domain.Utils;
using Microsoft.AspNetCore.Hosting;
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
        /// <param name="services"> Коллекция сервисов </param>
        /// <returns>Коллекция сервисов с добавленными сервисами менеджмента</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            IHostingEnvironment env = services.BuildServiceProvider().GetService<IHostingEnvironment>();

            if (env.IsDevelopment())
            {
                services.AddScoped<IUniqueImageService, DefaultImageService>();
            }

            if (env.IsProduction())
            {
                services.AddScoped<IUniqueImageService, DicebearImageService>();
            }

            services.AddSingleton<SaltManager>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            return services;
        }
    }
}