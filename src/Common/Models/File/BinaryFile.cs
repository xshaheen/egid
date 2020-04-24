using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EGID.Common.Models.File
{
    public class BinaryFile
    {
        public BinaryFile
        (
            Guid id,
            string name,
            byte[] bytes,
            long length,
            string contentType
        )
        {
            Id = id;
            Name = name;
            Bytes = bytes;
            Length = length;
            ContentType = contentType;
        }

        public Guid Id { get; }

        public string Name { get; }

        public byte[] Bytes { get; private set; }

        public long Length { get; }

        public string ContentType { get; }

        /// <summary>
        ///     Read a file that have the name <paramref name="id" />.
        /// </summary>
        /// <param name="directory">The directory that contain the file.</param>
        /// <param name="id">The file name.</param>
        /// <returns>
        ///     Returns a BinaryFile instance or null.
        /// </returns>
        public static async Task<BinaryFile> ReadAsync(string directory, Guid id)
        {
            if (string.IsNullOrWhiteSpace(directory) || id == Guid.Empty) return null;

            var file = new DirectoryInfo(directory).GetFiles(string.Concat(id.ToString(), ".", "*"))
                .SingleOrDefault();

            if (file == null) return null;

            var bytes = await System.IO.File.ReadAllBytesAsync(file.FullName);

            if (bytes == null || bytes.LongLength == 0) return null;

            return new BinaryFile(id, file.Name, bytes, file.Length, file.Extension);
        }

        /// <summary>
        ///     Save a the file to the <paramref name="directory" />.
        /// </summary>
        /// <param name="directory">The directory to save the file to.</param>
        public async Task SaveAsync(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(Name) || Bytes == null ||
                Bytes.LongLength == 0) return;

            Directory.CreateDirectory(directory);

            var name = string.Concat(Id, Path.GetExtension(Name));

            var path = Path.Combine(directory, name);

            await System.IO.File.WriteAllBytesAsync(path, Bytes);

            Bytes = null;
        }
    }
}
