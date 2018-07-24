using AstralNotes.Domain.Entities;
using AstralNotes.Domain.Models;
using AstralNotes.Identity.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Identity
{
    /// <summary>
    /// Методы расширения для регистрации сервисов Identity
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Регистрация сервисов Identity
        /// </summary>
        /// <param name="services"> <see cref="IServiceCollection"/> </param>
        /// <returns> <see cref="IServiceCollection"/> </returns>
        public static IServiceCollection AddAstralNotesIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, AccessPolicy>()
                .AddUserStore<IdentityUserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            services.AddProvider<SessionContext, SessionContextProvider>();
            
            return services;
        }
    }
}