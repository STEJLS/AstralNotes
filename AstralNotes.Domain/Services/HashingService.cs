using AstralNotes.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;
using AstralNotes.Domain.Utils;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class HashingService : IHashingService
    {
        private readonly SaltManager _saltManager;

        /// <summary>
        /// Конструктор с одним параметром IConfiguration
        /// </summary>
        /// <param name="saltManager"> Объект, предоставляющий соль </param>
        public HashingService(SaltManager saltManager)
        {
            _saltManager = saltManager;
        }

        /// <inheritdoc />
        public string Get(string password)
        {
            Encoding utf8 = Encoding.UTF8;
            byte[] salt = _saltManager.Get();
            byte[] pass = utf8.GetBytes(password);
            byte[] result = new byte[salt.Length + password.Length];

            Array.Copy(salt, 0, result, 0, salt.Length);
            Array.Copy(pass, 0, result, salt.Length, pass.Length);

            using (HashAlgorithm alg = new SHA256Managed())
            {
                result = alg.ComputeHash(result);
            }

            return utf8.GetString(result);
        }
    }
}
