/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class PaymentProduct840SpecificOutput
    {
        /// <summary>
        /// Object containing billing address details<para />
        /// </summary>
        public Address BillingAddress { get; set; } = null;

        /// <summary>
        /// Object containing the details of the PayPal account<para />
        /// </summary>
        public PaymentProduct840CustomerAccount CustomerAccount { get; set; } = null;

        /// <summary>
        /// Object containing billing address details<para />
        /// </summary>
        public Address CustomerAddress { get; set; } = null;

        /// <summary>
        /// Kind of seller protection in force for the PayPal transaction<para />
        /// </summary>
        public ProtectionEligibility ProtectionEligibility { get; set; } = null;
    }
}
