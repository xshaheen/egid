using EGID.Web.Infra.Crypto;
using EGID.Web.Infra.DigitalSignature;
using EGID.Web.Infra.KeysGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace EGID.Web.Infra
{
    public static class Installer
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IKeyGeneratorService, KeyGeneratorService>();
            services.AddTransient<IAesCryptoService, AesCryptoService>();
            services.AddTransient<IDigitalSignatureService, DigitalSignatureService>();
        }
    }
}
