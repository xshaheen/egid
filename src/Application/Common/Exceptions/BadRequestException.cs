using System;

namespace EGID.Application.Common.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if entity is not valid for the
    ///     requested operation.
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        public string[] Errors { get; }

        public BadRequestException(string[] errors) => Errors = errors;
    }
}