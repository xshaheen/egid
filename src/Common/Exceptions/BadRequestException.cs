using System;

namespace EGID.Common.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if entity is not valid for the
    ///     requested operation.
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message) {}

        public BadRequestException(string message, Exception inner)
            : base(message, inner) {}
    }
}
