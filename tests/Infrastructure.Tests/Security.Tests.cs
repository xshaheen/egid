using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EGID.Application;
using EGID.Infrastructure.Security;
using EGID.Infrastructure.Security.Cryptography;
using EGID.Infrastructure.Security.DigitalSignature;
using EGID.Infrastructure.Security.Hash;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EGID.Infrastructure.Tests
{
    public class SecurityTests
    {
        private readonly ISymmetricCryptographyService _symmetricCryptographyService;
        private readonly IHashService _hashService;
        private readonly IDigitalSignatureService _digitalSignatureService;

        public SecurityTests()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ISymmetricCryptographyService>(_ =>
                new SymmetricCryptographyService(Guid.NewGuid().ToString()));
            services.AddSingleton<IHashService>(_ => new HashService(10000, 128));
            services.AddSingleton<IDigitalSignatureService, DigitalSignatureService>();

            _symmetricCryptographyService = services.BuildServiceProvider().GetService<ISymmetricCryptographyService>();
            _hashService = services.BuildServiceProvider().GetService<IHashService>();
            _digitalSignatureService = services.BuildServiceProvider().GetService<IDigitalSignatureService>();
        }

        [Fact]
        public async Task CryptographyService()
        {
            // arrange
            const string value = nameof(SecurityTests);
            // act
            var crypt = await _symmetricCryptographyService.EncryptAsync(value);
            var decrypt = await _symmetricCryptographyService.DecryptAsync(crypt);
            // assert
            Assert.Equal(value, decrypt);
        }

        [Fact]
        public void DigitalSignature()
        {
            // arrange
            var data = new byte[] {0, 1, 2, 4, 8, 12, 12, 12, 12, 244, 12, 1, 1, 89, 9};
            // - hash
            var sha = new SHA512Managed();
            var sha512Hash = sha.ComputeHash(data);
            // - base64
            var data64 = Convert.ToBase64String(sha512Hash);
            // - keys
            var keyGenerator = new KeysGeneratorService();

            // act
            var signature = _digitalSignatureService.SignHash(data64, keyGenerator.PrivateKeyXml);

            var isValid = _digitalSignatureService.VerifySignature(data64, signature, keyGenerator.PublicKeyXml);

            data[0] = 1;
            sha512Hash = sha.ComputeHash(data);
            data64 = Convert.ToBase64String(sha512Hash);

            var isNotValid = _digitalSignatureService.VerifySignature(data64, signature, keyGenerator.PublicKeyXml);

            Assert.NotNull(signature);
            Assert.True(isValid);
            Assert.False(isNotValid);
        }

        [Fact]
        public void HashService()
        {
            // arrange
            const string value = nameof(SecurityTests);
            var salt = Guid.NewGuid().ToString();
            // act
            var hash1 = _hashService.Create(value, salt);
            var hash2 = _hashService.Create(value, salt);
            // assert
            Assert.NotNull(hash1);
            Assert.Equal(hash1, hash2);
        }
    }
}