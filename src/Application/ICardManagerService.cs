using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Common.Models.Result;

namespace EGID.Application
{
    public class CardModel
    {
        public string OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class CreateCardModel
    {
        public string OwnerId { get; set; }

        public string Puk { get; set; }

        public string Pin1 { get; set; }

        public string Pin2 { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    public interface ICardManagerService
    {
        /// <summary>
        ///     Is there exit any users returns true if there users, false otherwise.
        /// </summary>
        Task<bool> AnyAsync { get; }

        // #region Quary
        //
        // /// <summary>
        // ///     Get a card by id or return an exception if not find it.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<Card> GetAsync(string id);
        //
        // /// <summary>
        // ///     Get a card by id or return an exception if not find it.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<Card> GetByPrivateKeyAsync(string privateKeyXml);
        //
        // /// <summary>
        // ///     Returns a flag to indelicate that the card is active or not
        // ///     or throw exception if card not founded.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Card not founded.
        // /// </exception>
        // Task<bool> IsActiveAsync(string id);
        //
        // #endregion Quary
        //
        // #region Commands
        //
        // /// <summary>
        // ///     Login and return a success result with a token or return
        // ///     a Errors collection of messages.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<(Result result, string token)> LoginAsync(LoginModel model);
        //
        // /// <summary>
        // ///     Register card and return success result with card id
        // ///     or return failed result with Errors messages.
        // /// </summary>
        // Task<(Result result, string cardId)> RegisterAsync(CreateCardModel model);
        //
        // /// <summary>
        // ///     Change Pin1 and returns success result or a failed
        // ///     result with Error messages.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<Result> ChangePin1Async(ChangePin1Model model);
        //
        // /// <summary>
        // ///     Change Pin3 and returns success result or a failed
        // ///     result with Error messages.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<Result> ChangePin2Async(ChangePin2Model model);
        //
        // /// <summary>
        // ///     Change Puk and returns success result or a failed
        // ///     result with Error messages.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Can not find card.
        // /// </exception>
        // Task<Result> ChangePukAsync(ChangePukModel model);
        //
        // /// <summary>
        // ///     Inactive a card.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Card not founded.
        // /// </exception>
        // public Task<Result> InactivateAsync(string id);
        //
        // /// <summary>
        // ///     Active a card.
        // /// </summary>
        // /// <exception cref="EntityNotFoundException">
        // ///     Card not founded.
        // /// </exception>
        // public Task<Result> ActivateAsync(string id);
        //
        // #endregion Commands
    }
}
