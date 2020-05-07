using System.IO;
using EGID.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace EGID.Web.Services
{
    public class FilesDirectoryService : IFilesDirectoryService
    {
        public FilesDirectoryService(IWebHostEnvironment env)
        {
            var imgPath = Path.Combine(env.WebRootPath, "Img");
            CitizenPhotosDirectory = Path.Combine(imgPath, "CitizenPhotos");
            HealthInfoDirectory = Path.Combine(imgPath, "HealthAttachments");
        }

        public string CitizenPhotosDirectory { get; }

        public string HealthInfoDirectory { get; }
    }
}