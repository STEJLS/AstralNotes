using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstralNotes.Services
{
    public class UniqueImageService
    {
        private readonly string _url = "https://avatars.dicebear.com/v2/identicon";
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
