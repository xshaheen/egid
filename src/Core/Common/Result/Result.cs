using System.Collections.Generic;
using System.Linq;

namespace EGID.Core.Common.Result
{
    public class Result
    {
        /// <summary>
        ///     Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeeded { get; protected set; }

        public string[] Errors { get; protected set; }

        internal Result() {}

        #region Helpers

        public static Result Success() => new Result { Succeeded = true };

        public static Result Failure() => new Result { Succeeded = false };

        public static Result Failure(IEnumerable<string> errors) => new Result
        {
            Succeeded = false,
            Errors = errors is string[] e ? e : errors.ToArray()
        };

        public static Result Failure(params string[] message)
        {
            return new Result
            {
                Succeeded = false,
                Errors = message
            };
        }

        #endregion Helpers
    }
}
