using System;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Tests.Extensions
{
    /// <summary>
    /// Заполнение БД юзерам
    /// </summary>
    public class UserDataFactory
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IHashingService _hashingService;

        /// <summary />
        public UserDataFactory(DatabaseContext databaseContext, IHashingService hashingService)
        {
            _databaseContext = databaseContext;
            _hashingService = hashingService;
        }

        /// <summary>
        /// Создние тестового юзера
        /// </summary>
        public void Create(Guid? userGuid = null, string login = "login", string password = "password",
            string email = "email@email.ru")
        {
            var guid = userGuid ?? Guid.NewGuid();
            var user = new User
            {
                UserGuid = userGuid ?? Guid.NewGuid(),
                Login = login,
                Password = _hashingService.Get(password),
                Email = email
            };

            _databaseContext.Users.Add(user);
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        /// <returns cref="Task" />
        public async Task Dispose()
        {
            var users = await _databaseContext.Users.ToListAsync();
            _databaseContext.Users.RemoveRange(users);
        }
    }
}