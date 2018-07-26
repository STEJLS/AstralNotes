using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using AstralNotes.Domain.Tests.Extensions;
using AstralNotes.Database;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Tests.Tests
{
    class UserTests
    {
        //Для arrange и assert
        private const string _login = "TestLogin";
        private const string _password = "TestPassword";
        private const string _email = "Test@mail.ru";

        //Тестируемый сервис
        private readonly IUserService _userService;

        public UserTests()
        {
            _userService = TestInitializer.Provider.GetService<IUserService>();
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

        [Test]
        public async Task Create_WithRightInputs_ShouldSuccess()
        {
            Assert.DoesNotThrowAsync(async () => await _userService.Create(_login, _password, _email));
            var resultUser = await TestInitializer.Provider.GetService<DatabaseContext>().Users.FirstAsync(u => u.Login == _login);

            Assert.NotNull(resultUser);
            Assert.NotNull(resultUser.Password);
            Assert.NotNull(resultUser.Email);
            Assert.AreEqual(TestInitializer.Provider.GetService<IHashingService>().Get(_password), resultUser.Password);
            Assert.AreEqual(_email, resultUser.Email);
        }


    }
}
