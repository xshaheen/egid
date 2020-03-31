namespace EGID.Web.Infra.KeysGeneratorService
{
    public interface IKeyGenerator
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}
