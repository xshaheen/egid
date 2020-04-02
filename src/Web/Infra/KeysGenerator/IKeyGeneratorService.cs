namespace EGID.Web.Infra.KeysGenerator
{
    public interface IKeyGeneratorService
    {
        public string PrivateKeyXml { get; }

        public string PublicKeyXml { get; }
    }
}
