using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid NoteGuid { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }
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
        
        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }

        /// <summary />
        public Note()
        {
            
        }

        /// <summary />
        public Note(Guid userGuid, string theme, string text, byte[] image)
        {
            NoteGuid = Guid.NewGuid();
            UserGuid = userGuid;
            Theme = theme;
            Text = text;
            Image = image;
        }
    }
}