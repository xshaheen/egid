using System.Collections.Generic;
using System.IO;
using EGID.Application.Common.Models.Files;
using Microsoft.AspNetCore.Http;

namespace EGID.Web.Infrastructure
{
    public static class HttpRequestExtensions
    {
        public static IList<BinaryFile> Files(this HttpRequest request)
        {
            var files = new List<BinaryFile>();

            foreach (var file in request.Form.Files)
            {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                files.Add(new BinaryFile(file.Name, memoryStream.ToArray(), file.Length, file.ContentType));
            }

            return files;
        }
    }
}
