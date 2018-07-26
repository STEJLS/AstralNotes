namespace AstralNotes.Domain.Models
{
    /// <summary>
    /// Параметры конфигурации
    /// </summary>
    public class ConfigurationOptions
    {
        /// <summary>
        /// URL сервиса для получения изображений
        /// </summary>
        public string ImageServiceUrl { get; set; }
        /// <summary>
        /// Соль хеширования паролей
        /// </summary>
        public string Salt { get; set; }
    }
}