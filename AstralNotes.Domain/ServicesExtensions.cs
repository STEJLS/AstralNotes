using System;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Models;
using AstralNotes.Domain.Services;
using AstralNotes.Domain.Utils;
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
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddSingleton<SaltManager>();
            
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setUp"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainUtils(this IServiceCollection services, Action<ConfigurationOptions> setUp)
        {
            var configurationOptions = new ConfigurationOptions();
            setUp(configurationOptions);
            services.AddSingleton(configurationOptions);
            services.AddScoped<IUniqueImageService, DicebearImageService>();
            
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setUp"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainUtilsStub(this IServiceCollection services, Action<ConfigurationOptions> setUp)
        {
            var configurationOptions = new ConfigurationOptions();
            setUp(configurationOptions);
            services.AddSingleton(configurationOptions);
            services.AddScoped<IUniqueImageService, DefaultImageService>();
           
            return services;
        }
    }
}