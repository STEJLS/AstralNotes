using System.Threading.Tasks;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Domain.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Authorize(string email, string password);
    }
}