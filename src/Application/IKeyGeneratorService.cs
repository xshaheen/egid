namespace EGID.Application
{
    public interface IKeyGeneratorService
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}
