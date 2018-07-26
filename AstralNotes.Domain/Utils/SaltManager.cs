using System.Text;
using AstralNotes.Domain.Models;

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
        /// <param name="options">Параметры конфигурации</param>
        public SaltManager(ConfigurationOptions options)
        {
            _salt = Encoding.UTF8.GetBytes(options.Salt);
        }

        /// <summary>
        /// Возвращет соль в виде массива байт
        /// </summary>
        /// <returns></returns>
        public byte[] Get() => _salt;
    }
}
