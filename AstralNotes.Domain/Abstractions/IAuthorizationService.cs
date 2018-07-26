using System.Threading.Tasks;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Domain.Abstractions
{
    /// <summary>
    /// Сервис, предоставляющий основной функционал для работы с авторизацией
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        Task<User> Authorize(string login, string password);
    }
}