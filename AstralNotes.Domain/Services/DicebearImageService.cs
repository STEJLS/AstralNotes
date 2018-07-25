using System.Net.Http;
using AstralNotes.Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class DicebearImageService : IUniqueImageService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструктор с одним параметром IConfiguration
        /// </summary>
        /// <param name="configuration"> Объект конфигурации</param>
        public DicebearImageService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                return client.GetByteArrayAsync($"{_configuration["ImageServiceUrl"]}/{seed}.svg").Result;
            }
        }
    }
}
