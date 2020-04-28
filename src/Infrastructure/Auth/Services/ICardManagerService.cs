using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Models.Result;
using EGID.Infrastructure.Auth.Models;
using EGID.Infrastructure.Auth.Services.Dto;

namespace EGID.Infrastructure.Auth.Services
{
    public interface ICardManagerService
    {
        #region Quary

        /// <summary>
        ///     Get a card by id or return an exception if not find it.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Card> GetAsync(string id);

        /// <summary>
        ///     Get a card by id or return an exception if not find it.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Card> GetByPrivateKeyAsync(string privateKeyXml);

        /// <summary>
        ///     Returns a flag to indelicate that the card is active or not
        ///     or throw exception if card not founded.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Card not founded.
        /// </exception>
        Task<bool> IsActiveAsync(string id);

        #endregion Quary

        #region Commands

        /// <summary>
        ///     Login and return a success result with a token or return
        ///     a Errors collection of messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<(Result result, string token)> LoginAsync(LoginModel model);

        /// <summary>
        ///     Register card and return success result with card id
        ///     or return failed result with Errors messages.
        /// </summary>
        Task<(Result result, string cardId)> RegisterAsync(CreateCardDto model);

        /// <summary>
        ///     Change Pin1 and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePin1Async(ChangePin1Dto model);

        /// <summary>
        ///     Change Pin3 and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePin2Async(ChangePin2Dto model);

        /// <summary>
        ///     Change Puk and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePukAsync(ChangePukDto model);

        /// <summary>
        ///     Inactive a card.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Card not founded.
        /// </exception>
        public Task<Result> InactivateAsync(string id);

        /// <summary>
        ///     Active a card.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Card not founded.
        /// </exception>
        public Task<Result> ActivateAsync(string id);

        #endregion Commands
    }
}