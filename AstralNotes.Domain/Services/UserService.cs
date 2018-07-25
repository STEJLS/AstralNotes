using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService: IUserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;

        /// <summary />
        public UserService(DatabaseContext dbContext, IHashingService hashingService)
        {
            _dbContext = dbContext;
            _hashingService = hashingService;
        }

        /// <inheritdoc />
        public async Task Create(string login, string password, string email)
        {
            var user = new User(login, _hashingService.Get(password), email);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}