namespace EGID.Core.Common.Result
{
    public class DataResult<T> : Result
    {
        /// <summary>
        ///     Data to carry out
        /// </summary>
        public T Data { get; protected set; }

        internal DataResult() {}

        #region Helpers

        public static DataResult<T> Success(T data) => new DataResult<T> { Succeeded = true, Data = data };

        public static DataResult<T> Fail() => new DataResult<T> { Succeeded = false };

        public static DataResult<T> Fail(params string[] message) =>
            new DataResult<T> { Succeeded = false, Errors = message };

        #endregion Helpers
    }
}
