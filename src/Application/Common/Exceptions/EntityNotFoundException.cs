using System;

namespace EGID.Application.Common.Exceptions
{
    /// <summary>
    ///     An exception that is thrown if can not find a entity in database.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, string key)
            : base($"Entity {type}|{key} was not found.") { }
    }
}