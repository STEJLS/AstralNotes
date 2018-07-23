using System.Net.Http;
using AstralNotes.Domain.Abstractions;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class DicebearImageService : IUniqueImageService
    {
        private readonly string _url = "https://avatars.dicebear.com/v2/identicon";

        /// <inheritdoc />
        public byte[] Get(string seed)
        {
            if (seed.Length > 300)
            {
                seed = seed.Substring(0, 300);
            }
            using (var client = new HttpClient())
            {
                return client.GetByteArrayAsync($"{_url}/{seed}.svg").Result;
            }
        }
    }
}
