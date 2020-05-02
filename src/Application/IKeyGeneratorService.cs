namespace EGID.Application
{
    public interface IKeysGeneratorService
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}
