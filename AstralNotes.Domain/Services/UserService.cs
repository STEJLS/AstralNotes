using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService: IUserService
    {
        private readonly DatabaseContext _dbContext;

        /// <summary />
        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task Create(string login, string password, string email)
        {
            var user = new User(login, password, email);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}