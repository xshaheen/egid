namespace EGID.Application.Common.Interfaces
{
    public interface IFilesDirectoryService
    {
        public string CitizenPhotosDirectory { get; }
        public string CitizenPhotosRelativePath { get; }

        public string HealthInfoDirectory { get; }
        public string HealthInfoRelativePath { get; }
    }
}
