using AstralNotes.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstralNotes.Domain.Tests.Services
{
    class StubImageService : IUniqueImageService
    {
        public byte[] Get(string seed)
        {
            return new byte[] { 1, 2, 3, 4, 5 };
        }
    }
}
