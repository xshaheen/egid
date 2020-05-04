using System.Threading.Tasks;
using EGID.Application.Cards.Commands;
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

    public interface ICardManagerService
    {
        /// <summary>
        ///     Is there exit any users returns true if there users, false otherwise.
        /// </summary>
        Task<bool> AnyAsync { get; }

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
        
        /// <summary>
        ///     Returns a flag to indelicate that the card is active or not
        ///     or throw exception if card not founded.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Card not founded.
        /// </exception>
        Task<bool> IsActiveAsync(string id);
        
        /// <summary>
        ///     Login and return a success result with a token or return
        ///     a Errors collection of messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<(Result result, string token)> LoginAsync(string privateKey, string pin1);
        
        /// <summary>
        ///     Register card and return success result with card id
        ///     or return failed result with Errors messages.
        /// </summary>
        Task<(Result result, string cardId)> RegisterAsync(CreateCardCommand model);
        
        /// <summary>
        ///     Add Admin Card.
        /// </summary>
        Task<(Result result, string cardId)> RegisterAdminAsync(string ownerId, string puk,
            string email, string phone, string publicKey, string privateKey);

        /// <summary>
        ///     Change Pin1 and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePin1Async(string cardId, string puk, string newPin1);
        
        /// <summary>
        ///     Change Pin3 and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePin2Async(string cardId, string puk, string newPin2);
        
        /// <summary>
        ///     Change Puk and returns success result or a failed
        ///     result with Error messages.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> ChangePukAsync(string cardId, string currentPuk, string newPuk);
        
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

        /// <summary>
        ///     Add a card to role.
        /// </summary>
        Task<Result> AddToRoleAsync(string cardId, string role);

        /// <summary>
        ///     Delete card async or throw exception if entity not founded.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find card.
        /// </exception>
        Task<Result> DeleteAsync(string cardId);
    }
}
