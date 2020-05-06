using System;

namespace EGID.Application.Common.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if failed to process a file.
    /// </summary>
    [Serializable]
    public class FileProcessingException : Exception
    {
        public FileProcessingException(string fileName)
            : base($"Can not upload {fileName}") { }
    }
}