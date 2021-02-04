/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class TokenResponse
    {
        /// <summary>
        /// Object containing card details<para />
        /// </summary>
        public TokenCard Card { get; set; } = null;

        /// <summary>
        /// Object containing eWallet details<para />
        /// </summary>
        public TokenEWallet EWallet { get; set; } = null;

        public ExternalTokenLinked ExternalTokenLinked { get; set; } = null;

        /// <summary>
        /// ID of the token<para />
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// Payment product identifier - Please see [payment products](https://support.direct.ingenico.com/documentation/api/reference/index.html#tag/Products) for a full overview of possible values.<para />
        /// </summary>
        public int? PaymentProductId { get; set; } = null;
    }
}
