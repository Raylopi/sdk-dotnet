/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class CreateMandateResponse
    {
        /// <summary>
        /// Object containing the created mandate.<para />
        /// </summary>
        public MandateResponse Mandate { get; set; } = null;

        /// <summary>
        /// Object that contains the action, including the needed data, that you should perform next, showing the redirect to a third party to complete the payment or like showing instructions.<para />
        /// </summary>
        public MandateMerchantAction MerchantAction { get; set; } = null;
    }
}
