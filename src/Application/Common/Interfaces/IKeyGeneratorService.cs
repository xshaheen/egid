namespace EGID.Application.Common.Interfaces
{
    public interface IKeysGeneratorService
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}