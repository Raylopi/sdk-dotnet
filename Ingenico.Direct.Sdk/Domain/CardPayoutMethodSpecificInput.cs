/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class CardPayoutMethodSpecificInput
    {
        /// <summary>
        /// Object containing card details<para />
        /// </summary>
        public Card Card { get; set; } = null;

        /// <summary>
        /// Payment product identifier - Please see Products documentation for a full overview of possible values.<para />
        /// </summary>
        public int? PaymentProductId { get; set; } = null;

        /// <summary>
        /// ID of the token<para />
        /// </summary>
        public string Token { get; set; } = null;
    }
}
