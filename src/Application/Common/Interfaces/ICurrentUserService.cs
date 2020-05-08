namespace EGID.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        /// <summary>
        ///     Current citizen id.
        /// </summary>
        public string CitizenId { get; }

        /// <summary>
        ///     Current card id.
        /// </summary>
        string CardId { get; }

        /// <summary>
        ///     Returns true if the current request is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }
    }
}