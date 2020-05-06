using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGID.Application.Common.Models.Files
{
    public static class BinaryFileExtensions
    {
        public static async Task<IEnumerable<BinaryFile>> SaveAsync(
            this IEnumerable<BinaryFile> files, string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) return null;

            BinaryFile[] binaryFiles = files as BinaryFile[] ?? files.ToArray();

            foreach (var file in binaryFiles)
                await file.SaveAsync(directory).ConfigureAwait(false);

            return binaryFiles;
        }
    }
}