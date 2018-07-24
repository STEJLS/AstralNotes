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
        /// <returns> <see cref="IUniqueImageService"/> </returns>
        Task CreateAsync(string theme, string text);

        /// <summary>
        /// Возвращает заметку
        /// </summary>
        /// <param name="noteGuid"> Идентификатор заметки </param>
        /// <returns></returns>
        Task<Note> GetAsync(Guid noteGuid);

        /// <summary>
        /// Возвращет все заметки
        /// </summary>
        /// <returns></returns>
        Task<List<Note>> GetAllAsync();

        /// <summary>
        /// Удаляет заметку
        /// </summary>
        /// <param name="noteGuid"> Идентификатор заметки </param>
        /// <returns></returns>
        Task DeleteAsync(Guid noteGuid);

        /// <summary>
        /// Поиск заметки
        /// </summary>
        /// <param name="searchString"> Строки по которой ведется поиск заметки </param>
        /// <returns></returns>
        Task<List<Note>> SearchAsync(string searchString);

        /// <summary>
        /// Редактирование заметки
        /// </summary>
        /// <param name="theme"> Новая тема заметки </param>
        /// <param name="text"> Новый текст заметки </param>
        /// <param name="noteGuid"> Идентификатор заметки </param>
        /// <returns></returns>
        Task EditAsync(string theme, string text, Guid noteGuid);
    }
}
