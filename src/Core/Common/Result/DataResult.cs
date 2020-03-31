using System.Collections.Generic;
using System.Linq;

namespace EGID.Core.Common.Result
{
    public class DataResult<T>
    {
        internal DataResult() {}

        /// <summary>
        ///     Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeeded { get; protected set; }

        /// <summary>
        ///     Flag indicating whether if the operation failed or not.
        /// </summary>
        /// <value>True if the operation failed, otherwise true.</value>
        public bool Failed => !Succeeded;

        /// <summary>
        ///     Data to carry out
        /// </summary>
        public T Data { get; protected set; }

        public string[] Errors { get; protected set; }

        #region Helpers

        public static DataResult<T> Success(T data) => new DataResult<T> { Succeeded = true, Data = data };

        public static DataResult<T> Failure() => new DataResult<T> { Succeeded = false };

        public static DataResult<T> Failure(IEnumerable<string> message) =>
            new DataResult<T> { Succeeded = false, Errors = message.ToArray() };

        public static DataResult<T> Failure(params string[] message) =>
            new DataResult<T> { Succeeded = false, Errors = message };

        #endregion Helpers
    }
}
