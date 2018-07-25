using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AstralNotes.Domain.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace AstralNotes.Domain.Services
{
    class DefaultImageService : IUniqueImageService
    {
        private readonly IHostingEnvironment _appEnvironment;

        public DefaultImageService(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }
        public byte[] Get(string seed)
        {
            string path = Path.Combine(_appEnvironment.ContentRootPath, "Defaults", "NoteImage.svg");
            byte[] image = System.IO.File.ReadAllBytes(path);
            return image;
        }
    }
}
