using System.IO;
using AstralNotes.Domain.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    class DefaultImageService : IUniqueImageService
    {
        private readonly IHostingEnvironment _appEnvironment;

        /// <summary />
        public DefaultImageService(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        /// <inheritdoc />
        public byte[] Get(string seed)
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Defaults", "NoteImage.svg");
            byte[] image = File.ReadAllBytes(path);
            return image;
        }
    }
}
