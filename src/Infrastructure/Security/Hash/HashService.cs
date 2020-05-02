using System;
using System.Security.Cryptography;
using System.Text;

namespace EGID.Infrastructure.Security.Hash
{
    public class HashService : IHashService
    {
        public HashService(int iterations, int size)
        {
            Iterations = iterations;
            Size = size;
        }

        public int Iterations { get; }

        public int Size { get; }

        public string Create(string value, string salt)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                value,
                Encoding.UTF8.GetBytes(salt), Iterations,
                HashAlgorithmName.SHA512
            );

            return Convert.ToBase64String(algorithm.GetBytes(Size));
        }
    }
}