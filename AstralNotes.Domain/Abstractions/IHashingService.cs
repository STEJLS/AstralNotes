using System;
using System.Collections.Generic;
using System.Text;

namespace AstralNotes.Domain.Abstractions
{    /// <summary>
     /// Сервис хэширования
     /// </summary>
    public interface IHashingService
    {
        /// <summary>
        /// Возвращает хэш в строки
        /// </summary>
        /// <param name="password"> Строка для хэширования </param>
        /// <returns></returns>
        string Get(string password);
    }
}
