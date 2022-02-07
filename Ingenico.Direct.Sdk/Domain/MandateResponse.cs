/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
namespace Ingenico.Direct.Sdk.Domain
{
    public class MandateResponse
    {
        /// <summary>
        /// An alias for the mandate. This can be used to visually represent the mandate. Do not include any unmasked sensitive data in the alias. If this field is not provided the masked IBAN of the customer is used.<para />
        /// </summary>
        public string Alias { get; set; } = null;

        /// <summary>
        /// Customer object containing customer specific inputs.<para />
        /// Required for Create mandate and Create payment calls.<para />
        /// </summary>
        public MandateCustomer Customer { get; set; } = null;

        /// <summary>
        /// The unique identifier of a customer<para />
        /// </summary>
        public string CustomerReference { get; set; } = null;

        /// <summary>
        /// Specifies whether the mandate is for one-off or recurring payments. Possible values are:<para />
        /// * UNIQUE<para />
        /// * RECURRING<para />
        /// </summary>
        public string RecurrenceType { get; set; } = null;

        public string Status { get; set; } = null;

        /// <summary>
        /// The unique identifier of the mandate<para />
        /// </summary>
        public string UniqueMandateReference { get; set; } = null;
    }
}
