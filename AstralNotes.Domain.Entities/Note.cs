using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Domain.Entities
{
    /// <summary>
    /// Заметка
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key] 
        public int Id { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public IdentityUser User { get; set; }
        /// <summary>
        /// Тема
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; set; }
    }
}