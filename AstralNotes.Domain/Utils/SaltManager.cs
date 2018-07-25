using Microsoft.Extensions.Configuration;
using System.Text;

namespace AstralNotes.Domain.Utils
{
    /// <summary>
    /// Класс, предоставляющий общий доступ к переменной "Salt" конфига в виде массива байт
    /// </summary>
    public class SaltManager
    {
        private readonly byte[] _salt;

        /// <summary>
        /// Конструктор, принимающий один параметр объект конфигурации
        /// </summary>
        /// <param name="configuration"> Объект конфигурации </param>
        public SaltManager(IConfiguration configuration)
        {
            _salt = Encoding.UTF8.GetBytes(configuration["Salt"]);
        }

        /// <summary>
        /// Возвращет соль в виде массива байт
        /// </summary>
        /// <returns></returns>
        public byte[] Get() => _salt;
    }
}
