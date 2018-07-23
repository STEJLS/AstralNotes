using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Domain.Abstractions
{
    /// <summary>
    /// Сервис, предоставляющий основной функционал для работы с заметками
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Создает новую заметку
        /// </summary>
        /// <param name="theme"> Тема </param>
        /// <param name="text"> Текст </param>
        /// <param name="claims"> Claims авторизованного пользователя </param>
        /// <returns> <see cref="IUniqueImageService"/> </returns>
        Task CreateAsync(string theme, string text, ClaimsPrincipal claims);

        /// <summary>
        /// Возвращает заметку
        /// </summary>
        /// <param name="id"> Идентификатор заметки </param>
        /// <param name="claims"> Claims авторизованного пользователя </param>
        /// <returns></returns>
        Task<Note> GetAsync(int id, ClaimsPrincipal claims);

        /// <summary>
        /// Возвращет все заметки
        /// </summary>
        /// <param name="claims"> claims авторизованного пользователя </param>
        /// <returns></returns>
        Task<List<Note>> GetAllAsync(ClaimsPrincipal claims);

        /// <summary>
        /// Удаляет заметку
        /// </summary>
        /// <param name="id"> Идентификатор заметки </param>
        /// <param name="claims"> Claims авторизованного пользователя </param>
        /// <returns></returns>
        Task DeleteAsync(int id, ClaimsPrincipal claims);

        /// <summary>
        /// Поиск заметки
        /// </summary>
        /// <param name="searchString"> Строки по которой ведется поиск заметки </param>
        /// <param name="claims"> Claims авторизованного пользователя </param>
        /// <returns></returns>
        Task<List<Note>> SearchAsync(string searchString, ClaimsPrincipal claims);

        /// <summary>
        /// Редактирование заметки
        /// </summary>
        /// <param name="theme"> Новая тема заметки </param>
        /// <param name="text"> Новый текст заметки </param>
        /// <param name="id"> Идентификатор заметки </param>
        /// <param name="claims"> Claims авторизованного пользователя </param>
        /// <returns></returns>
        Task EditAsync(string theme, string text, int id, ClaimsPrincipal claims);
    }
}
