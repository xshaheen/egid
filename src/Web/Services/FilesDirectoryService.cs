using System.IO;
using EGID.Application;
using Microsoft.AspNetCore.Hosting;

namespace EGID.Web.Services
{
    public class FilesDirectoryService : IFilesDirectoryService
    {
        public FilesDirectoryService(IWebHostEnvironment env)
        {
            CitizenPhotosDirectory = Path.Combine(env.WebRootPath, "Img", "CitizenPhotos");
        }

        public string CitizenPhotosDirectory { get; }
    }

    
}
