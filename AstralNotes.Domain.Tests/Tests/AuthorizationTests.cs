using System;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AstralNotes.Domain.Tests.Tests
{
    /// <summary>
    /// Тесты авторизации
    /// </summary>
    [TestFixture]
    public class Authorizationtests
    {
        //Для arrange и assert
        private const string Login = "TestLogin";
        private const string Password = "TestPassword";

        //Тестируемый сервис
        private readonly IAuthorizationService _authorizationService;

        /// <summary />
        public Authorizationtests()
        {
            _authorizationService = TestInitializer.Provider.GetService<IAuthorizationService>();
        }

        /// <summary>
        /// Инициализация теста
        /// </summary>
        /// <returns cref="Task"/>
        [SetUp]
        public async Task Initialize()
        {
            TestInitializer.Provider.GetService<UserDataFactory>().Create(Guid.NewGuid(), Login, Password);
            await TestInitializer.Provider.GetService<DatabaseContext>().SaveChangesAsync();
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        /// <returns cref="Task"/>
        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<DatabaseContext>().SaveChangesAsync();
        }
        
        /// <summary>
        /// Тест авторизации пользователя с корректными данными
        /// Ожидается успешное выполнение
        /// </summary>
        [Test]
        public void Authorize_WithWrongLogin_ShouldSuccess()
        {
            Assert.DoesNotThrowAsync(async () => await _authorizationService.Authorize(Login, Password));
        }
        
        /// <summary>
        /// Тест авторизации пользователя с несуществующим логином
        /// Ожидается ошибка
        /// </summary>
        [Test]
        public void Authorize_WithWrongLogin_ShouldThrowInvalidOperationException()
        {
            //arrange
            
            //act
            var exception = 
                Assert.ThrowsAsync<InvalidOperationException>(async () => await _authorizationService.Authorize("WrongLogin", Password));
            
            //assert
            Assert.AreEqual("Пользователь с указаной парой Логин/Пароль не найден.", exception.Message);
        }
        
        /// <summary>
        /// Тест авторизации пользователя с несуществующим паролем
        /// Ожидается ошибка
        /// </summary>
        [Test]
        public void Authorize_WithWrongPassword_ShouldThrowInvalidOperationException()
        {
            //arrange
            
            //act
            var exception = 
                Assert.ThrowsAsync<InvalidOperationException>(async () => await _authorizationService.Authorize(Login, "WrongPassword"));
            
            //assert
            Assert.AreEqual("Пользователь с указаной парой Логин/Пароль не найден.", exception.Message);
        }
    }
}