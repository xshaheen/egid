using System.IO;
using EGID.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace EGID.Web.Services
{
    public class FilesDirectoryService : IFilesDirectoryService
    {
        public FilesDirectoryService(IWebHostEnvironment env)
        {
            CitizenPhotosRelativePath = Path.Combine("Img", "CitizenPhotos");
            CitizenPhotosDirectory = Path.Combine(env.WebRootPath, CitizenPhotosRelativePath);

            HealthInfoRelativePath = Path.Combine("Img", "HealthAttachments");
            HealthInfoDirectory = Path.Combine(env.WebRootPath, HealthInfoRelativePath);
        }

        public string CitizenPhotosDirectory { get; }

        public string CitizenPhotosRelativePath { get; }

        public string HealthInfoDirectory { get; }

        public string HealthInfoRelativePath { get; }
    }
}