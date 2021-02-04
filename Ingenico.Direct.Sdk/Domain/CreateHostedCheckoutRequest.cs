/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class CreateHostedCheckoutRequest
    {
        /// <summary>
        /// Object containing the specific input details for card payments<para />
        /// </summary>
        public CardPaymentMethodSpecificInputBase CardPaymentMethodSpecificInput { get; set; } = null;

        /// <summary>
        /// Object containing additional data that will be used to assess the risk of fraud<para />
        /// </summary>
        public FraudFields FraudFields { get; set; } = null;

        /// <summary>
        /// Object containing hosted checkout specific data<para />
        /// </summary>
        public HostedCheckoutSpecificInput HostedCheckoutSpecificInput { get; set; } = null;

        /// <summary>
        /// Order object containing order related data <para />
        ///  Please note that this object is required to be able to submit the amount.<para />
        /// </summary>
        public Order Order { get; set; } = null;

        /// <summary>
        /// Object containing the specific input details for payments that involve redirects to 3rd parties to complete, like iDeal and PayPal<para />
        /// </summary>
        public RedirectPaymentMethodSpecificInput RedirectPaymentMethodSpecificInput { get; set; } = null;
    }
}
