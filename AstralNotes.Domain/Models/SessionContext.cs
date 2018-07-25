using System;
using System.Diagnostics.CodeAnalysis;
using AstralNotes.Domain.Entities;
using Newtonsoft.Json;

namespace AstralNotes.Domain.Models
{
    /// <summary>
    /// Контекст сессии
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SessionContext
    {
        /// <summary>
        /// Авторизованы ли мы
        /// </summary>
        [JsonProperty("isAuthorized")]
        public bool Authorized { get; set; }
        /// <summary>
        /// Идентификатор юзера
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary/>
        public SessionContext()
        {
            Authorized = false;
        }

        /// <summary/>
        public SessionContext(User user)
        {
            Authorized = true;
            UserGuid = user.UserGuid;
            Login = user.Login;
            Email = user.Email;
        }
    }
}