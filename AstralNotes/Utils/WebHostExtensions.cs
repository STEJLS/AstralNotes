using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AstralNotes.Utils
{
    /// <summary>
    /// Методы расширения для IWebHost
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Миграция БД
        /// </summary>
        /// <typeparam name="TContext">Тип контекста</typeparam>
        /// <param name="host"><see cref="IWebHost"/></param>
        public static IWebHost MigrateDatabase<TContext>(this IWebHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                dbContext.Database.Migrate();
            }

            return host;
        }

        /// <summary>
        /// Выполенение операции с сервисом
        /// </summary>
        /// <typeparam name="TService">Тип сервиса</typeparam>
        /// <param name="host"><see cref="IWebHost"/></param>
        /// <param name="setUpAction">Делегат операции</param>
        /// <returns></returns>
        public static IWebHost SetUpWithService<TService>(this IWebHost host, Action<TService> setUpAction)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TService>();
                setUpAction(service);
            }

            return host;
        }
    }
}
