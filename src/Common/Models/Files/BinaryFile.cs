using System;
using System.IO;
using System.Threading.Tasks;

namespace EGID.Common.Models.Files
{
    public class BinaryFile
    {
        public BinaryFile
        (
            string name,
            byte[] bytes,
            long length,
            string contentType
        )
        {
            Name = name;
            Bytes = bytes;
            Length = length;
            ContentType = contentType;
        }

        public string Name { get; private set; }

        public byte[] Bytes { get; private set; }

        public long Length { get; }

        public string ContentType { get; }

        /// <summary>
        ///     Save a the file to the <paramref name="directory" /> and mutate
        ///     BinaryFile.Name to the saved name name.
        /// </summary>
        /// <param name="directory">The directory to save the file to.</param>
        public async Task SaveAsync(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory) ||
                string.IsNullOrWhiteSpace(Name) ||
                Bytes == null ||
                Bytes.LongLength == 0)
                throw new ArgumentException();

            Directory.CreateDirectory(directory);

            var name = Guid.NewGuid() + Path.GetExtension(Name);

            var path = Path.Combine(directory, name);

            await File.WriteAllBytesAsync(path, Bytes);

            Bytes = null;
        }

        /// <summary>
        ///     Read a file from the directory <paramref name="directory"/>
        ///     which file fileName matches <paramref fileName="fileName" />
        ///     or returns null.
        /// </summary>
        /// <param name="directory">The directory that contain the file.</param>
        /// <param name="fileName">The file fileName.</param>
        /// <returns>
        ///     Returns a BinaryFile instance or null.
        /// </returns>
        public static async Task<BinaryFile> ReadAsync(string directory, string fileName)
        {
            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(fileName)) return null;

            var path = Path.Combine(directory, fileName);

            var file = new FileInfo(path);

            var bytes = await File.ReadAllBytesAsync(file.FullName);

            if (bytes == null || bytes.LongLength == 0) return null;

            return new BinaryFile(file.Name, bytes, file.Length, file.Extension);
        }

        public static void Delete(string directory, string fileName)
        {
            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(fileName)) return;

            var path = Path.Combine(directory, fileName);

            File.Delete(path);
        }
    }
}