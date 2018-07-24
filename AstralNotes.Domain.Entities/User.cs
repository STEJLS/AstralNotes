using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralNotes.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Тема
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Текст
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        public string Email { get; set; }

        /// <summary />
        public User()
        {
            
        }
        
        /// <summary />
        public User(string login, string password, string email)
        {
            UserGuid = Guid.NewGuid();
            Login = login;
            Password = password;
            Email = email;
        }
    }
}