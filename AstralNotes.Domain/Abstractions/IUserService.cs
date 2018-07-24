using System.Threading.Tasks;

namespace AstralNotes.Domain.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task Create(string login, string password, string email);
    }
}