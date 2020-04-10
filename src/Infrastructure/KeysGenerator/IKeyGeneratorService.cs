namespace EGID.Infrastructure.KeysGenerator
{
    public interface IKeyGeneratorService
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}
