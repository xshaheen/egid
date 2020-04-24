using System;

namespace EGID.Common.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if failed to process a file.
    /// </summary>
    [Serializable]
    public class FileProcessingException : Exception
    {
        public FileProcessingException(string message)
            : base(message) {}

        public FileProcessingException(string message, Exception inner)
            : base(message, inner) {}
    }
}
