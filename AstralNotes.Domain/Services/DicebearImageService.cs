using System.Net.Http;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Models;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class DicebearImageService : IUniqueImageService
    {
        private readonly ConfigurationOptions _options;

        /// <summary>
        /// Конструктор с одним параметром ConfigurationOptions
        /// </summary>
        /// <param name="options">Параметры конфигурации</param>
        public DicebearImageService(ConfigurationOptions options)
        {
            _options = options;
        }
    
        /// <inheritdoc />
        public byte[] Get(string seed)
        {
            if (seed.Length > 300)
            {
                seed = seed.Substring(0, 300);
            }
            using (var client = new HttpClient())
            {
                return client.GetByteArrayAsync($"{_options.ImageServiceUrl}/{seed}.svg").Result;
            }
        }
    }
}
