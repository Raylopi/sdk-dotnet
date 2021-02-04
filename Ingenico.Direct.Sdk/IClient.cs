/*
 * This class was auto-generated from the API references found at
 * https://support.direct.ingenico.com/documentation/api/reference
 */
using Ingenico.Direct.Sdk.Logging;
using Ingenico.Direct.Sdk.Merchant;
using System;

namespace Ingenico.Direct.Sdk
{
    /// <summary>
    /// Ingenico ePayments platform client. Thread-safe.
    ///
    /// This client and all its child clients are bound to one specific value for the <i>X-GCS-ClientMetaInfo</i> header.
    /// To get a new client with a different header value, use <see cref="WithClientMetaInfo"/>.
    /// </summary>
    public interface IClient : IDisposable, ILoggingCapable
    {
        /// <summary>
        /// Returns a new Client which uses the passed meta data for the <i>X-GCS-ClientMetaInfo</i> header.
        /// </summary>
        /// <param name="clientMetaInfo">JSON string containing the meta data for the client</param>
        /// <exception cref="MarshallerSyntaxException">if the given clientMetaInfo is not a valid JSON string</exception>
        Client WithClientMetaInfo(string clientMetaInfo);

        /// <summary>
        /// Utility method that delegates the call to this client's communicator.
        /// </summary>
        void CloseExpiredConnections();

        /// <summary>
        /// Utility method that delegates the call to this client's communicator.
        /// </summary>
        /// <param name="timespan">Idle time.</param>
        void CloseIdleConnections(TimeSpan timespan);

        /// <summary>
        /// Resource /v2/{merchantId}
        /// </summary>
        /// <param name="merchantId">string</param>
        /// <returns>MerchantClient</returns>
        IMerchantClient WithNewMerchant(string merchantId);
    }
}
