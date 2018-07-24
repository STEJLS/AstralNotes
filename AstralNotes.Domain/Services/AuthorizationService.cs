using System;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Entities;
using AstralNotes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class AuthorizationService: IAuthorizationService
    {
        private readonly DatabaseContext _dbContext;
        private readonly SessionContext _sessionContext;

        /// <summary />
        public AuthorizationService(DatabaseContext dbContext, SessionContext sessionContext)
        {
            _dbContext = dbContext;
            _sessionContext = sessionContext;
        }
        
        /// <inheritdoc />
        public async Task<User> Authorize(string login, string password)
        {
            login = login.Trim().ToLower();

            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Login.ToLower() == login && a.Password == password);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с указаной парой Логин/Пароль не найден.");
            }
            
            return user;
        }
    }
}