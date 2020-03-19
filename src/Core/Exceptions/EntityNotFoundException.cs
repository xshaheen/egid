using System;

namespace EGID.Core.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if can not find a entity in database.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message) {}

        public EntityNotFoundException(string message, Exception inner)
            : base(message, inner) {}
    }
}
