using System;
using AstralNotes.Database;
using AstralNotes.Domain.Models;
using AstralNotes.Domain.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace AstralNotes.Domain.Tests.Tests
{
    /// <summary>
    /// Инициализация тестов
    /// </summary>
    [SetUpFixture]
    public class TestInitializer
    {
        /// <summary>
        /// Интерфейс управления механизом для извлечения объекта службы(объекта, обеспечивающего настраиваемую поддержку
        /// для других объектов).
        /// </summary>
        public static IServiceProvider Provider { get; private set; }
        
        /// <summary>
        /// Регистрация необходимых сервисов
        /// </summary>
        [OneTimeSetUp]
        public static void Initialize()
        {
            var services = new ServiceCollection();
            
            //DbContexts
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("AstralNotes"));

            //Логика
            services.AddDomainServices();
            services.AddDomainUtilsStub(options =>
            {
                options.Salt = "Salt";
                options.ImageServiceUrl = "";
            });
            
            //DataFactories
            services.AddDataFactories();
            
            //Session контекст
            services.AddScoped<SessionContext>();
            
            Provider = services.BuildServiceProvider();
        }
        
        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        [OneTimeTearDown]
        public static void Cleanup()
        {
            Provider.GetService<DatabaseContext>().Database.EnsureDeleted();
        }
    }
}