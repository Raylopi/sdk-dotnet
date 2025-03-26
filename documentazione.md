openapi: 3.0.0
info:
  title: API specifications
  version: 2.352.2
  description: >-
    # Introduction 

    The API has been designed as a REST API. It uses the HTTP protocol as its foundation. Each resource is accessible under a clearly named URL and the HTTP response codes are used to relay status. HTTP Verbs like GET and POST are used to interact with the resources. To support accessibility by clients directly, as opposed to your server, our servers support cross-origin resource sharing. We use JSON for all of our payloads, including error messages. We use HMAC-SHA256 for the [authentication](/documentation/api/authentication).


    All these characteristics mean that you will be able to use standard off-the-shelf software to interact with our payment platform. To make the integration even easier, payment platform also has SDKs that wrap both the complete Server API as well as the complete Client API.
tags:
- name: HostedCheckout
  description: >-
    Using hostedCheckout you can process transactions through our hosted responsive payment pages. Your consumers will be able to complete the payment process using HostedCheckout that is hosted by us. Using these pages to capture card details and process card transactions will greatly reduce not only your integration efforts, but it will also reduce your 'PCI exposure' to the lowest level (SAQ-A).

     The HostedCheckout pages automatically adapt based on screen width of your consumers device. Clients that do not support JavaScript to be run on the client will still be able to make payments (although they will lack all the client side validation and automatic formatting to assist them). Please note that both the responsive nature and the support for clients without JavaScript support might not apply to 3rd party payment pages that the consumer might be redirected to complete the payment.

     HostedCheckout is also modular in its setup, allowing you to specify exactly what you want us to do as part of your checkout flow. You decide if we should display the payment product selection and/or the confirmation page after a successful payment.
- name: Payments
  description: The payments REST services allow you to initiate a payment, retrieve the payment details or perform specific actions like refunding or requesting capture of a payment. A payment is identified by its paymentId. Some payment products allow a two step approach allowing you to control when the authorization takes place separate from when the funds are actually captured.
- name: Captures
  description: The captures REST services allow you to retrieve details of a capture created through the capture payment API. A capture is identified by its captureId.
- name: Refunds
  description: The refund API allows you to manipulate refunds that have been created on a payment. Funds will be refunded to either the card or wallet that was originally charged or to a bank account if a direct refund is not possible.
- name: Pre-authorisation
  description: Operation typical to pre-authorisation lifecycle management, to increment a pre-authorisation, or add market specfic additional data prior to capturing the payment.
- name: Complete
- name: Products
  description: Products is your entry on all things related to payment products. You will be able to retrieve all relevant payment products, based on your configuration and provided filters, their associated fields and potential directories. You can retrieve all of the information in one call or do calls on individual payment products. The data returned is designed to give you all the required information to build up your interface towards your consumers in a dynamic fashion. By doing it like that you know that you will be ready for future changes and new payment products without much effort.
- name: ProductGroups
  description: Through this API you can retrieve payment product groups. A payment product group has a collection of payment products that can be grouped together on a payment product selection page, and a set of fields to render on the payment product details page that allow to determine which payment product of the group the consumer wants to select. We currently support one payment product group named cards that has every credit card we support (and every debit card that behaves like a credit card).
- name: Services
  description: >-
    Under services you find several calls that can be used to support your payment flow:
     - Test your connection to us
     - Retrieve the card type and country where the card was issued based on the IIN of the card
- name: Tokens
  description: >-
    Using our tokenization service you can tokenize re-usable payment data like card data, bank account data including Direct Debit Mandates and PayPal BillingAgreementIDs. The main purpose for tokens is re-use of payment details. The additional benefit is that you do not need to store any sensitive payment details on your server, while still having the benefit to be able to re-use them. This allows you to process recurring card transactions without actually having access to the real card data.

     Tokens can be used for two types of transactions:

    * Recurring: Automatically charging your consumer in a regular, e.g. monthly, time frame; 

    * One-off: Charge the consumer without the consumer having to re-enter all of their payment details.


    The second scenario can be used to facilitate a one-click checkout solution, that would still allow the consumer to enter only their CVV value for a card transaction. CVV values can't be tokenized as they are not allowed to be stored at all.


    Besides the re-use of payment data, tokens have one other major use-case: Direct Debit Mandates. Especially SEPA Direct Debit transactions require that the mandate for the transactions is managed through a token with an associated mandate. Mandates are created in one go with the token, but can have a state that requires that they are approved before they can be used. As the mandate process is in most cases an offline process the approval will allow you to set the location and date where and when the mandate was signed by the consumer. Without an approved SEPA mandate you will not be able to process any payments regarding this mandate.
- name: Payouts
- name: HostedTokenization
  description: Using the hosted tokenization service you can generate a form that can be embedded in your own payment page to capture and tokenize card payment data.
- name: Sessions
  description: Sessions allow clients, like mobile phones or web-browsers, to make use of our Client API. A session always needs to be initiated through the Server API because the client will send API requests on your behalf. Sessions allow consumers to make payments and allow you to easily show the correct payment product, ask for the right properties and easily present and re-use previously stored (tokenized) payment details. It also allows for client-side encryption of sensitive data, like card number, expiry date and CVV.
- name: ClientProducts
  description: Through this API you can retrieve details of the payment products that are configured for your account.
- name: ClientCrypto
  description: The crypto API provides a transaction-specific public key for encrypting sensitive data, such as card details.
- name: ClientProductGroups
  description: Through this API you can retrieve payment product groups. A payment product group has a collection of payment products that can be grouped together on a payment product selection page, and a set of fields to render on the payment product details page that allow to determine which payment product of the group the consumer wants to select. We currently support one payment product group, named cards, that has every credit card we support (and every debit card that behaves like a credit card).
- name: ClientServices
- name: Mandates
  description: The mandates REST services allow you to manage mandates, used in SEPA Direct Debit payments.
- name: PrivacyPolicy
  description: Through this API you can retrieve the privacy policies to display on your own page when using Server-to-Server or Hosted Tokenization Page.
- name: PaymentLinks
  description: This API allows you to create payment links, retrieve details about your existing links, or cancel a link. A payment link is identified by its paymentLinkId.
- name: Webhooks
  description: Webhooks allows you to receive notifications concerning any status change on your payments and operations. The following endpoints allow you to test and demo the webhook feature.
x-tagGroups:
- name: Server API
  tags:
  - HostedCheckout
  - Payments
  - Captures
  - Refunds
  - Complete
  - Products
  - ProductGroups
  - Services
  - Tokens
  - Payouts
  - HostedTokenization
  - Sessions
  - Mandates
  - PrivacyPolicy
  - PaymentLinks
  - MerchantBatch
  - MyCheckout
  - Webhooks
- name: Client API
  tags:
  - ClientCrypto
  - ClientPayments
  - ClientProductGroups
  - ClientProducts
  - ClientServices
paths:
  /v2/{merchantId}/hostedcheckouts:
    post:
      summary: Create hosted checkout
      description: >-
        You can start a hostedCheckout flow by posting the relevant details to the endpoint. We will then return you all the details you need to redirect the consumer to us, retrieve the status and recognize the consumer when he/she returns to your website. 
         The hosted checkout allows the use of three distinct components:

        * Presenting a (filtered) list of payment products that the consumer can choose from;


        * Handling of the actual payment, potentially involving data capture, redirection and/or the displaying of payment instructions;


        * Presenting a confirmation/failure page after the payment.


         Step 1 and 3 from the above list are optional. When no or partial filtering is provided, the first page the consumer will see is a payment product selection page. However, the hosted checkout will start as if a payment product had been selected if that specific payment product is the only entry in the request's restriction filters.

        By providing payment product ids and groups in the hostedCheckoutSpecificInput's paymentProductFilters object, you can reduce the list of available payment products by either excluding or restricting to certain products. Note that at least one viable payment product must be left after filtering and that exclusion is leading, meaning that restricting and excluding the same product will lead to exclusion.


        By setting the tokensOnly boolean to true, as part of the paymentProductFilters object, the consumer may only complete the payment using one of the accounts on file provided in the tokens property of hostedCheckoutSpecificInput.


        By setting the showResultPage boolean to false, as part of the hostedCheckoutSpecificInput, the system will skip the confirmation/failure page after the consumer has completed the payment. This setting is true by default and the results page will be presented to the user.


        In case the payment product selection page needs to be skipped the hostedCheckout call needs to contain one of the following objects in which all payment products have been categorized. These will also be used if the consumer selects a related payment product.


        * Card payments - cardPaymentMethodSpecificInput


        * All credit and debit card products fall into this category if they allow for direct submission of card data without a redirect to a third party to capture the card details.


        * Redirect payments - redirectPaymentMethodSpecificInput


        * Mobile payments - mobilePaymentMethodSpecificInput


        * All payment products that involve a redirect to a 3rd party to complete the payment directly online, like PayPal.


        A generic transaction can be submitted using the order and the fraudFields objects.
      tags:
      - HostedCheckout
      operationId: CreateHostedCheckoutApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createHostedCheckoutRequest'
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createHostedCheckoutResponse'
        400:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/hostedcheckouts/{hostedCheckoutId}:
    get:
      summary: Get hosted checkout status
      description: >-
        You can retrieve the current status of the hosted checkout by doing a get on the hostedCheckoutId. When a payment has been created during the hosted checkout session the details are returned in this object.


        Sessions have a maximum life span of 3 hours. This means that you can only retrieve this information while the session has not timed-out.


        The status of the hostedCheckout and the payment will change when the consumer is still busy completing the hosted checkout session.
      tags:
      - HostedCheckout
      operationId: GetHostedCheckoutApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: hostedCheckoutId
        in: path
        description: The HostedCheckout Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: The request was processed correctly and a valid response is returned. In case a payment was created during the hosted checkout the details are returned in the createdPaymentOutput object.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getHostedCheckoutResponse'
        404:
          x-nullable: true
          description: Bad request
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/hostedtokenizations:
    post:
      summary: Create hosted tokenization session
      description: 'You can start a hosted tokenization session by posting the relevant details to the endpoint. We will then return you the url with the tokenization form to be embedded in your page. '
      tags:
      - HostedTokenization
      operationId: CreateHostedTokenizationApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createHostedTokenizationRequest'
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createHostedTokenizationResponse'
        400:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/hostedtokenizations/{hostedTokenizationId}:
    get:
      summary: Get hosted tokenization session
      description: >-
        You can retrieve the current status of the hosted tokenization by doing a get on the hostedTokenizationId. When a token has been created or updated during the hosted tokenization session, the details are returned in this object.


        Sessions have a maximum life span of 3 hours. This means that you can only retrieve this information while the session has not timed-out.
      tags:
      - HostedTokenization
      operationId: GetHostedTokenizationApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: hostedTokenizationId
        in: path
        description: The HostedTokenization Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: The request was processed correctly and a valid response is returned. In case a token was created or updated during the hosted tokenization session, the details are returned in the Token object.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getHostedTokenizationResponse'
        404:
          x-nullable: true
          description: Hosted tokenization session not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payments:
    post:
      summary: Create payment
      description: >-
        You initiate a payment by posting all the required payment details. After you have done so one or more of the following things can take place:


        * Your payment request is rejected. This can happen for various reasons, but a detailed reason is always returned in the response to you. In some cases a payment object was created and you will find all the details in the response as well. 

        * The data you submitted used to assess the risk of potential fraud. If this is deemed to great based on your configuration the transaction is either rejected or placed in a queue for your manual review. If the fraud risk is deemed to be within the acceptable set limits the processing is continued and one of the other possible outcomes listed here will be returned. 

        * The data is sent to a third party for authorization 

        * The consumer is required to authenticate themselves and a redirect to a third party is required, sometimes this also includes the actual authorization of the payment by the consumer while at the third party. The response will contain all the details required for you to redirect the consumer to the third party. 

        * Payment instruction details are returned so you can provide the right instructions to your consumer on how to complete the payment. 

        * The data is simply stored for future processing.


        Please look at the flow diagram of each payment product that you would like to integrate to see what possible responses can be returned to you depending on the payment product.


        The type of processing flow is also dependent on the individual configuration of your account(s). This will be chosen in conjunction with you to best match your business
      tags:
      - Payments
      operationId: CreatePaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createPaymentRequest'
        required: true
      responses:
        201:
          x-nullable: true
          description: The payment request was successfully processed and a payment object was created.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createPaymentResponse'
                description: Object that contains details on the created payment in case one has been created.
        400:
          x-nullable: true
          description: The request was malformed or was missing required data. When a required property was missing the error message will point out which property caused the error.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        402:
          x-nullable: true
          description: The payment was declined by a 3rd party (acquirer, payment processor, etc.)
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        403:
          x-nullable: true
          description: You are not allowed to access the service or account or your API authentication failed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        404:
          x-nullable: true
          description: Payment not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        409:
          x-nullable: true
          description: Conflict
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        502:
          x-nullable: true
          description: Any 5XX response points to something that went wrong on our end. This could also be that the system was unable to route the transaction to an appropriate acquirer/3rd party. Another reason for such a response if when the 3rd party's response could not be understood.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        503:
          x-nullable: true
          description: Service unavailable
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
  /v2/{merchantId}/payments/{paymentId}:
    get:
      summary: Get payment
      description: >-
        Retrieves the details of the payment operation with the paymentId provided in the URL.  This paymentId was returned to you with the create payment request. The request does not have any additional input parameters.

        For capture operations, you must use Get Captures or Get Payment Details. For refund operations, you must use Get Refunds or Get Payment Details.
      tags:
      - Payments
      operationId: GetPaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Return the details of the payment
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentResponse'
                description: Object that holds the payment related properties
        404:
          x-nullable: true
          description: Payment not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payments/{paymentId}/details:
    get:
      summary: Get payment details
      description: Retrieves the full details of the payment with the paymentId provided in the URL.  This paymentId was returned to you with the create payment request. The request does not have any additional input parameters.
      tags:
      - Payments
      operationId: GetPaymentDetailsApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Return the details of the payment
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentDetailsResponse'
                description: Object that holds the payment details properties
        404:
          x-nullable: true
          description: Payment not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payments/{paymentId}/cancel:
    post:
      summary: Cancel payment
      description: >-
        If you decided that you do not want to process the payment it is always smart to cancel the payment. This makes it impossible to process the payment any further and will also try to reverse an authorization on a card. Reversing an authorization that you will not be utilizing will prevent you from having to pay a fee/penalty for unused authorization requests.


        Whilst scheme regulations require that acquirers (and their providers, like us) support authorization reversals, there are no rules towards issuers mandating them to process the reversal advice. Therefore there is no guarantee the authorization hold is released. Also be aware that the issuer needs time to process the request. The funds may not be unblocked immediately even whilst the request is sent real-time.

        For any authorization reversal request initiated by a merchant, we will pass the request through to an acquirer for subsequent submission to the card issuer for processing.


        The authorization reversal can only be performed by the card issuer, and under no circumstances will we be responsible for performing the authorization reversal.


        There is no guarantee that the card issuer will process the authorization reversal, nor is there any guarantee that the authorization reversal will occur in real-time. Neither we nor any of its affiliates will be liable to a merchant for any costs, losses and/or damages arising out of a card issuer not processing or delaying to process an authorization reversal request.
      tags:
      - Payments
      operationId: CancelPaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/cancelPaymentRequest'
      responses:
        200:
          x-nullable: true
          description: The payment has been cancelled. Some acquirers/issuers will provide some feedback in case a reversal of the authorization has been performed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/cancelPaymentResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        402:
          x-nullable: true
          description: Payment required
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: Payment was not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        409:
          x-nullable: true
          description: Cancellation is not allowed because payment is closed
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        500:
          x-nullable: true
          description: Internal server exception
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payments/{paymentId}/captures:
    get:
      summary: Get captures of payment
      description: Retrieves the details of all captures performed on the payment with the paymentId provided in the URL
      tags:
      - Captures
      operationId: GetCapturesApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: An HTTP 200 response is returned if the payment was found, even if no captures were performed on it.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/capturesResponse'
  /v2/{merchantId}/payments/{paymentId}/capture:
    post:
      summary: Capture payment
      description: >-
        When you want to capture the funds on a payment with a PENDING_CAPTURE state you can call this API. This API allows multiple, partial captures of the authorized funds. Depending on the payment product and the 3rd party used to process the payment this might be done in real-time or in more off-line, batch-like fashion.


        If the created payment requires approval then it will require this step before the funds are actually captured.


        PENDING_CAPTURE is only a common status with card transactions. You can specify the amount you would like to be captured in case you want to capture a lower amount than the authorized amount.
      tags:
      - Payments
      operationId: CapturePaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/capturePaymentRequest'
        required: true
      responses:
        201:
          x-nullable: true
          description: Successful response, payment has been captured
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/captureResponse'
  /v2/{merchantId}/payments/{paymentId}/refunds:
    get:
      summary: Get refunds of payment
      description: Retrieves the details of all refunds performed on the payment with the paymentId provided in the URL.
      tags:
      - Refunds
      operationId: GetRefundsApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: An HTTP 200 response is returned if the refunds were found.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/refundsResponse'
  /v2/{merchantId}/payments/{paymentId}/refund:
    post:
      summary: Refund payment
      description: >-
        You can refund any transaction by just calling this API.  The system will automatically select the most appropriate means to refund the transaction based on the payment product that was used for the payment and the currency and the country of the refund request. 


        Depending on the payment product of the payment you will need to supply refund specific details on where the money should be refunded to or not. You always have the option to refund just a portion of the payment amount. It is also possible to submit multiple refund requests on one payment as long as the total amount to be refunded does not exceed the total amount that was paid.
      tags:
      - Payments
      operationId: RefundPaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/refundRequest'
        required: true
      responses:
        201:
          x-nullable: true
          description: The refund was successfully created.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/refundResponse'
                description: This object has the numeric representation of the current refund status, timestamp of last status change and performable action on the current refund resource. In case of a rejected refund, detailed error information is listed.
        400:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/refundErrorResponse'
        404:
          x-nullable: true
          description: The most common cause for this response id that the payment was not in a cancelable state.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/refundErrorResponse'
  /v2/{merchantId}/payments/{paymentId}/complete:
    post:
      summary: Complete payment
      description: >-
        Currently, the only payment product that makes use of Complete payment is PayPal (840).

        Certain payment products use a two-step process to collect the required information to perform a payment. In these cases, the first step is to create a payment, the second is to complete it by calling this API. This API accepts a full payment object, but only specific properties will be required depending on the payment product.

        This API is used in the Express Checkout Shortcut flow that allows consumers to pre-select PayPal earlier in the checkout flow. This makes it possible for you to use billing and shipping details that are stored at PayPal. It separates the authentication of the user at PayPal from the transaction processing, which can then be done in the background. 

        You call Complete payment when the redirect to PayPal was finished successful and you have all the right data (amounts) and are ready to capture the transaction.
      tags:
      - Complete
      operationId: CompletePaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/completePaymentRequest'
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/completePaymentResponse'
        400:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
  /v2/{merchantId}/payments/{paymentId}/subsequent:
    post:
      summary: Subsequent payment
      description: You can initiate a subsequent payment based on the initial Payment Id by calling the Subsequent endpoint.
      tags:
      - Payments
      operationId: SubsequentPaymentApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id.
        schema:
          type: string
        required: true
      - name: paymentId
        in: path
        description: The Payment Id of the first transaction (either in-store or online), from which you request to make a new subsequent payment.
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/subsequentPaymentRequest'
        required: true
      responses:
        201:
          x-nullable: true
          description: The payment request was successfully processed and a payment object was created.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/subsequentPaymentResponse'
                description: Object that contains details on the created payment in case one has been created.
        400:
          x-nullable: true
          description: The request was malformed or was missing required data. When a required property was missing the error message will point out which property caused the error.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        402:
          x-nullable: true
          description: The payment was declined by a 3rd party (acquirer, payment processor, etc.)
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        403:
          x-nullable: true
          description: You are not allowed to access the service or account or your API authentication failed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        404:
          x-nullable: true
          description: Payment not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        409:
          x-nullable: true
          description: Conflict
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        502:
          x-nullable: true
          description: Any 5XX response points to something that went wrong on our end. This could also be that the system was unable to route the transaction to an appropriate acquirer/3rd party. Another reason for such a response if when the 3rd party's response could not be understood.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
        503:
          x-nullable: true
          description: Service unavailable
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentErrorResponse'
  /v2/{merchantId}/productgroups:
    get:
      summary: Get product groups
      description: This service retrieves payment product groups, filterable by transaction details. Responses include groups matching criteria, e.g., supporting recurring payments. Use 'hide' to exclude token details or field lists.
      tags:
      - ProductGroups
      operationId: GetProductGroups
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: 'Deprecated: This field has no effect.'
        schema:
          type: string
        deprecated: true
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPaymentProductGroupsResponse'
                description: The response contains an array of payment product groups that match the filters supplied in the request.
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/productgroups/{paymentProductGroupId}:
    get:
      summary: Get product group
      description: Use this service if you are just interested in a particular payment product group. You can submit additional details on the transaction as filters. In that case, the group is returned only if it matches the filters, otherwise a 404 response is returned.
      tags:
      - ProductGroups
      operationId: GetProductGroup
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentProductGroupId
        in: path
        description: The payment product group id.
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: 'Deprecated: This field has no effect.'
        schema:
          type: string
        deprecated: true
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProductGroup'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/products:
    get:
      summary: Get payment products
      description: >-
        The returned payment products will be limited by the filters provided in the Create Session request.


        The list can be limited on a by-call basis by providing additional details on the transaction in the request. For example, you can request that only payment products that support recurring transactions will be returned when you are setting your user up for a recurring payment. You can also reduce the data that is returned in the response by hiding certain elements. This is done using the hide field and allows the request to omit token details and/or the list of fields associated with each of the payment products.


        By submitting the locale you can benefit from the server side language packs if your application does not have these or if you are using the JavaScript SDK. Note however that the values returned will be based on our default language packs and will not contain any specific modifications that you might have made through the Configuration Center. Our iOS and Android SDKs have language packs included which allows for easy management.
      tags:
      - Products
      operationId: GetPaymentProducts
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: Locale used in the GUI towards the consumer.
        schema:
          type: string
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains an array of payment products that match the filters supplied in the request.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPaymentProductsResponse'
                description: The response contains an array of payment products that match the filters supplied in the request.
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/products/{paymentProductId}:
    get:
      summary: Get payment product
      description: Use this API if you are just interested in one payment product or if you have filtered out the fields in the Get payment products call and you want to retrieve the field details on a single payment product. By doing it in this two-step process you will reduce the amount of data that needs to be transported back to your client, but you will have to make two calls. Usually the performance penalty is bigger when you need to do multiple calls with a small response package than one call with a bigger response package. You are however free to choose the best solution for your use case. You can submit additional details on the transaction as filters. In that case, the payment product is returned only if it matches the filters, otherwise a 400 response is returned.
      tags:
      - Products
      operationId: GetPaymentProduct
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentProductId
        in: path
        description: The payment product identifier.
        schema:
          type: integer
          format: int32
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: Locale used in the GUI towards the consumer.
        schema:
          type: string
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProduct'
                description: Payment product
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/products/{paymentProductId}/networks:
    get:
      summary: Get payment product networks
      description: Mobile payment methods (e.g., Apple Pay, Google Pay) use networks like "VISA" for actual payments. The consumer's device should display cards using available networks. This endpoint lists networks for the current payment context. The returned networks depend on query parameters, payment product support, and configurations.
      tags:
      - Products
      operationId: GetPaymentProductNetworks
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentProductId
        in: path
        description: Payment product identifier. Please see payment products for a full overview of possible values.
        schema:
          type: integer
          format: int32
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: amount
        in: query
        description: Amount in cents and always having 2 decimals
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter networks based on their support for recurring or not

          * true

          * false
        schema:
          type: boolean
      responses:
        200:
          x-nullable: true
          description: 'An array of networks is returned. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProductNetworksResponse'
                description: 'Array containing network entries for a payment product. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: When no networks can be found matching the input criteria a HTTP 404 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/products/{paymentProductId}/directory:
    get:
      summary: Get payment product directory
      description: "Certain payment products have directories that the consumer needs to pick from. \nThe most well known example is the list of banks for iDeal that the consumer needs to select their bank from. \niDeal is however not the only payment product for which this applies. "
      tags:
      - Products
      operationId: GetProductDirectoryApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentProductId
        in: path
        description: The Payment Product Id
        schema:
          type: integer
          format: int32
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code
        schema:
          type: string
          minLength: 2
          maxLength: 2
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency of the transaction
        schema:
          type: string
          minLength: 3
          maxLength: 3
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/productDirectory'
        400:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: no directory Found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/services/testconnection:
    get:
      summary: Test connection
      description: 'Test your connection and credentials '
      tags:
      - Services
      operationId: TestConnectionApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/testConnection'
        403:
          x-nullable: true
          description: Your API authentication failed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/services/getIINdetails:
    post:
      summary: Get IIN details
      description: This call verifies card processability and suggests the best card type based on configuration, using the initial 6 or more digits. Some cards are dual branded, having both local and international brands, and may not be returned if not supported. Once the first 6 digits are captured, use this API to validate card type and acceptance. The resulting paymentProductId can be used to display the suitable payment product logo for user feedback.
      tags:
      - Services
      operationId: GetIINDetailsApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/getIINDetailsRequest'
              description: Input for the retrieval of the IIN details request
        required: true
      responses:
        200:
          x-nullable: true
          description: The IIN submitted in your request matches a known card type that is configured for your account. The response contains information on that card type.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getIINDetailsResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: When the IIN does not match any of the products that are configured on your account an HTTP 404 response is returned. This means that we will not be able to process this card, most likely due to the fact that your account is not set up for certain specific card products that the consumer is trying to make the payment with.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/services/dccrate:
    post:
      summary: Get currency conversion quote
      description: This call returns the currency conversion rate.
      tags:
      - Services
      operationId: GetDccRateInquiryApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/currencyConversionRequest'
        required: true
        description: Currency conversion request
      responses:
        200:
          description: Successfully returns currency conversion quote
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/currencyConversionResponse'
                description: Payload of the response to a rate inquiry request
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/webhooks/validateCredentials:
    post:
      summary: Validate credentials
      description: Validate credentials for webhooks
      tags:
      - Webhooks
      operationId: ValidateWebhookCredentialsApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/validateCredentialsRequest'
        required: true
      responses:
        200:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/validateCredentialsResponse'
        403:
          x-nullable: true
          description: API authentication failed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/webhooks/sendtest:
    post:
      summary: Send test
      description: Send a test webhook to the provided URL. If no URL is provided, it would be read from the configuration.
      tags:
      - Webhooks
      operationId: SendTestWebhookApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/sendTestRequest'
      responses:
        204:
          description: The request was successful
        400:
          x-nullable: true
          description: Incorrect input data.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        403:
          x-nullable: true
          description: API authentication failed.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: Configuration for sending webhook messages was not found.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/sessions:
    post:
      summary: Create session
      description: >-
        A new session is created by sending a POST request to the above mentioned end-point.


        Sessions have a maximum lifespan of 3 hours. It is not possible to chain sessions. In case a session has expired, you can simply create a new one through this API.


        One payment can span multiple sessions if needed and within one session multiple payments can be processed as long as they are with the same consumer.


        It's possible to limit the payment products available to the consumer to complete the payment by providing restriction and exclusion filters for payment product ids and payment product groups.In case you have identified that the consumer is now willing to start a new session and you have previously stored tokens for this consumer it is possible to provide a list of stored tokens. If you do so, the previously stored details can easily be re-used by the consumer. Offering this will reduce the required input in the checkout process and will greatly improve the conversion to paying consumers.


        It is important to secure the access to the consumer's account in your system as it provides direct access to stored payment details.
      tags:
      - Sessions
      operationId: CreateSessionApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/sessionRequest'
        required: true
      responses:
        201:
          x-nullable: true
          description: ''
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/sessionResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/tokens/{tokenId}:
    get:
      summary: Get token
      description: >-
        Use GET on a specific token to retrieve all the tokenized data for that ID. You can use some of this data towards the consumer to let them choose which stored data to re-use. You can also use data like the expiry date to check if you need to ask for updated data from your consumer.


        We will never return full card details.
      tags:
      - Tokens
      operationId: GetTokenApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: tokenId
        in: path
        description: The Token Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Successful response, token has been retrieved. All non-sensitive data that is stored is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/tokenResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: In case you are trying to retrieve a token that does not exist or has been deleted you will get a 404 response.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
    delete:
      summary: Delete token
      description: Use DELETE on a specific token to remove or invalidate it. You will not be able to undo this deleting and will not be able to use the token in the future. In case you are deleting a SEPA Direct Debit you can set the date on which the mandate is to be cancelled.
      tags:
      - Tokens
      operationId: DeleteTokenApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: tokenId
        in: path
        description: The Token Id
        schema:
          type: string
        required: true
      responses:
        204:
          description: Successful response, token has been deleted
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: In case the token could not be found a HTTP 404 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/tokens:
    post:
      summary: Create token
      description: This API endpoint tokenizes payment details, with basic input validation. It does not verify details with acquirers or processors.
      tags:
      - Tokens
      operationId: CreateTokenApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createTokenRequest'
              description: Object containing the token details
        required: true
      responses:
        200:
          x-nullable: true
          description: An HTTP 200 response indicates that the card data was previously tokenized. The previous token is returned again. The evaluation regarding duplication is done purely on the card number. Please note that it is possible that the data other than the card number is different from the stored data, in that case you may need to update the token with the new data.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createdTokenResponse'
        201:
          x-nullable: true
          description: An HTTP 201 response indicates that the data was successfully tokenized and the newly created token is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createdTokenResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payouts/{payoutId}:
    get:
      summary: Get payout
      description: Our payout service enables seamless direct money transfers to a chosen bank account.
      tags:
      - Payouts
      operationId: GetPayoutApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: payoutId
        in: path
        description: The Payout Id
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: For every successfully retrieved payout an HTTP 200 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/payoutResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: In case the payoutId could not be found a HTTP 404 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/payouts:
    post:
      summary: Create payout
      description: Our payout service enables seamless direct money transfers to a chosen bank account.
      tags:
      - Payouts
      operationId: CreatePayoutApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createPayoutRequest'
              description: Object containing the payout details
        required: true
      responses:
        201:
          x-nullable: true
          description: For every successfully created payout a HTTP 201 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/payoutResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/payoutErrorResponse'
  /client/v1/{customerId}/crypto/publickey:
    get:
      summary: Get public key
      description: The crypto API provides a transaction-specific public key for encrypting sensitive data, such as card details.
      tags:
      - ClientCrypto
      operationId: GetClientPublicKey
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: The response contains the needed input for the encryption process.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPublicKeyResponse'
  /client/v1/{customerId}/productgroups:
    get:
      summary: Get product groups
      description: This service retrieves payment product groups, filterable by transaction details. Responses include groups matching criteria, e.g., supporting recurring payments. Use 'hide' to exclude token details or field lists.
      tags:
      - ClientProductGroups
      operationId: GetClientProductGroups
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: 'Deprecated: This field has no effect.'
        schema:
          type: string
        deprecated: true
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPaymentProductGroupsResponse'
                description: The response contains an array of payment product groups that match the filters supplied in the request.
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/productgroups/{paymentProductGroupId}:
    get:
      summary: Get product group
      description: Use this service if you are just interested in a particular payment product group. You can submit additional details on the transaction as filters. In that case, the group is returned only if it matches the filters, otherwise a 404 response is returned.
      tags:
      - ClientProductGroups
      operationId: GetClientProductGroup
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      - name: paymentProductGroupId
        in: path
        description: The payment product group id.
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: 'Deprecated: This field has no effect.'
        schema:
          type: string
        deprecated: true
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProductGroup'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/products:
    get:
      summary: Get payment products
      description: >-
        The returned payment products will be limited by the filters provided in the Create Session request.


        The list can be limited on a by-call basis by providing additional details on the transaction in the request. For example, you can request that only payment products that support recurring transactions will be returned when you are setting your user up for a recurring payment. You can also reduce the data that is returned in the response by hiding certain elements. This is done using the hide field and allows the request to omit token details and/or the list of fields associated with each of the payment products.


        By submitting the locale you can benefit from the server side language packs if your application does not have these or if you are using the JavaScript SDK. Note however that the values returned will be based on our default language packs and will not contain any specific modifications that you might have made through the Configuration Center. Our iOS and Android SDKs have language packs included which allows for easy management.
      tags:
      - ClientProducts
      operationId: GetClientPaymentProducts
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: Locale used in the GUI towards the consumer.
        schema:
          type: string
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      - name: operationType
        in: query
        description: >-
          This allows you to filter payment products based on the operation type. Allowed values:

          * Authorization - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days.

          * Pre-authorization - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount.

          * Sale - The payment creation results in an authorization that is already captured at the moment of approval.

          * Payout - Payout service enables seamless direct money transfers to a chosen bank account.
        schema:
          type: string
        required: false
      responses:
        200:
          x-nullable: true
          description: The response contains an array of payment products that match the filters supplied in the request.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPaymentProductsResponse'
                description: The response contains an array of payment products that match the filters supplied in the request.
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/products/{paymentProductId}:
    get:
      summary: Get payment product
      description: Use this API if you are just interested in one payment product or if you have filtered out the fields in the Get payment products call and you want to retrieve the field details on a single payment product. By doing it in this two-step process you will reduce the amount of data that needs to be transported back to your client, but you will have to make two calls. Usually the performance penalty is bigger when you need to do multiple calls with a small response package than one call with a bigger response package. You are however free to choose the best solution for your use case. You can submit additional details on the transaction as filters. In that case, the payment product is returned only if it matches the filters, otherwise a 400 response is returned.
      tags:
      - ClientProducts
      operationId: GetClientPaymentProduct
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      - name: paymentProductId
        in: path
        description: The payment product identifier.
        schema:
          type: integer
          format: int32
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code of the transaction
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: Locale used in the GUI towards the consumer.
        schema:
          type: string
      - name: amount
        in: query
        description: Whole amount in cents (not containing any decimals)
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter payment products based on their support for recurring payments.

          * true - return only payment products that support recurring payments,

          * false - return all payment products that support one-time transactions. Payment products that support recurring products are usually also part of this list.
        schema:
          type: boolean
      - name: hide
        in: query
        description: >-
          Allows you to hide elements from the response, reducing the amount of data that needs to be returned to your client. Possible options are:

          * fields - Do not return any data on fields of the payment product

          * accountsOnFile - Do not return any accounts on file data

          * translations - Do not return any label texts associated with the payment products

          * productsWithoutFields - Do not return products that require any additional data to be captured

          * productsWithoutInstructions - Do not return products that show instructions

          * productsWithRedirects - Do not return products that require a redirect to a 3rd party. Note that products that involve potential redirects related to 3D Secure authentication are not hidden
        schema:
          type: array
          items:
            type: string
        style: form
        explode: false
      - name: operationType
        in: query
        description: >-
          This allows you to filter payment products based on the operation type. Allowed values:

          * Authorization - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days.

          * Pre-authorization - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount.

          * Sale - The payment creation results in an authorization that is already captured at the moment of approval.

          * Payout - Payout service enables seamless direct money transfers to a chosen bank account.
        schema:
          type: string
        required: false
      responses:
        200:
          x-nullable: true
          description: The response contains the details of just one payment product.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProduct'
                description: Payment product
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/products/{paymentProductId}/networks:
    get:
      summary: Get payment product networks
      description: Mobile payment methods (e.g., Apple Pay, Google Pay) use networks like "VISA" for actual payments. The consumer's device should display cards using available networks. This endpoint lists networks for the current payment context. The returned networks depend on query parameters, payment product support, and configurations.
      tags:
      - ClientProducts
      operationId: GetClientPaymentProductNetworks
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      - name: paymentProductId
        in: path
        description: Payment product identifier. Please see payment products for a full overview of possible values.
        schema:
          type: integer
          format: int32
        required: true
      - name: countryCode
        in: query
        description: ISO 3166-1 alpha-2 country code
        schema:
          type: string
        required: true
      - name: currencyCode
        in: query
        description: Three-letter ISO currency code representing the currency for the amount
        schema:
          type: string
        required: true
      - name: amount
        in: query
        description: Amount in cents and always having 2 decimals
        schema:
          type: integer
          format: int64
      - name: isRecurring
        in: query
        description: >-
          This allows you to filter networks based on their support for recurring or not

          * true

          * false
        schema:
          type: boolean
      responses:
        200:
          x-nullable: true
          description: 'An array of networks is returned. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentProductNetworksResponse'
                description: 'Array containing network entries for a payment product. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: When no networks can be found matching the input criteria a HTTP 404 response is returned.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/services/getIINdetails:
    post:
      summary: Get IIN details
      description: This call verifies card processability and suggests the best card type based on configuration, using the initial 6 or more digits. Some cards are dual branded, having both local and international brands, and may not be returned if not supported. Once the first 6 digits are captured, use this API to validate card type and acceptance. The resulting paymentProductId can be used to display the suitable payment product logo for user feedback.
      tags:
      - ClientServices
      operationId: GetClientIINdetails
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/getIINDetailsRequest'
              description: Input for the retrieval of the IIN details request
      responses:
        200:
          x-nullable: true
          description: The IIN submitted in your request matches a known card type that is configured for your account. The response contains information on that card type.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getIINDetailsResponse'
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: When the IIN does not match any of the products that are configured on your account an HTTP 404 response is returned. This means that we will not be able to process this card, most likely due to the fact that your account is not set up for certain specific card products that the consumer is trying to make the payment with.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v2/{customerId}/services/dccrate:
    post:
      summary: Get currency conversion quote
      description: This call returns the currency conversion rate.
      tags:
      - ClientServices
      operationId: GetClientDccRateInquiryApi
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/clientCurrencyConversionRequest'
        required: true
        description: Currency conversion request
      responses:
        200:
          description: Successfully returns currency conversion quote
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/currencyConversionResponse'
                description: Payload of the response to a rate inquiry request
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/mandates:
    post:
      summary: Create mandate
      description: Creates a mandate to be used in a SEPA Direct Debit payment.
      tags:
      - Mandates
      operationId: CreateMandateApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createMandateRequest'
              description: Object containing information to create a SEPA Direct Debit mandate.
        required: true
      responses:
        201:
          x-nullable: true
          description: The create mandate request succeeded. Data on the resulting mandate can be found in the response.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/createMandateResponse'
                description: Object containing the Create Mandate response
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/mandates/{uniqueMandateReference}:
    get:
      summary: Get mandate
      description: Gets a created mandate to be used in a SEPA Direct Debit payment.
      tags:
      - Mandates
      operationId: GetMandateApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: uniqueMandateReference
        in: path
        description: The Unique Mandate Reference
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: The get mandate request succeeded. Data on the resulting mandate can be found in the response.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getMandateResponse'
                description: Object containing the Get Mandate response
        404:
          x-nullable: true
          description: Mandate not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/mandates/{uniqueMandateReference}/block:
    post:
      summary: Block mandate
      description: Updates the mandate status to BLOCKED.
      tags:
      - Mandates
      operationId: BlockMandateApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: uniqueMandateReference
        in: path
        description: The Unique Mandate Reference
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Blocking the mandate succeeded. Data can be found in the body.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getMandateResponse'
                description: Object containing the Get Mandate response
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: Mandate not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/mandates/{uniqueMandateReference}/unblock:
    post:
      summary: Unblock mandate
      description: Updates the mandate status from BLOCKED to ACTIVE.
      tags:
      - Mandates
      operationId: UnblockMandateApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: uniqueMandateReference
        in: path
        description: The Unique Mandate Reference
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Unblocking the mandate succeeded. Data can be found in the body.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getMandateResponse'
                description: Object containing the Get Mandate response
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: Mandate not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/mandates/{uniqueMandateReference}/revoke:
    post:
      summary: Revoke mandate
      description: Updates the mandate status to REVOKED.
      tags:
      - Mandates
      operationId: RevokeMandateApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: uniqueMandateReference
        in: path
        description: The Unique Mandate Reference
        schema:
          type: string
        required: true
      responses:
        200:
          x-nullable: true
          description: Revoking the mandate succeeded. Data can be found in the body.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getMandateResponse'
                description: Object containing the Get Mandate response
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        404:
          x-nullable: true
          description: Mandate not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/services/privacypolicy:
    get:
      summary: Get Privacy Policy
      description: Fetch a formated privacy policy for selected merchant and payment product IDs.
      tags:
      - PrivacyPolicy
      operationId: GetPrivacyPolicyApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: locale
        in: query
        description: Locale in which the privacy policy will be returned.
        schema:
          type: string
          default: en_US
        required: false
        example: fr_FR
      - name: paymentProductId
        in: query
        description: ID of the specific payment product for which you wish to retrieve the privacy policy. When none is provided you will receive a complete policy for all the payment methods available for the specified merchantId.
        schema:
          type: integer
        required: false
        example: 3012
      responses:
        200:
          x-nullable: true
          description: Your request was executed successfully and the response contains any applicable privacy policy texts.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/getPrivacyPolicyResponse'
                description: Object containing the privacy policy.
        400:
          x-nullable: true
          description: Incorrect input data
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/services/surchargecalculation:
    post:
      summary: Surcharge Calculation
      tags:
      - Services
      operationId: SurchargeCalculation
      parameters:
      - name: merchantId
        in: path
        description: The merchant identifier
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/calculateSurchargeRequest'
        required: true
      responses:
        200:
          description: The surcharge calculation was successful. The response contains the calculated surcharge, requested amount, total amount and surcharge rate information if a surcharge was applicable. If no surcharge was applicable, the response will contain a surcharge amount of zero.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/calculateSurchargeResponse'
        400:
          description: Bad Request
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /client/v1/{customerId}/services/surchargecalculation:
    post:
      summary: Surcharge Calculation
      tags:
      - ClientServices
      operationId: ClientSurchargeCalculation
      parameters:
      - name: customerId
        in: path
        description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/clientCalculateSurchargeRequest'
        required: true
      responses:
        200:
          description: The surcharge calculation was successful. The response contains the calculated surcharge, requested amount, total amount and surcharge rate information if a surcharge was applicable. If no surcharge was applicable, the response will contain a surcharge amount of zero.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/calculateSurchargeResponse'
        400:
          description: Bad Request
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/paymentlinks:
    post:
      summary: Create payment link
      description: >-
        You can create a payment link by posting the relevant details to the endpoint. We will then return you the created payment link.

         Any value provided for `hostedCheckoutSpecificInput.sessionTimeout` will be ignored. The payment link session will remain valid until the payment link is paid, cancelled or exceeds the payment link expiration date, whichever happens first.
      tags:
      - PaymentLinks
      operationId: CreatePaymentLinkApi
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      requestBody:
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/createPaymentLinkRequest'
              description: An object containing the Create PaymentLink request.
        required: true
      responses:
        201:
          x-nullable: true
          description: Created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentLinkResponse'
                description: An object representing a payment link.
        400:
          x-nullable: true
          description: Bad request
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/paymentlinks/{paymentLinkId}:
    get:
      summary: Get payment link by ID
      description: Retrieves the details of payment link with the paymentLinkId provided in the URL.
      tags:
      - PaymentLinks
      operationId: GetPaymentLinkById
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentLinkId
        in: path
        description: id of the paymentLink
        schema:
          type: string
        required: true
      responses:
        200:
          description: Returns unique paymentLink
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/paymentLinkResponse'
                description: An object representing a payment link.
        404:
          x-nullable: true
          description: PaymentLink not found
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
  /v2/{merchantId}/paymentlinks/{paymentLinkId}/cancel:
    post:
      summary: Cancel PaymentLink by ID
      description: Cancels the payment link with paymentLinkId provided in the URL.
      tags:
      - PaymentLinks
      operationId: CancelPaymentLinkById
      parameters:
      - name: merchantId
        in: path
        description: The Merchant Id
        schema:
          type: string
        required: true
      - name: paymentLinkId
        in: path
        description: id of the paymentLink
        schema:
          type: string
        required: true
      responses:
        204:
          description: Payment link was cancelled successfully.
        404:
          x-nullable: true
          description: The requested payment link was not found.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
        409:
          x-nullable: true
          description: The requested payment link was found, but it is in a state in which it can't be updated. Only links with status active can be cancelled.
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/errorResponse'
components:
  schemas:
    feedbacks:
      type: object
      description: This section will contain feedback Urls to provide feedback on the payment.
      properties:
        webhookUrl:
          type: string
          description: The URL where the webhook will be dispatched for all status change events related to this payment.
          example: https://www.example.com
          maxLength: 325
    discount:
      type: object
      description: Object to apply a discount to the total basket by adding a discount line.
      properties:
        amount:
          type: integer
          format: int64
          description: >-
            Amount in the smallest currency unit, i.e.:


            * EUR is a 2-decimals currency, the value 1234 will result in EUR 12.34


            * KWD is a 3-decimals currency, the value 1234 will result in KWD 1.234


            * JPY is a zero-decimal currency, the value 1234 will result in JPY 1234
          example: 1000
          maximum: 999999999999
          minimum: 0
    cancelPaymentResponse:
      type: object
      properties:
        payment:
          $ref: '#/components/schemas/paymentResponse'
          description: Object that holds the payment related properties
    cancelPaymentRequest:
      type: object
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        isFinal:
          $ref: '#/components/schemas/isFinal'
          description: This property indicates whether this will be the final operation. The default value for this property is false.
    hostedCheckoutSpecificOutput:
      type: object
      description: Hosted Checkout specific information. Populated if the payment was created on the platform through a Hosted Checkout.
      properties:
        hostedCheckoutId:
          $ref: '#/components/schemas/hostedCheckoutId'
          description: The ID of the Hosted Checkout Session in which the payment was made.
        variant:
          $ref: '#/components/schemas/variant'
          description: It is possible to upload multiple templates of your payment pages using the Merchant Portal. You can force the use of a custom template by specifying it in the variant field. This allows you to test out the effect of certain changes to your payment pages in a controlled manner. Please note that you need to specify the filename of the template or customization.
    paymentOutput:
      type: object
      description: Object containing payment details
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        merchantParameters:
          $ref: '#/components/schemas/merchantParameters'
          description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
          deprecated: true
          x-deprecated-by: references/merchantParameters
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
        amountPaid:
          type: integer
          format: int64
          description: Amount that has been paid. This is deprecated. Use acquiredAmount instead.
          deprecated: true
          x-deprecated-by: acquiredAmount
        acquiredAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Amount that has been acquired by the Acquirer
        customer:
          $ref: '#/components/schemas/customerOutput'
          description: Object containing the details of the customer
        cardPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificOutput'
          description: Object containing the card payment method details
        mobilePaymentMethodSpecificOutput:
          $ref: '#/components/schemas/mobilePaymentMethodSpecificOutput'
          description: Object containing the mobile payment method details
        paymentMethod:
          $ref: '#/components/schemas/paymentMethod'
          description: Payment method identifier used by the our payment engine.
        redirectPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/redirectPaymentMethodSpecificOutput'
          description: Object containing the redirect payment product details
        sepaDirectDebitPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentMethodSpecificOutput'
          description: Object containing the SEPA direct debit details
        surchargeSpecificOutput:
          $ref: '#/components/schemas/surchargeSpecificOutput'
          description: Object containing specific surcharging attributes applied to an order.
        discount:
          $ref: '#/components/schemas/discount'
          description: Object to apply a discount to the total basket by adding a discount line.
    amountOfMoney:
      type: object
      description: Object containing amount and ISO currency code attributes
      properties:
        amount:
          type: integer
          format: int64
          description: >-
            Amount in the smallest currency unit, i.e.:

            * EUR is a 2-decimals currency, the value 1234 will result in EUR 12.34

            * KWD is a 3-decimals currency, the value 1234 will result in KWD 1.234

            * JPY is a zero-decimal currency, the value 1234 will result in JPY 1234
          example: 1000
          maximum: 999999999999
          minimum: 0
        currencyCode:
          type: string
          description: Three-letter ISO currency code representing the currency for the amount
          example: EUR
          minLength: 3
          maxLength: 3
      required:
      - amount
      - currencyCode
    paymentReferences:
      type: object
      description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
      properties:
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
        merchantParameters:
          $ref: '#/components/schemas/merchantParameters'
          description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
    operationPaymentReferences:
      type: object
      description: Object that holds all reference properties that are linked to this transaction
      properties:
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
    cardPaymentMethodSpecificOutput:
      type: object
      description: Object containing the card payment method details
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        authorisationCode:
          $ref: '#/components/schemas/authorisationCode'
          description: Card Authorization code as returned by the acquirer
        card:
          $ref: '#/components/schemas/cardEssentials'
          description: Object containing card details
        fraudResults:
          $ref: '#/components/schemas/cardFraudResults'
          description: Fraud results contained in the CardFraudResults object
        initialSchemeTransactionId:
          $ref: '#/components/schemas/initialSchemeTransactionId'
          description: The unique scheme transactionId of the initial transaction that was performed with SCA. In case this is unknown a scheme transactionId of an earlier transaction part of the same sequence can be used as a fall-back. Strongly advised to be submitted for any MerchantInitiated or recurring transaction (a subsequent one).
        schemeReferenceData:
          $ref: '#/components/schemas/schemeReferenceData'
          description: This is the unique Scheme Reference Data from the initial transaction that was performed with a Strong Customer Authentication. In case this value is unknown, a Scheme Reference of an earlier transaction that was part of the same sequence can be used as a fall-back. Still, it is strongly advised to submit this value for any Merchant Initiated Transaction or any recurring transaction (hereby defined as "Subsequent").
        paymentAccountReference:
          $ref: '#/components/schemas/paymentAccountReference'
          description: The Payment Account Reference is a unique alphanumeric identifier that links a PAN with all subsequent PANs for the same payment account (e.g., following card replacement) and all EMV payment tokens associated with that account. On its own Payment Account Reference cannot be used to start financial transactions, but it does allow for complying with regulatory requirements, performing risk analysis & supporting loyalty programs. Please note that the Payment Account Reference is a value returned after an authorization & only if provided by the acquirer and/or the issuer.
        threeDSecureResults:
          $ref: '#/components/schemas/threeDSecureResults'
          description: 3D Secure results object
        token:
          $ref: '#/components/schemas/tokenIdOutput'
          description: ID of the token. This property is populated when the payment was done with a token or when the payment was tokenized.
        paymentOption:
          $ref: '#/components/schemas/paymentOption'
          description: 'The specific payment option for the payment. To be used as a complement of the more generic paymentProductId (oney, banquecasino, cofidis), which allows to define a variation of the selected paymentProductId (ex: facilypay3x, banquecasino4x, cofidis3x-sansfrais, ...). List of modalities included in the payment product page.'
        externalTokenLinked:
          $ref: '#/components/schemas/externalTokenLinked'
        authenticatedAmount:
          $ref: '#/components/schemas/authenticationAmount'
          description: The amount to be authenticated. This field should be populated if the amount to be authenticated differs from the amount to be authorized (by default they are considered equal). Amount in cents and always having 2 decimals.
        currencyConversion:
          $ref: '#/components/schemas/currencyConversion'
        paymentProduct3208SpecificOutput:
          $ref: '#/components/schemas/paymentProduct3208SpecificOutput'
          description: OneyDuplo Leroy Merlin specific details
        paymentProduct3209SpecificOutput:
          $ref: '#/components/schemas/paymentProduct3209SpecificOutput'
          description: OneyDuplo Alcampo specific details
        acquirerInformation:
          $ref: '#/components/schemas/acquirerInformation'
          description: Information about the acquirer used to process the transaction
    paymentProduct3208SpecificOutput:
      type: object
      description: OneyDuplo Leroy Merlin specific details
      properties:
        buyerCompliantBankMessage:
          $ref: '#/components/schemas/buyerCompliantBankMessage'
          description: This field indicates the text that must be returned and shown to the buyer to be compliant with the law regulating this payment product.
    paymentProduct3209SpecificOutput:
      type: object
      description: OneyDuplo Alcampo specific details
      properties:
        buyerCompliantBankMessage:
          $ref: '#/components/schemas/buyerCompliantBankMessage'
          description: This field indicates the text that must be returned and shown to the buyer to be compliant with the law regulating this payment product.
    acquirerInformation:
      type: object
      description: Information about the acquirer used to process the transaction
      properties:
        name:
          type: string
          description: Name of the acquirer used to process the transaction
        acquirerSelectionInformation:
          $ref: '#/components/schemas/acquirerSelectionInformation'
          description: Information about the acquirer selection
    acquirerSelectionInformation:
      type: object
      description: Information about the acquirer selection
      properties:
        result:
          type: string
          description: Result of the acquirer selection operation
          enum:
          - NO_MATCHING_RULESET
          - RULE_MATCHED
        ruleName:
          type: string
          description: Name of the rule used to select the acquirer
        fallbackLevel:
          type: integer
          format: int32
          description: Specifies whether a fallback occurred from the primary choice of the acquirer selection service.
    cardEssentials:
      type: object
      description: Object containing card details
      properties:
        cardNumber:
          type: string
          description: The masked credit/debit card number
          maxLength: 19
        expiryDate:
          type: string
          description: >-
            Expiry date of the card 
             Format: MMYY
          example: 0529
          maxLength: 4
        bin:
          type: string
          description: The first digits of the credit card number from left to right with a minimum of 6 digits.
        countryCode:
          $ref: '#/components/schemas/countryCode'
          description: ISO 3166-1 alpha-2 country code
    cardFraudResults:
      type: object
      description: Fraud results contained in the CardFraudResults object
      properties:
        fraudServiceResult:
          $ref: '#/components/schemas/fraudServiceResult'
          description: >-
            Resulting advice of the fraud prevention checks. Possible values are:

            * accepted - Based on the checks performed the transaction can be accepted

            * challenged - Based on the checks performed the transaction should be manually reviewed

            * denied - Based on the checks performed the transaction should be rejected

            * no-advice - No fraud check was requested/performed

            * error - The fraud check resulted an error. Note that the fraud check was thus not performed.
        avsResult:
          type: string
          description: >2-
             Result of the Address Verification Service checks. Possible values are: 
             * A - Address (Street) matches, Zip does not 
             * B - Street address match for international transactionsâ€”Postal code not verified due to incompatible formats 
             * C - Street address and postal code not verified for international transaction due to incompatible formats 
             * D - Street address and postal code match for international transaction, cardholder name is incorrect 
             * E - AVS error 
             * F - Address does match and five digit ZIP code does match (UK only) 
             * G - Address information is unavailable; international transaction; non-AVS participant 
             * H - Billing address and postal code match, cardholder name is incorrect (Amex) 
             * I - Address information not verified for international transaction 
             * K - Cardholder name matches (Amex) 
             * L - Cardholder name and postal code match (Amex) 
             * M - Cardholder name, street address, and postal code match for international transaction 
             * N - No Match on Address (Street) or Zip 
             * O - Cardholder name and address match (Amex) 
             * P - Postal codes match for international transactionâ€”Street address not verified due to incompatible formats 
             * Q - Billing address matches, cardholder is incorrect (Amex) 
             * R - Retry, System unavailable or Timed out 
             * S - Service not supported by issuer 
             * U - Address information is unavailable 
             * W - 9 digit Zip matches, Address (Street) does not 
             * X - Exact AVS Match 
             * Y - Address (Street) and 5 digit Zip match 
             * Z - 5 digit Zip matches, Address (Street) does not 
             * 0 - No service available
        cvvResult:
          type: string
          description: >2-
             Result of the Card Verification Value checks. Possible values are: 
             * M - CVV check performed and valid value 
             * N - CVV checked and no match 
             * P - CVV check not performed, not requested 
             * S - Cardholder claims no CVV code on card, issuer states CVV-code should be on card 
             * U - Issuer not certified for CVV2 
             * Y - Server provider did not respond 
             * 0 - No service available
    threeDSecureResults:
      type: object
      description: 3D Secure results object
      properties:
        version:
          type: string
          description: 3D Secure Protocol version used during this transaction.
          example: 2.2.0
        flow:
          $ref: '#/components/schemas/threeDSecureFlow'
          description: 3D Secure Flow used during this transaction.
        cavv:
          type: string
          description: Cardholder Authentication Verification Value. End-2-end reference generated by the Issuer to recognize that the authentication has taken place.
        eci:
          type: string
          description: Indicates Authentication validation results returned after AuthenticationValidation
        schemeEci:
          type: string
          description: 3D Secure ECI (Electronic Commerce Indicator) depending on the Scheme. Returned by DS.
          example: 5
          maxLength: 2
        authenticationStatus:
          type: string
          description: >-
            One-letter authentication status returned by DS. Possible values are:

            * Y - Authentication succeeded

            * A - Authentication attempted

            * I - Information only, liability shifted to the merchant

            * N - Authentication failed

            * R - Authentication rejected

            * U - Authentication unavailable

            * C - Authentication required
          example: Y
          minLength: 1
          maxLength: 1
        acsTransactionId:
          type: string
          description: Authenticated Transaction Identifier at the ACS/Issuer.
          example: 3E1D57DF-8DB1-4614-91D5-B11962519703
          maxLength: 36
        dsTransactionId:
          type: string
          description: 3D Secure Directory Server Transaction Identifier used for this transaction.
          example: 3E1D57DF-8DB1-4614-91D5-B11962519703
        xid:
          type: string
          description: Transaction ID for the Authentication
        challengeIndicator:
          type: string
          description: Challenge Indicator used for this transaction. This value might differ from the one sent by the merchant if the card is not supporting it (3DS version 2.1 vs 3DS version 2.2).
          enum:
          - no-preference
          - no-challenge-requested
          - challenge-requested
          - challenge-required
          - no-challenge-requested-risk-analysis-performed
          - no-challenge-requested-data-share-only
          - no-challenge-requested-consumer-authentication-performed
          - no-challenge-requested-use-whitelist-exemption
          - challenge-requested-whitelist-prompt-requested
          - request-scoring-without-connecting-to-acs
        liability:
          type: string
          description: >-
            Determines the Fraud liability. Possible values are:

            * issuer - Fraud liability shifts to the issuer

            * merchant - Fraud liability with the merchant 


            Note: When not filled in Fraud liability is not applicable for the current transaction.
          enum:
          - issuer
          - merchant
        appliedExemption:
          type: string
          description: Exemption requested and applied in the authorization
          enum:
          - low-value
          - merchant-acquirer-transaction-risk-analysis
          - whitelisting
          - corporate
          - delegate-authentication
        exemptionEngineFlow:
          type: string
          description: Detailed description of the Exemption Engine outcomes
          enum:
          - not-applicable-challenge-indicator-received
          - not-applicable-scheme-not-supported
          - not-applicable-acquirer-not-supported
          - fraud-result-not-present
          - fraud-result-red-transaction-blocked
          - fraud-result-red-overridable-sca-requested-challenge-indicator-mandate
          - fraud-result-orange-sca-requested-challenge-indicator-merchant-preference
          - fraud-result-green-amount-above-500-euro-sca-requested-challenge-indicator-no-preference
          - tra-accepted
          - tra-soft-declined-retry-with-sca-challenge-indicator-mandate
          - low-value-not-applicable-sca-requested-challenge-indicator-no-challenge-requested
          - low-value-accepted
          - low-value-soft-declined-retry-with-sca-challenge-indicator-mandate
          - cb-lrmp-accepted
          - cb-lrmp-not-approved
          - cb-tra-accepted
          - cb-tra-not-approved
          - cb-low-value-accepted
          - cb-low-value-not-approved
          - acquirer-out-of-psd2-scope
    threeDSecureFlow:
      type: string
      description: 3D Secure Flow used during this transaction.
      enum:
      - frictionless
      - challenge
    mobilePaymentMethodSpecificOutput:
      type: object
      description: Object containing the mobile payment method details
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        authorisationCode:
          $ref: '#/components/schemas/authorisationCode'
          description: Card Authorization code as returned by the acquirer
        fraudResults:
          $ref: '#/components/schemas/cardFraudResults'
          description: Fraud results contained in the CardFraudResults object
        paymentData:
          $ref: '#/components/schemas/mobilePaymentData'
          description: Object containing payment details
        threeDSecureResults:
          $ref: '#/components/schemas/threeDSecureResults'
          description: 3D Secure results object
        network:
          $ref: '#/components/schemas/network'
          description: The card network that was used for a mobile payment method operation
    mobilePaymentData:
      type: object
      description: Object containing payment details
      properties:
        dpan:
          type: string
          description: The obfuscated DPAN. Only the last four digits are visible.
          maxLength: 19
        expiryDate:
          type: string
          description: 'Expiry date of the tokenized card. Format: MMYY'
          example: 0529
          maxLength: 4
    redirectPaymentMethodSpecificOutput:
      type: object
      description: Object containing the redirect payment product details
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        fraudResults:
          $ref: '#/components/schemas/fraudResults'
          description: Object containing the results of the fraud screening
        paymentProduct840SpecificOutput:
          $ref: '#/components/schemas/paymentProduct840SpecificOutput'
          description: PayPal (payment product 840) specific details
        paymentProduct5001SpecificOutput:
          $ref: '#/components/schemas/paymentProduct5001SpecificOutput'
          description: Bizum (payment product 5001) specific details
        paymentProduct5500SpecificOutput:
          $ref: '#/components/schemas/paymentProduct5500SpecificOutput'
          description: Multibanco (payment product 5500) specific details
        paymentProduct5402SpecificOutput:
          $ref: '#/components/schemas/paymentProduct5402SpecificOutput'
          description: Meal vouchers (payment product 5402) specific details
        paymentProduct3203SpecificOutput:
          $ref: '#/components/schemas/paymentProduct3203SpecificOutput'
          description: PostFinancePay (payment product 3203) specific details
        paymentOption:
          $ref: '#/components/schemas/paymentOption'
          description: 'The specific payment option for the payment. To be used as a complement of the more generic paymentProductId (oney, banquecasino, cofidis), which allows to define a variation of the selected paymentProductId (ex: facilypay3x, banquecasino4x, cofidis3x-sansfrais, ...). List of modalities included in the payment product page.'
        token:
          $ref: '#/components/schemas/tokenIdOutput'
          description: ID of the token. This property is populated when the payment was done with a token or when the payment was tokenized.
        customerBankAccount:
          $ref: '#/components/schemas/customerBankAccount'
          description: Data of customer bank account
        authorisationCode:
          $ref: '#/components/schemas/authorisationCode'
          description: Card Authorization code as returned by the acquirer
    fraudResults:
      type: object
      description: Object containing the results of the fraud screening
      properties:
        fraudServiceResult:
          $ref: '#/components/schemas/fraudServiceResult'
          description: >-
            Resulting advice of the fraud prevention checks. Possible values are:

            * accepted - Based on the checks performed the transaction can be accepted

            * challenged - Based on the checks performed the transaction should be manually reviewed

            * denied - Based on the checks performed the transaction should be rejected

            * no-advice - No fraud check was requested/performed

            * error - The fraud check resulted an error. Note that the fraud check was thus not performed.
    paymentProduct840SpecificOutput:
      type: object
      description: PayPal (payment product 840) specific details
      properties:
        billingAddress:
          $ref: '#/components/schemas/address'
          description: Object containing billing address details.
        customerAccount:
          $ref: '#/components/schemas/paymentProduct840CustomerAccount'
          description: Object containing the details of the PayPal account
        customerAddress:
          $ref: '#/components/schemas/address'
          description: Object containing billing address details.
        protectionEligibility:
          $ref: '#/components/schemas/protectionEligibility'
          description: Kind of seller protection in force for the PayPal transaction
    paymentProduct5001SpecificOutput:
      type: object
      description: Bizum (payment product 5001) specific details
      properties:
        authorisationCode:
          type: string
          description: The reference returned by redsys to identify the transaction
        operationCode:
          type: string
          description: The reference returned by redsys to identify the operation
        mobilePhoneNumber:
          type: string
          description: The mobile phone number used for this transaction
        accountNumber:
          type: string
          description: The account number used for this transaction
        liability:
          type: string
          description: >-
            Determines the Fraud liability. Possible values are:

            * issuer - Fraud liability shifts to the issuer (eq. exemption accepted)

            * merchant - Fraud liability with the merchant 

            Note: When not filled in, Fraud liability is not applicable for the current transaction.
          enum:
          - issuer
          - merchant
    paymentProduct5500SpecificOutput:
      type: object
      description: Multibanco (payment product 5500) specific details
      properties:
        paymentReference:
          type: string
          description: The reference to be used within the Multibanco network to confirm the payment
        paymentStartDate:
          type: string
          description: The start date of the payment validity
        paymentEndDate:
          type: string
          description: The end date of the payment validity
        entityId:
          type: string
          description: The reference to be used during Multibanco payment for reconciliation matter
          example: 12538
          maxLength: 10
    paymentProduct5402SpecificOutput:
      type: object
      description: Meal vouchers (payment product 5402) specific details
      properties:
        brand:
          type: string
          description: The specific meal voucher brand used for the transaction
    paymentProduct3203SpecificOutput:
      type: object
      description: PostFinancePay (payment product 3203) specific details
      properties:
        billingAddress:
          $ref: '#/components/schemas/addressPersonal'
          description: Object containing address information
        shippingAddress:
          $ref: '#/components/schemas/addressPersonal'
          description: Object containing address information
    customerBankAccount:
      type: object
      description: Data of customer bank account
      properties:
        iban:
          type: string
          description: The IBAN is the International Bank Account Number. It is an internationally agreed format for the BBAN and includes the ISO country code and two check digits.
          example: BE01 0123 4567 8910
          maxLength: 50
        bic:
          type: string
          description: Bank Identification Code
          example: BOHIUS77
          maxLength: 11
        accountHolderName:
          type: string
          description: Name of account holder
          example: John Doe
    address:
      type: object
      description: Object containing billing address details.
      properties:
        additionalInfo:
          $ref: '#/components/schemas/additionalInfo'
          description: Second line of street or additional address information
        city:
          $ref: '#/components/schemas/city'
          description: City
        countryCode:
          $ref: '#/components/schemas/countryCode'
          description: ISO 3166-1 alpha-2 country code
        houseNumber:
          $ref: '#/components/schemas/houseNumber'
          description: House number
        state:
          $ref: '#/components/schemas/state'
          description: ISO 3166-2 country subdivision code
        street:
          $ref: '#/components/schemas/street'
          description: Street name
        zip:
          type: string
          description: Zip code
          example: 1930
          x-trim-at: 10
    paymentProduct840CustomerAccount:
      type: object
      description: Object containing the details of the PayPal account
      properties:
        accountId:
          type: string
          description: Username with which the PayPal account holder has registered at PayPal
          example: customer-account@email.com
        companyName:
          type: string
          description: Name of the company in case the PayPal account is owned by a business
          example: Customer Company Name
        countryCode:
          $ref: '#/components/schemas/countryCode'
          description: ISO 3166-1 alpha-2 country code
        customerAccountStatus:
          type: string
          description: >-
            Status of the PayPal account

            * verified - PayPal has verified the funding means for this account

            * unverified - PayPal has not verified the funding means for this account
          example: verified
        customerAddressStatus:
          type: string
          description: >-
            Status of the customer's shipping address as registered by PayPal

            * none - Status is unknown at PayPal

            * confirmed - The address has been confirmed

            * unconfirmed - The address has not been confirmed
          example: confirmed
        firstName:
          type: string
          description: First name of the PayPal account holder
          example: John
        payerId:
          $ref: '#/components/schemas/payerId'
          description: The unique identifier of a PayPal account and will never change in the life cycle of a PayPal account
        surname:
          type: string
          description: Surname of the PayPal account holder
          example: Doe
    protectionEligibility:
      type: object
      description: Kind of seller protection in force for the PayPal transaction
      properties:
        eligibility:
          type: string
          description: >-
            * Eligible - Merchant is protected by PayPal's Seller Protection Policy for Unauthorized Payment and Item Not Received

            * PartiallyEligible - Merchant is protected by PayPal's Seller Protection Policy for Item Not Received

            * Ineligible â€” Merchant is not protected under the Seller Protection Policy
          example: Eligible
        type:
          type: string
          description: >-
            - ItemNotReceivedEligible - Merchant is protected by PayPal's Seller Protection Policy for Item Not Received

            - UnauthorizedPaymentEligible - Merchant is protected by PayPal's Seller Protection Policy for Unauthorized Payment

            - Ineligible - Merchant is not protected under the Seller Protection Policy
          example: ItemNotReceivedEligible
    sepaDirectDebitPaymentMethodSpecificOutput:
      type: object
      description: Object containing the SEPA direct debit details
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        fraudResults:
          $ref: '#/components/schemas/fraudResults'
          description: Object containing the results of the fraud screening
        paymentProduct771SpecificOutput:
          $ref: '#/components/schemas/paymentProduct771SpecificOutput'
          description: Output that is SEPA Direct Debit specific (i.e. the used mandate)
    paymentProduct771SpecificOutput:
      type: object
      description: Output that is SEPA Direct Debit specific (i.e. the used mandate)
      properties:
        mandateReference:
          type: string
          description: Unique reference to a Mandate
    paymentStatusOutput:
      type: object
      description: This object has the numeric representation of the current payment status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
      properties:
        errors:
          $ref: '#/components/schemas/errors'
          description: Contains the set of errors
        isCancellable:
          type: boolean
          description: Flag indicating if the payment can be cancelled
        statusCategory:
          $ref: '#/components/schemas/statusCategoryValue'
          description: Highlevel status of the payment, payout or refund.
        statusCode:
          $ref: '#/components/schemas/statusCode'
          description: Numeric status code of the legacy API. The value can also be found in the BackOffice and in report files.
        statusCodeChangeDateTime:
          $ref: '#/components/schemas/statusCodeChangeDateTime'
          description: Timestamp of the latest status change
        isAuthorized:
          type: boolean
          description: Indicates if the transaction has been authorized
        isRefundable:
          type: boolean
          description: Flag indicating if the payment can be refunded
    aPIError:
      type: object
      description: Contains detailed information on one single error.
      properties:
        errorCode:
          type: string
          description: Error code
          example: 50001130
        category:
          type: string
          description: >-
            Category the error belongs to. The category should give an indication of the type of error you are dealing with. Possible values:

            * DIRECT_PLATFORM_ERROR - indicating that a functional error has occurred in the platform.

            * PAYMENT_PLATFORM_ERROR - indicating that a functional error has occurred in the payment platform.

            * IO_ERROR - indicating that a technical error has occurred within the payment platform or between the payment platform and third party systems.
          example: PAYMENT_PLATFORM_ERROR
        code:
          type: string
          description: >-
            Deprecated: Use errorCode instead.

            Error code
          example: 50001130
          deprecated: true
          x-deprecated-by: errorCode
        httpStatusCode:
          type: integer
          format: int32
          description: HTTP status code for this error that can be used to determine the type of error
          example: 404
        id:
          type: string
          description: ID of the error. This is a short human-readable message that briefly describes the error.
          example: UNKNOWN_PAYMENT_ID
        message:
          type: string
          description: Human-readable error message that is not meant to be relayed to customer as it might tip off people who are trying to commit fraud
          example: Authorisation declined
        propertyName:
          type: string
          description: >-
            Returned only if the error relates to a value that was missing or incorrect.


            Contains a location path to the value as a JSonata query.


            Some common examples:

            * a.b selects the value of property b of root property a,

            * a[1] selects the first element of the array in root property a,

            * a[b='some value'] selects all elements of the array in root property a that have a property b with value 'some value'.
          example: paymentId
        retriable:
          type: boolean
          description: >-
            Flag indicating if the request is retriable. 

            Retriable requests mean that a technical error happened and that the same request can safely be sent again with a new idempotence key.
      required:
      - errorCode
    errorResponse:
      type: object
      properties:
        errorId:
          type: string
          description: Unique reference, for debugging purposes, of this error response
          example: 15eabcd5-30b3-479b-ae03-67bb351c07e6-00000092
        errors:
          type: array
          description: List of one or more errors
          items:
            $ref: '#/components/schemas/aPIError'
            description: Contains detailed information on one single error.
      required:
      - errorId
      - errors
    capturesResponse:
      type: object
      properties:
        captures:
          type: array
          description: The list of all captures performed on the requested payment.
          items:
            $ref: '#/components/schemas/capture'
    capture:
      type: object
      properties:
        id:
          type: string
        captureOutput:
          $ref: '#/components/schemas/captureOutput'
          description: Object containing capture details
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/captureStatusOutput'
          description: This object has the numeric representation of the current capture status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
    captureOutput:
      type: object
      description: Object containing capture details
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        merchantParameters:
          $ref: '#/components/schemas/merchantParameters'
          description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
          deprecated: true
          x-deprecated-by: references/merchantParameters
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
          deprecated: true
          x-deprecated-by: operationReferences
        operationReferences:
          $ref: '#/components/schemas/operationPaymentReferences'
          description: Object that holds all reference properties that are linked to this transaction
        amountPaid:
          type: integer
          format: int64
          description: Amount that has been paid. This is deprecated. Use acquiredAmount instead.
          deprecated: true
          x-deprecated-by: acquiredAmount
        acquiredAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Amount that has been acquired by the Acquirer
        cardPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificOutput'
          description: Object containing the card payment method details
        mobilePaymentMethodSpecificOutput:
          $ref: '#/components/schemas/mobilePaymentMethodSpecificOutput'
          description: Object containing the mobile payment method details
        paymentMethod:
          $ref: '#/components/schemas/paymentMethod'
          description: Payment method identifier used by the our payment engine.
        redirectPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/redirectPaymentMethodSpecificOutput'
          description: Object containing the redirect payment product details
        sepaDirectDebitPaymentMethodSpecificOutput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentMethodSpecificOutput'
          description: Object containing the SEPA direct debit details
        surchargeSpecificOutput:
          $ref: '#/components/schemas/surchargeSpecificOutput'
          description: Object containing specific surcharging attributes applied to an order.
    captureStatusOutput:
      type: object
      description: This object has the numeric representation of the current capture status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
      properties:
        statusCode:
          $ref: '#/components/schemas/statusCode'
          description: Numeric status code of the legacy API. The value can also be found in the BackOffice and in report files.
    captureResponse:
      type: object
      properties:
        captureOutput:
          $ref: '#/components/schemas/captureOutput'
          description: Object containing capture details
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/captureStatusOutput'
          description: This object has the numeric representation of the current capture status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
        id:
          $ref: '#/components/schemas/paymentId'
          description: Our unique payment transaction identifier
          example: 3066019730_1
    capturePaymentRequest:
      type: object
      properties:
        amount:
          type: integer
          format: int64
          description: >-
            Here you can specify the amount that you want to capture (specified in cents, where single digit currencies are presumed to have 2 digits). The amount can be lower than the amount that was authorized, but not higher. 
             If left empty, the full amount will be captured and the request will be final. 
             If the full amount is captured, the request will also be final.
        isFinal:
          $ref: '#/components/schemas/isFinal'
          description: This property indicates whether this will be the final operation. The default value for this property is false.
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
          deprecated: true
          x-deprecated-by: operationReferences
        operationReferences:
          $ref: '#/components/schemas/operationPaymentReferences'
          description: Object that holds all reference properties that are linked to this transaction
    completePaymentResponse:
      type: object
      properties:
        creationOutput:
          $ref: '#/components/schemas/paymentCreationOutput'
          description: 'Deprecated: This field is not used by any payment product'
          deprecated: true
          x-deprecated-by: none
        merchantAction:
          $ref: '#/components/schemas/merchantAction'
          description: 'Deprecated: This field is not used by any payment product'
          deprecated: true
          x-deprecated-by: none
        payment:
          $ref: '#/components/schemas/paymentResponse'
          description: Object that holds the payment related properties
    paymentCreationOutput:
      type: object
      description: Object containing the details of the created payment.
      properties:
        externalReference:
          type: string
          description: The external reference identifier for this transaction. Data in this property will also be returned in our report files, allowing you to reconcile them
        isNewToken:
          $ref: '#/components/schemas/isNewToken'
          description: >-
            Indicates if a new token was created 
             * true - A new token was created 
             * false - A token with the same card number already exists and is returned. Please note that the existing token has not been updated. When you want to update other data then the card number, you need to update data stored in the token explicitly, as data is never updated during the creation of a token.
        token:
          $ref: '#/components/schemas/tokenIdOutput'
          description: ID of the token. This property is populated when the payment was done with a token or when the payment was tokenized.
        tokenizationSucceeded:
          type: boolean
          description: Indicates if tokenization was successful or not. If this value is false, then the token and isNewToken properties will not be set.
    merchantAction:
      type: object
      description: Object that contains the action, including the needed data, that you should perform next, like showing instructions, showing the transaction results or redirect to a third party to complete the payment
      properties:
        actionType:
          type: string
          description: >-
            Action merchants needs to take in the online payment process. Possible values are: 
             * REDIRECT - The customer needs to be redirected using the details found in redirectData 
             * SHOW_FORM - The customer needs to be shown a form with the fields found in formFields. You can submit the data entered by the user in a Complete payment request. 
             * SHOW_INSTRUCTIONS - The customer needs to be shown payment instruction using the details found in showData. Alternatively the instructions can be rendered by us using the instructionsRenderingData 
             * SHOW_TRANSACTION_RESULTS - The customer needs to be shown the transaction results using the details found in showData. Alternatively the instructions can be rendered by us using the instructionsRenderingData 
             * MOBILE_THREEDS_CHALLENGE - The customer needs to complete a challenge as part of the 3D Secure authentication inside your mobile app. The details contained in mobileThreeDSecureChallengeParameters need to be provided to the EMVco certified Mobile SDK as a challengeParameters object. 
             * CALL_THIRD_PARTY - The merchant needs to call a third party using the data found in thirdPartyData
        redirectData:
          $ref: '#/components/schemas/redirectData'
          description: Object containing all data needed to redirect the customer
        showFormData:
          $ref: '#/components/schemas/showFormData'
          description: Object returned for the SHOW_FORM actionType.
        mobileThreeDSecureChallengeParameters:
          $ref: '#/components/schemas/mobileThreeDSecureChallengeParameters'
          description: Mobile 3D Secure Challenge Parameters
    redirectData:
      type: object
      description: Object containing all data needed to redirect the customer
      properties:
        RETURNMAC:
          type: string
          description: A Message Authentication Code (MAC) is used to authenticate the redirection back to merchant after the payment
          example: fecab85c-9b0e-42ee-a9d9-ebb69b0c2eb0
        redirectURL:
          type: string
          description: The URL that the customer should be redirected to. Be sure to redirect using the GET method
    showFormData:
      type: object
      description: Object returned for the SHOW_FORM actionType.
      properties:
        paymentProduct5407:
          $ref: '#/components/schemas/paymentProduct5407'
          description: Contains the third party data for payment product 5407 (Twint)
        paymentProduct5404:
          $ref: '#/components/schemas/paymentProduct5404'
          description: Contains the third party data for payment product 5404 (WeChat Pay)
        paymentProduct3012:
          $ref: '#/components/schemas/paymentProduct3012'
          description: Contains the third party data for payment product 3012 (Bancontact)
    mobileThreeDSecureChallengeParameters:
      type: object
      description: Mobile 3D Secure Challenge Parameters
      properties:
        acsReferenceNumber:
          type: string
          description: The unique identifier assigned by the EMVCo Secretariat upon testing and approval.
        acsSignedContent:
          type: string
          description: Contains the JWS object created by the ACS for the challenge (ARes).
        acsTransactionId:
          type: string
          description: The ACS Transaction ID for a prior 3-D Secure authenticated transaction (for example, the first recurring transaction that was authenticated with the customer).
        threeDServerTransactionId:
          type: string
          description: The 3-D Secure version 2 transaction ID that is used for the 3D Authentication
    paymentProduct5407:
      type: object
      description: Contains the third party data for payment product 5407 (Twint)
      properties:
        pairingToken:
          type: string
          description: A numeric token, which the user has to copy or type into the TWINT app in order to pair it with the merchant for the payment process.
        qrCode:
          type: string
          description: Contains a base64 encoded PNG image. By prepending data:image/png;base64, this value can be used as the source of an HTML inline image on a desktop or tablet (intended to be scanned by a device with the Twint app)
    paymentProduct5404:
      type: object
      description: Contains the third party data for payment product 5404 (WeChat Pay)
      properties:
        qrCodeUrl:
          type: string
          description: Contains a QR code url that can be used to build a QR code (intended to be scanned by a device with the WeChat Pay app)
        appSwitchLink:
          type: string
          description: Contains a application switch url that should open WeChat Pay application in mobile device (intended to be used by a device with the WeChat Pay app)
    paymentProduct3012:
      type: object
      description: Contains the third party data for payment product 3012 (Bancontact)
      properties:
        urlIntent:
          type: string
          description: Contains URL intent that can be used as the link of an "open the app" button on a device
          example: BEPGenApp://DoTx?TransId=my.url.worldline.com/BEP*P-4444444$DNEDXGBFGHVCOVEU6P5YP7TM
        qrCode:
          type: string
          description: Contains a value which can be used to build a QR code (intended to be scanned by a device with the Bancontact app)
          example: BEP://my.url.worldline.com/BEP*P-444444$DNEDXGBFGHVCOVEU6P5YP7TM
    paymentErrorResponse:
      type: object
      properties:
        errorId:
          type: string
          description: Unique reference, for debugging purposes, of this error response
        errors:
          type: array
          items:
            $ref: '#/components/schemas/aPIError'
            description: Contains detailed information on one single error.
        paymentResult:
          $ref: '#/components/schemas/createPaymentResponse'
          description: Object that contains details on the created payment in case one has been created.
    completePaymentRequest:
      type: object
      properties:
        cardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/completePaymentCardPaymentMethodSpecificInput'
        order:
          $ref: '#/components/schemas/order'
          description: >-
            Order object containing order related data 
             Please note that this object is required to be able to submit the amount.
      required:
      - order
    completePaymentCardPaymentMethodSpecificInput:
      type: object
      properties:
        card:
          $ref: '#/components/schemas/cardWithoutCvv'
    cardWithoutCvv:
      type: object
      properties:
        cardNumber:
          $ref: '#/components/schemas/cardNumberObfuscated'
          description: The obfuscated card number
        expiryDate:
          $ref: '#/components/schemas/expiryDate'
          description: >-
            Expiry date of the card

            Format: MMYY
        cardholderName:
          $ref: '#/components/schemas/cardholderName'
          description: The card holder's name on the card.
    order:
      type: object
      description: >-
        Order object containing order related data 
         Please note that this object is required to be able to submit the amount.
      properties:
        additionalInput:
          $ref: '#/components/schemas/additionalOrderInput'
          description: Object containing additional input on the order
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        customer:
          $ref: '#/components/schemas/customer'
          description: Object containing the details of the customer
        references:
          $ref: '#/components/schemas/orderReferences'
          description: Object that holds all reference properties that are linked to this transaction
        shipping:
          $ref: '#/components/schemas/shipping'
          description: Object containing information regarding shipping / delivery
        shoppingCart:
          $ref: '#/components/schemas/shoppingCart'
          description: Shopping cart data, including items and specific amounts.
        surchargeSpecificInput:
          $ref: '#/components/schemas/surchargeSpecificInput'
          description: Object containing specific input required to apply surcharging to an order.
        discount:
          $ref: '#/components/schemas/discount'
          description: Object to apply a discount to the total basket by adding a discount line.
      required:
      - amountOfMoney
    additionalOrderInput:
      type: object
      description: Object containing additional input on the order
      properties:
        airlineData:
          $ref: '#/components/schemas/airlineData'
          description: Object that holds airline specific data
        loanRecipient:
          $ref: '#/components/schemas/loanRecipient'
          description: Object containing specific data regarding the recipient of a loan in the UK
        lodgingData:
          $ref: '#/components/schemas/lodgingData'
          description: Object that holds lodging specific data
        typeInformation:
          $ref: '#/components/schemas/orderTypeInformation'
          description: Object that holds the purchase and usage type indicators
    airlineData:
      type: object
      description: Object that holds airline specific data
      properties:
        agentNumericCode:
          type: string
          description: >-
            Numeric code identifying the agent

            This field is used by the following payment products: 840
          maxLength: 8
        code:
          type: string
          description: >-
            Airline numeric code

            This field is used by the following payment products: 840
          maxLength: 3
        flightDate:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Date of the Flight

            Format: YYYYMMDD
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
          deprecated: true
          x-deprecated-by: none
        flightIndicator:
          type: string
          description: Indicator representing the type of flight on the itinerary.
          enum:
          - one-way
          - round-trip
          - multi-city
        flightLegs:
          type: array
          description: Object that holds the data on the individual legs of the flight ticket
          items:
            $ref: '#/components/schemas/airlineFlightLeg'
            description: Object that holds the data on the individual legs of the ticket
          minItems: 0
          uniqueItems: false
        invoiceNumber:
          type: string
          description: >-
            Airline tracing number

            This field is used by the following payment products: cards
          maxLength: 17
        isETicket:
          type: boolean
          description: >-
            Deprecated: This field is not used by any payment product
             * true = The ticket is an E-Ticket
             * false = the ticket is not an E-Ticket'
          deprecated: true
          x-deprecated-by: none
        isRestrictedTicket:
          type: boolean
          description: >-
            Indicates if the ticket is refundable or not.
             * true - Restricted, the ticket is non-refundable
             * false - No restrictions, the ticket is (partially) refundable
            This field is used by the following payment products: 840
        isThirdParty:
          type: boolean
          description: >-
            Deprecated: This field is not used by any payment product
             * true - The payer is the ticket holder
             * false - The payer is not the ticket holder
          deprecated: true
          x-deprecated-by: none
        issueDate:
          type: string
          description: >-
            This is the date of issue recorded in the airline system In a case of multiple issuances of the same ticket to a cardholder, you should use the last ticket date.

            Format: YYYYMMDD

            This field is used by the following payment products: cards, 840
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        merchantCustomerId:
          type: string
          description: >-
            Your ID of the customer in the context of the airline data

            This field is used by the following payment products: 840
          maxLength: 16
        name:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Name of the airline
          maxLength: 20
          deprecated: true
          x-deprecated-by: none
        passengerName:
          type: string
          description: >-
            Deprecated: Use passengers instead

            Name of passenger
          maxLength: 100
          deprecated: true
          x-deprecated-by: none
        passengers:
          type: array
          description: >-
            Object that holds the data on the individual passengers

            This field is used by the following payment products: cards, 840
          items:
            $ref: '#/components/schemas/airlinePassenger'
          minItems: 0
          uniqueItems: false
        placeOfIssue:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Place of issue

            For sales in the US the last two characters (pos 14-15) must be the US state code.
          maxLength: 15
          deprecated: true
          x-deprecated-by: none
        pnr:
          type: string
          description: '***Deprecated***. Use passengers instead.'
          maxLength: 127
          deprecated: true
          x-deprecated-by: passengers
        pointOfSale:
          type: string
          description: >-
            IATA point of sale name

            This field is used by the following payment products: 840
          maxLength: 25
        posCityCode:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            City code of the point of sale
          maxLength: 10
          deprecated: true
          x-deprecated-by: none
        ticketCurrency:
          type: string
          description: Three-letter ISO currency code representing the currency in which ticket purchase amount is expressed.
          example: EUR
          maxLength: 3
        ticketDeliveryMethod:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Delivery method of the ticket
          enum:
          - e-ticket
          - city-ticket-office
          - airport-ticket-office
          - ticket-by-mail
          - ticket-on-departure
          x-enum-to-string: false
          deprecated: true
          x-deprecated-by: none
        ticketNumber:
          type: string
          description: >-
            The ticket or document number contains:
             * Airline code: 3-digit airline code number
             * Form code: A maximum of 3 digits indicating the type of document, the source of issue and the number of coupons it contains
             * Serial number: A maximum of 8 digits allocated on a sequential basis, provided that the total number of digits allocated to the form code and serial number shall not exceed ten
             * TICKETNUMBER can be replaced with PNR if the ticket number is unavailable
            This field is used by the following payment products: cards, 840
          maxLength: 16
        totalFare:
          type: integer
          format: int32
          description: >-
            Total fare for all legs on the ticket, excluding taxes and fees. If multiple tickets are purchased, this is the total fare for all tickets

            This field is used by the following payment products: 840
          minimum: 0
        totalFee:
          type: integer
          format: int32
          description: >-
            Total fee for all legs on the ticket. If multiple tickets are purchased, this is the total fee for all tickets

            This field is used by the following payment products: 840
          minimum: 0
        totalTaxes:
          type: integer
          format: int32
          description: >-
            Total taxes for all legs on the ticket. If multiple tickets are purchased, this is the total taxes for all tickets

            This field is used by the following payment products: 840
          minimum: 0
        travelAgencyName:
          type: string
          description: >-
            Name of the travel agency issuing the ticket. For direct airline integration, leave this property blank

            This field is used by the following payment products: 840
          maxLength: 25
    airlineFlightLeg:
      type: object
      description: Object that holds the data on the individual legs of the ticket
      properties:
        airlineClass:
          type: string
          description: >-
            Reservation Booking Designator

            This field is used by the following payment products: cards
          maxLength: 15
        arrivalAirport:
          type: string
          description: >-
            Arrival airport/city code

            This field is used by the following payment products: 840
          maxLength: 5
        arrivalTime:
          type: string
          description: >-
            The arrival time in the local time zone

            Format: HH:MM

            This field is used by the following payment products: 840
          maxLength: 5
          pattern: ^(\d{2}:\d{2})?$
        carrierCode:
          type: string
          description: >-
            IATA carrier code

            This field is used by the following payment products: cards, 840
          maxLength: 4
        conjunctionTicket:
          type: string
          description: >-
            Identifying number of a ticket issued to a passenger in conjunction with this ticket and that constitutes a single contract of carriage

            This field is used by the following payment products: 840
          maxLength: 14
        couponNumber:
          type: string
          description: >-
            The coupon number associated with this leg of the trip. A ticket can contain several legs of travel, and each leg of travel requires a separate coupon

            This field is used by the following payment products: 840
          maxLength: 1
        date:
          type: string
          description: >-
            Date of the leg

            Format: YYYYMMDD

            This field is used by the following payment products: cards, 840
          pattern: ^((19|20|21)\d{6})?$
        departureTime:
          type: string
          description: >-
            The departure time in the local time at the departure airport

            Format: HH:MM

            This field is used by the following payment products: 840
          pattern: ^(\d{2}:\d{2})?$
        endorsementOrRestriction:
          type: string
          description: >-
            An endorsement can be an agency-added notation or a mandatory government required notation, such as value-added tax. A restriction is a limitation based on the type of fare, such as a ticket with a 3-day minimum stay

            This field is used by the following payment products: 840
          maxLength: 20
        exchangeTicket:
          type: string
          description: >-
            New ticket number that is issued when a ticket is exchanged

            This field is used by the following payment products: 840
          maxLength: 15
        fare:
          type: string
          description: >-
            Deprecated: Use legFare instead.

            Fare of this leg
          maxLength: 12
          deprecated: true
          x-deprecated-by: legFare
        legFare:
          $ref: '#/components/schemas/legFare'
          description: >-
            Fee for this leg of the trip

            This field is used by the following payment products: 840
        fareBasis:
          type: string
          description: >-
            Fare Basis/Ticket Designator

            This field is used by the following payment products: 840
          maxLength: 15
        fee:
          $ref: '#/components/schemas/legFare'
          description: >-
            Fee for this leg of the trip

            This field is used by the following payment products: 840
        flightNumber:
          type: string
          description: >-
            The flight number assigned by the airline carrier with no leading spaces

            Should be a numeric string

            This field is used by the following payment products: cards, 840
          maxLength: 4
        number:
          type: integer
          format: int32
          description: >-
            Deprecated: This field is not used by any payment product

            Sequence number of the flight leg
          maximum: 99999
          minimum: 0
          deprecated: true
          x-deprecated-by: none
        originAirport:
          type: string
          description: >-
            Origin airport/city code

            This field is used by the following payment products: cards, 840
          maxLength: 5
        passengerClass:
          type: string
          description: >-
            PassengerClass if this leg

            This field is used by the following payment products: 840
          maxLength: 2
        stopoverCode:
          type: string
          description: >-
            Possible values are:
             * permitted = Stopover permitted
             * non-permitted = Stopover not permitted
            This field is used by the following payment products: cards, 840
          enum:
          - permitted
          - non-permitted
        taxes:
          type: integer
          format: int32
          description: >-
            Taxes for this leg of the trip

            This field is used by the following payment products: 840
          minimum: 0
    airlinePassenger:
      type: object
      properties:
        airlineLoyaltyStatus:
          type: string
          description: Airline loyalty program level for the passenger on the itinerary.
          enum:
          - none
          - enrolled
          - other-status (includes all tiers above enrolled status)
        firstName:
          type: string
          description: >-
            First name of the passenger

            This field is used by the following payment products: cards, 840
          maxLength: 100
        surname:
          type: string
          description: >-
            Surname of the passenger

            This field is used by the following payment products: cards, 840
          maxLength: 100
        surnamePrefix:
          type: string
          description: >-
            Surname prefix or middle name of the passenger

            This field is used by the following payment products: 840
          maxLength: 100
        title:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Title of the passenger (this property is used for fraud screening on the payment platform)
          maxLength: 50
          deprecated: true
          x-deprecated-by: none
        passengerType:
          type: string
          description: 'Type of passenger on the itinerary. '
          enum:
          - adult
          - child
          - infant
    loanRecipient:
      type: object
      description: Object containing specific data regarding the recipient of a loan in the UK
      properties:
        accountNumber:
          type: string
          description: Should be filled with the last 10 digits of the bank account number of the recipient of the loan.
          maxLength: 10
        dateOfBirth:
          $ref: '#/components/schemas/dateOfBirth'
          description: >-
            The date of birth of the customer of the recipient of the loan.

            Format YYYYMMDD
        partialPan:
          type: string
          description: Should be filled with the first 6 and last 4 digits of the PAN number of the recipient of the loan.
          maxLength: 10
        surname:
          type: string
          description: Surname of the recipient of the loan.
          maxLength: 12
        zip:
          type: string
          description: Zip code of the recipient of the loan
          maxLength: 10
    lodgingData:
      type: object
      description: Object that holds lodging specific data
      properties:
        checkInDate:
          type: string
          description: >-
            The date the guest checks into (or plans to check in to) the facility.

            Format YYYYMMDD
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
    orderTypeInformation:
      type: object
      description: Object that holds the purchase and usage type indicators
      properties:
        purchaseType:
          type: string
          description: >-
            Possible values are:

            * physical (tangible goods shipped to the customers)

            * digital (digital services like ebooks, streaming...)
        transactionType:
          type: string
          description: >-
            Identifies the type of transaction being authenticated. Possible values are:

            * purchase = The purpose of the transaction is to purchase goods or services (Default)

            * check-acceptance = The purpose of the transaction is to accept a 'check'/'cheque'

            * account-funding = The purpose of the transaction is to fund an account

            * quasi-cash = The purpose of the transaction is to buy a quasi cash type product that is representative of actual cash such as money orders, traveler's checks, foreign currency, lottery tickets or casino gaming chips

            * prepaid-activation-or-load = The purpose of the transaction is to activate or load a prepaid card
    customer:
      type: object
      description: Object containing the details of the customer
      properties:
        companyInformation:
          $ref: '#/components/schemas/companyInformation'
          description: Object containing company information
        merchantCustomerId:
          type: string
          description: Your identifier for the customer. It is used in the fraud-screening process for payments on the payment platform.
          maxLength: 50
        account:
          $ref: '#/components/schemas/customerAccount'
          description: Object containing data related to the account the customer has with you
        accountType:
          type: string
          description: >-
            Type of the customer account that is used to place this order. Can have one of the following values:
             * none - The account that was used to place the order with is a guest account or no account was used at all
             * created - The customer account was created during this transaction
             * existing - The customer account was an already existing account prior to this transaction
        billingAddress:
          $ref: '#/components/schemas/address'
          description: Object containing billing address details.
        contactDetails:
          $ref: '#/components/schemas/contactDetails'
          description: Object containing contact details like email address and phone number
        device:
          $ref: '#/components/schemas/customerDevice'
          description: Object containing information on the device and browser of the customer
        fiscalNumber:
          type: string
          description: >-
            Fiscal registration number of the customer or the tax registration number of the company for a business customer. Please find below specifics per country:
             * Brazil - Consumer (CPF) with a length of 11 digits
             * Brazil - Company (CNPJ) with a length of 14 digits
             * Denmark - Consumer (CPR-nummer or personnummer) with a length of 10 digits
             * Finland - Consumer (Finnish: henkilÃ¶tunnus (abbreviated as HETU), Swedish: personbeteckning) with a length of 11 characters
             * Norway - Consumer (fÃ¸dselsnummer) with a length of 11 digits
             * Sweden - Consumer (personnummer) with a length of 10 or 12 digits
          maxLength: 14
        locale:
          type: string
          description: The locale that the customer should be addressed in (for 3rd parties). Note that some 3rd party providers only support the languageCode part of the locale, in those cases we will only use part of the locale provided.
          maxLength: 6
        personalInformation:
          $ref: '#/components/schemas/personalInformation'
          description: Object containing personal information like name, date of birth and gender.
    customerOutput:
      type: object
      description: Object containing the details of the customer
      properties:
        device:
          $ref: '#/components/schemas/customerDeviceOutput'
          description: Object containing information on the device and browser of the customer
    companyInformation:
      type: object
      description: Object containing company information
      properties:
        name:
          type: string
          description: Name of company, as a customer. For Klarna payment method, company name should be provided to trigger a B2B session. If nothing is provided, a B2C session will be the default.
          example: Customer Company Name
          x-trim-at: 50
    customerAccount:
      type: object
      description: Object containing data related to the account the customer has with you
      properties:
        authentication:
          $ref: '#/components/schemas/customerAccountAuthentication'
          description: Object containing data on the authentication used by the customer to access their account
        changeDate:
          type: string
          description: The last date (YYYYMMDD) on which the customer made changes to their account with you. These are changes to billing & shipping address details, new payment account (tokens), or new users(s) added.
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        changedDuringCheckout:
          type: boolean
          description: >-
            * true = the customer made changes to their account during this checkout

            * false = the customer did nnot change anything to their account during this checkout/n

             The changes ment here are changes to billing & shipping address details, new payment account (tokens), or new users(s) added.
        createDate:
          type: string
          description: The date (YYYYMMDD) on which the customer created their account with you
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        hadSuspiciousActivity:
          type: boolean
          description: >-
            Specifies if you have experienced suspicious activity on the account of the customer


            true = you have experienced suspicious activity (including previous fraud) on the customer account used for this transaction


            false = you have experienced no suspicious activity (including previous fraud) on the customer account used for this transaction
        passwordChangeDate:
          type: string
          description: The last date (YYYYMMDD) on which the customer changed their password for the account used in this transaction
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        passwordChangedDuringCheckout:
          type: boolean
          description: >-
            Indicates if the password of an account is changed during this checkout


            true = the customer made changes to their password of the account used during this checkout


            false = the customer did not change anything to their password of the account used during this checkout
        paymentAccountOnFile:
          $ref: '#/components/schemas/paymentAccountOnFile'
          description: Object containing information on the payment account data on file (tokens)
        paymentActivity:
          $ref: '#/components/schemas/customerPaymentActivity'
          description: Object containing data on the purchase history of the customer with you
    customerAccountAuthentication:
      type: object
      description: Object containing data on the authentication used by the customer to access their account
      properties:
        data:
          type: string
          description: Data about the authentication procedure of the customer to their account with you
          example: kjsn7fh83h4fiiifbsedbf3ins42o5hjo2mdnhiowwer234f4f
          maxLength: 20000
        method:
          $ref: '#/components/schemas/method'
          description: >-
            Authentication used by the customer on your website

            Possible values are
             * guest = no login occurred, customer is logged in as guest
             * merchant-credentials = the customer logged in using credentials that are specific to you
             * federated-id = the customer logged in using a federated ID
             * issuer-credentials = the customer logged in using credentials from the card issuer (of the card used in this transaction)
             * third-party-authentication = the customer logged in using third-party authentication
             * fido-authentication = the customer logged in using a FIDO authenticator
             * cico-b-connect-token = the customer logged in using Check-in/Check-out b.connect
        utcTimestamp:
          type: string
          description: Timestamp (YYYYMMDDHHmm) of the authentication of the customer to their account with you
          maxLength: 12
          pattern: ^(\d{12})?$
    paymentAccountOnFile:
      type: object
      description: Object containing information on the payment account data on file (tokens)
      properties:
        createDate:
          type: string
          description: >-
            The date (YYYYMMDD) when the payment account on file was first created.


            In case a token is used for the transaction we will use the creation date of the token in our system in case you leave this property empty.
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        numberOfCardOnFileCreationAttemptsLast24Hours:
          type: integer
          format: int32
          description: Number of attempts made to add new card to the customer account in the last 24 hours
          maximum: 999
          minimum: 0
    customerPaymentActivity:
      type: object
      description: Object containing data on the purchase history of the customer with you
      properties:
        numberOfPaymentAttemptsLast24Hours:
          type: integer
          format: int32
          description: Number of payment attempts (so including unsuccessful ones) made by this customer with you in the last 24 hours
          maximum: 999
          minimum: 0
        numberOfPaymentAttemptsLastYear:
          type: integer
          format: int32
          description: Number of payment attempts (so including unsuccessful ones) made by this customer with you in the last 12 months
          maximum: 999
          minimum: 0
        numberOfPurchasesLast6Months:
          type: integer
          format: int32
          description: Number of successful purchases made by this customer with you in the last 6 months
          maximum: 9999
          minimum: 0
    contactDetails:
      type: object
      description: Object containing contact details like email address and phone number
      properties:
        emailAddress:
          type: string
          description: Email address of the customer
          x-trim-at: 70
        faxNumber:
          type: string
          description: International version of the fax number of the customer including the leading + (i.e. +16127779311)
          x-trim-at: 20
        mobilePhoneNumber:
          type: string
          description: International version of the mobile phone number of the customer including the leading + (i.e. +16127779311)
          x-trim-at: 20
        phoneNumber:
          type: string
          description: International version of the phone number of the customer including the leading + (i.e. +16127779311)
          x-trim-at: 20
        workPhoneNumber:
          type: string
          description: International version of the work phone number of the customer including the leading + (i.e. +31235671500)
          x-trim-at: 20
    customerDevice:
      type: object
      description: Object containing information on the device and browser of the customer
      properties:
        acceptHeader:
          type: string
          description: The accept-header of the customer client from the HTTP Headers.
          maxLength: 2048
        browserData:
          $ref: '#/components/schemas/browserData'
          description: Object containing information regarding the browser of the customer
        ipAddress:
          type: string
          description: The IP address of the customer client from the HTTP Headers.
          maxLength: 45
        locale:
          type: string
          description: >-
            Locale of the client device/browser. Returned in the browser from the navigator.language property.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.
          maxLength: 35
        timezoneOffsetUtcMinutes:
          type: string
          description: >-
            Offset in minutes of timezone of the client versus the UTC. Value is returned by the JavaScript getTimezoneOffset() Method.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.
          maxLength: 6
        userAgent:
          type: string
          description: >-
            User-Agent of the client device/browser from the HTTP Headers.


            As a fall-back we will use the userAgent that might be included in the encryptedCustomerInput, but this is captured client side using JavaScript and might be different.
          maxLength: 2048
        deviceFingerprint:
          type: string
          description: The session ID for the device fingerprint must match the one sent in the device fingerprint script.
          maxLength: 1024
    customerDeviceOutput:
      type: object
      description: Object containing information on the device and browser of the customer
      properties:
        ipAddressCountryCode:
          $ref: '#/components/schemas/countryCode'
          description: ISO 3166-1 alpha-2 country code
    browserData:
      type: object
      description: Object containing information regarding the browser of the customer
      properties:
        colorDepth:
          type: integer
          format: int32
          description: >-
            ColorDepth in bits. Value is returned from the screen.colorDepth property.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.


            Note: This data can only be collected if JavaScript is enabled in the browser. This means that 3-D Secure version 2.1 requires the use of JavaScript to enabled. In the upcoming version 2.2 of the specification this is no longer a requirement.
          maximum: 99
        javaEnabled:
          type: boolean
          description: >-
            true =Java is enabled in the browser


            false = Java is not enabled in the browser


            Value is returned from the navigator.javaEnabled property.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.


            Note: This data can only be collected if JavaScript is enabled in the browser. This means that 3-D Secure version 2.1 requires the use of JavaScript to enabled. In the upcoming version 2.2 of the specification this is no longer a requirement.
        javaScriptEnabled:
          type: boolean
          description: >-
            * true = JavaScript is enabled in the browser.

            * false = JavaScript is not enabled in the browser. In this case the following parameters are not mandatory anymore: colorDepth, javaEnabled, screenHeight, screenWidth, timezoneOffsetUtcMinutes.
        screenHeight:
          type: string
          description: >-
            Height of the screen in pixels. Value is returned from the screen.height property.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.


            Note: This data can only be collected if JavaScript is enabled in the browser. This means that 3-D Secure version 2.1 requires the use of JavaScript to enabled. In the upcoming version 2.2 of the specification this is no longer a requirement.
          maxLength: 6
        screenWidth:
          type: string
          description: >-
            Width of the screen in pixels. Value is returned from the screen.width property.


            If you use the latest version of our JavaScript Client SDK, we will collect this data and include it in the encryptedCustomerInput property. We will then automatically populate this data if available.


            Note: This data can only be collected if JavaScript is enabled in the browser. This means that 3-D Secure version 2.1 requires the use of JavaScript to enabled. In the upcoming version 2.2 of the specification this is no longer a requirement.
          maxLength: 6
    personalInformation:
      type: object
      description: Object containing personal information like name, date of birth and gender.
      properties:
        dateOfBirth:
          $ref: '#/components/schemas/dateOfBirth'
          description: >-
            The date of birth of the customer of the recipient of the loan.

            Format YYYYMMDD
        gender:
          type: string
          description: The gender of the customer. All values are possible as long as it does not exceed the maximum length of 50 characters.
          maxLength: 50
        name:
          $ref: '#/components/schemas/personalName'
          description: Object containing the name details of the customer
    personalName:
      type: object
      description: Object containing the name details of the customer
      properties:
        firstName:
          type: string
          description: Given name(s) or first name(s) of the customer
          x-trim-at: 35
        surname:
          type: string
          description: Surname(s) or last name(s) of the customer
          x-trim-at: 35
        title:
          type: string
          description: Title of customer
          x-trim-at: 35
    customerFirstName:
      type: string
      description: Given name(s) or first name(s) of the customer.
      example: Jane
    customerSurname:
      type: string
      description: Surname(s) or last name(s) of the customer.
      example: Doe
    customerHonorific:
      type: string
      description: Object containing the title of the customer (Mr, Miss or Mrs)
      enum:
      - Mr
      - Miss
      - Mrs
    addressPersonal:
      type: object
      description: Object containing address information
      properties:
        additionalInfo:
          $ref: '#/components/schemas/additionalInfo'
          description: Second line of street or additional address information
        city:
          $ref: '#/components/schemas/city'
          description: City
        countryCode:
          $ref: '#/components/schemas/countryCode'
          description: ISO 3166-1 alpha-2 country code
        houseNumber:
          $ref: '#/components/schemas/houseNumber'
          description: House number
        state:
          $ref: '#/components/schemas/state'
          description: ISO 3166-2 country subdivision code
        street:
          $ref: '#/components/schemas/street'
          description: Street name
        zip:
          type: string
          description: Zip code
          x-trim-at: 10
        name:
          $ref: '#/components/schemas/personalName'
          description: Object containing the name details of the customer
        companyName:
          type: string
          description: Company Name
          example: Company Inc.
          maxLength: 50
    lineItem:
      type: object
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        invoiceData:
          $ref: '#/components/schemas/lineItemInvoiceData'
          description: Object containing the line items of the invoice or shopping cart
        orderLineDetails:
          $ref: '#/components/schemas/orderLineDetails'
          description: Object containing additional information that when supplied can have a beneficial effect on the discountrates
        otherDetails:
          $ref: '#/components/schemas/otherDetails'
          description: Other information for specific payment methods.
    lineItemInvoiceData:
      type: object
      description: Object containing the line items of the invoice or shopping cart
      properties:
        description:
          type: string
          description: Shopping cart item description
          x-trim-at: 116
    orderLineDetails:
      type: object
      description: Object containing additional information that when supplied can have a beneficial effect on the discountrates
      properties:
        discountAmount:
          type: integer
          format: int64
          description: Discount on the line item, with the last two digits implied as decimal places
          maximum: 2147483647
          minimum: 0
        productCode:
          type: string
          description: Product or UPC Code
          x-trim-at: 50
        productBrand:
          type: string
          description: The brand of the product.
          example: Apple
          maxLength: 50
        productName:
          type: string
          description: The name of the product.
          example: IPhone XX Pro Max 256Gb
          x-trim-at: 50
        productPrice:
          type: integer
          format: int64
          description: The price of one unit of the product, the value should be zero or greater
          maximum: 2147483647
          minimum: 0
        productType:
          type: string
          description: Code used to classify items that are purchased
          x-trim-at: 50
        quantity:
          type: integer
          format: int64
          description: >-
            Quantity of the units being purchased, should be greater than zero

            Note: Must not be all spaces or all zeros
          maximum: 9999
          minimum: 0
        taxAmount:
          type: integer
          format: int64
          description: Tax on the line item, with the last two digits implied as decimal places
          maximum: 2147483647
          minimum: 0
        unit:
          type: string
          description: 'Indicates the line item unit of measure; for example: each, kit, pair, gallon, month, etc.'
          x-trim-at: 50
    otherDetails:
      type: object
      description: Other information for specific payment methods.
      properties:
        travelData:
          type: string
          description: Information used by the following PaymentProducts [5110,5111,5112,5125,3104,3107,3108,3109].
          x-trim-at: 400
        metaData:
          type: string
          description: Information used by the following PaymentProducts [5300] to provide details on the item such as the color, size, etc. The field is in JSON format, with keys and values expected by the payment method at transaction creation. Please refer to the payment mean documentation.
          example: '{"color":"blue","size":"large","delivery-mode":"express"}'
          maxLength: 20000
    orderReferences:
      type: object
      description: Object that holds all reference properties that are linked to this transaction
      properties:
        descriptor:
          $ref: '#/components/schemas/descriptor'
          description: >-
            Descriptive text that is used towards to customer, either during an online checkout at a third party and/or on the statement of the customer. For card transactions this is usually referred to as a Soft Descriptor. The maximum allowed length varies per card acquirer:
             * AIB - 22 characters
             * American Express - 25 characters
             * Atos Origin BNP - 15 characters
             * Barclays - 25 characters
             * Catella - 22 characters
             * CBA - 20 characters
             * Elavon - 25 characters
             * First Data - 25 characters
             * INICIS (INIPAY) - 22-30 characters
             * JCB - 25 characters
             * Merchant Solutions - 22-25 characters
             * Payvision (EU & HK) - 25 characters
             * SEB Euroline - 22 characters
             * Sub1 Argentina - 15 characters
             * Wells Fargo - 25 characters

            Note that we advise you to use 22 characters as the max length as beyond this our experience is that issuers will start to truncate. We currently also only allow per API call overrides for AIB and Barclays

            For alternative payment products the maximum allowed length varies per payment product:
             * 402 e-Przelewy - 30 characters
             * 404 INICIS - 80 characters
             * 802 Nordea ePayment Finland - 234 characters
             * 809 iDeal - 32 characters
             * 836 SOFORT - 42 characters
             * 840 PayPal - 127 characters
             * 841 WebMoney - 175 characters
             * 849 Yandex - 64 characters
             * 861 Alipay - 256 characters
             * 863 WeChat Pay - 32 characters
             * 880 BOKU - 20 characters
             * 8580 Qiwi - 255 characters
             * 1504 Konbini - 80 characters

            All other payment products do not support a descriptor.
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
        merchantParameters:
          $ref: '#/components/schemas/merchantParameters'
          description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
    shipping:
      type: object
      description: Object containing information regarding shipping / delivery
      properties:
        address:
          $ref: '#/components/schemas/addressPersonal'
          description: Object containing address information
        addressIndicator:
          type: string
          description: >-
            Indicates shipping method chosen for the transaction. Possible values:
             * same-as-billing = the shipping address is the same as the billing address
             * another-verified-address-on-file-with-merchant = the address used for shipping is another verified address of the customer that is on file with you
             * different-than-billing = shipping address is different from the billing address
             * ship-to-store = goods are shipped to a store (shipping address = store address)
             * digital-goods = electronic delivery of digital goods
             * travel-and-event-tickets-not-shipped = travel and/or event tickets that are not shipped
             * other = other means of delivery
        emailAddress:
          type: string
          description: Email address linked to the shipping
          x-trim-at: 70
        firstUsageDate:
          type: string
          description: Date (YYYYMMDD) when the shipping details for this transaction were first used.
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        isFirstUsage:
          type: boolean
          description: >-
            Indicator if this shipping address is used for the first time to ship an order


            true = the shipping details are used for the first time with this transaction


            false = the shipping details have been used for other transactions in the past
        method:
          $ref: '#/components/schemas/shippingMethod'
          description: Object containing information regarding shipping method
        type:
          type: string
          description: >-
            Indicates the merchandise delivery timeframe. Possible values:
             * electronic = For electronic delivery (services or digital goods)
             * same-day = For same day deliveries
             * overnight = For overnight deliveries
             * 2-day-or-more = For two day or more delivery time
        shippingCost:
          type: integer
          format: int64
          description: Cost associated with the shipping of the order.
          maximum: 2147483647
          minimum: 0
        shippingCostTax:
          type: integer
          format: int64
          description: Tax amount of the shipping cost.
          maximum: 2147483647
          minimum: 0
    shippingMethod:
      type: object
      description: Object containing information regarding shipping method
      properties:
        details:
          type: string
          description: Details about the shipping method
          maxLength: 50
        name:
          type: string
          description: Name of the shipping method
          maxLength: 25
        speed:
          type: integer
          format: int16
          description: Number of hours to delivery
          maximum: 32767
          minimum: 0
        type:
          type: string
          description: Shipping method type
          enum:
          - carrier-low-cost
          - carrier-merchant-owned
          - carrier-third-party
          - collect-at-merchant-store
          - collect-at-network-points
          - collect-at-parcel-lockers
          - collect-at-travel-points
          - electronic
          - military
          - merchant-defined-1
          - merchant-defined-2
          - merchant-defined-3
          - merchant-defined-4
          - merchant-defined-5
          - merchant-defined-6
          - merchant-defined-7
          - merchant-defined-8
          - merchant-defined-9
    shoppingCart:
      type: object
      description: Shopping cart data, including items and specific amounts.
      properties:
        amountBreakdown:
          type: array
          description: >-
            Deprecated: Use order.shipping.shippingCost for shipping cost. Other amounts are not used.

            Determines how the total amount is split into amount types
          items:
            $ref: '#/components/schemas/amountBreakdown'
            description: Determines the type of the amount.
          deprecated: true
          x-deprecated-by: order.shipping.shippingCost
        giftCardPurchase:
          $ref: '#/components/schemas/giftCardPurchase'
          description: Object containing information on purchased gift card(s)
        isPreOrder:
          type: boolean
          description: The customer is pre-ordering one or more items
        items:
          type: array
          description: Shopping cart data
          items:
            $ref: '#/components/schemas/lineItem'
        preOrderItemAvailabilityDate:
          type: string
          description: Date (YYYYMMDD) when the preordered item becomes available
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        reOrderIndicator:
          type: boolean
          description: >-
            Indicates whether the cardholder is reordering previously purchased item(s)


            true = the customer is re-ordering at least one of the items again


            false = this is the first time the customer is ordering these items
    amountBreakdown:
      type: object
      description: Determines the type of the amount.
      properties:
        amount:
          type: integer
          format: int64
          description: Amount in cents and always having 2 decimals
        type:
          type: string
          description: >-
            Type of the amount. Each type is only allowed to be provided once. Allowed values:
             * AIRPORT_TAX - The amount of tax paid for the airport, with the last 2 digits implied as decimal places.
             * CONSUMPTION_TAX - The amount of consumption tax paid by the customer, with the last 2 digits implied as decimal places.
             * DISCOUNT - Discount on the entire transaction, with the last 2 digits implied as decimal places.
             * DUTY - Duty on the entire transaction, with the last 2 digits implied as decimal places.
             * SHIPPING - Shipping cost on the entire transaction, with the last 2 digits implied as decimal places.
             * VAT - Total amount of VAT paid on the transaction, with the last 2 digits implied as decimal places.
             * BASE_AMOUNT - Order amount excluding all taxes, discount & shipping costs, with the last 2 digits implied as decimal places. Note: BASE_AMOUNT is only supported by the payment platform.
          enum:
          - AIRPORT_TAX
          - CONSUMPTION_TAX
          - DISCOUNT
          - DUTY
          - SHIPPING
          - VAT
          - BASE_AMOUNT
          x-enum-to-string: false
      required:
      - amount
      - type
    giftCardPurchase:
      type: object
      description: Object containing information on purchased gift card(s)
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        numberOfGiftCards:
          type: integer
          format: int32
          description: Number of gift cards that are purchased through this transaction
          maximum: 99
    createHostedCheckoutResponse:
      type: object
      properties:
        RETURNMAC:
          type: string
          description: When the customer is returned to your site we will append this property and value to the query-string. You should store this data, so you can identify the returning customer.
          example: fecab85c-9b0e-42ee-a9d9-ebb69b0c2eb0
        hostedCheckoutId:
          $ref: '#/components/schemas/hostedCheckoutId'
          description: The ID of the Hosted Checkout Session in which the payment was made.
        invalidTokens:
          $ref: '#/components/schemas/invalidTokens'
          description: Tokens that are submitted in the request are validated. In case any of the tokens can't be used anymore they are returned in this array. You should most likely remove those tokens from your system.
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
        redirectUrl:
          $ref: '#/components/schemas/redirectUrl'
          description: The full hosted checkout URL as generated by our system. Use this URL to redirect your customer to the hosted checkout page.
        partialRedirectUrl:
          $ref: '#/components/schemas/partialRedirectUrl'
          description: The partial URL as generated by our system. You will need to add the protocol and the relevant subdomain to this URL, before redirecting your customer to this URL. A special 'payment' subdomain will always work so you can always add 'https://payment.' at the beginning of this response value to view your hosted pages.
    createHostedCheckoutRequest:
      type: object
      properties:
        cardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificInputBase'
          description: Object containing the specific input details for card payments
        hostedCheckoutSpecificInput:
          $ref: '#/components/schemas/hostedCheckoutSpecificInput'
          description: Object containing hosted checkout specific data
        redirectPaymentMethodSpecificInput:
          $ref: '#/components/schemas/redirectPaymentMethodSpecificInput'
          description: Object containing the specific input details for payments that involve redirects to 3rd parties to complete, like iDeal and PayPal
        mobilePaymentMethodSpecificInput:
          $ref: '#/components/schemas/mobilePaymentMethodHostedCheckoutSpecificInput'
          description: Object containing the specific input details for mobile payments
        sepaDirectDebitPaymentMethodSpecificInput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentMethodSpecificInputBase'
          description: Object containing the specific input details for SEPA direct debit payments
        fraudFields:
          $ref: '#/components/schemas/fraudFields'
          description: Object containing additional data that will be used to assess the risk of fraud
        order:
          $ref: '#/components/schemas/order'
          description: >-
            Order object containing order related data 
             Please note that this object is required to be able to submit the amount.
        feedbacks:
          $ref: '#/components/schemas/feedbacks'
          description: This section will contain feedback Urls to provide feedback on the payment.
      required:
      - order
    createHostedTokenizationRequest:
      type: object
      properties:
        locale:
          $ref: '#/components/schemas/locale'
          description: 'Locale used in the GUI towards the consumer. '
        variant:
          $ref: '#/components/schemas/variant'
          description: It is possible to upload multiple templates of your payment pages using the Merchant Portal. You can force the use of a custom template by specifying it in the variant field. This allows you to test out the effect of certain changes to your payment pages in a controlled manner. Please note that you need to specify the filename of the template or customization.
        tokens:
          $ref: '#/components/schemas/hostedSessionTokens'
          description: String containing comma separated tokens (no spaces) associated with the customer of this hosted session. Valid tokens will be used to present the customer the option to re-use previously used payment details. This means the customer for instance does not have to re-enter their card details again, which a big plus when the customer is using their mobile phone to complete the operation.
        askConsumerConsent:
          type: boolean
          description: >-
            Indicate if the tokenization form should contain a checkbox asking the user to give consent for storing their information for future payments.

            If this parameter is false or missing, you should ask the user yourself and provide their answer when submitting the Tokenizer in your JavaScript code. To pass this choice set the submitTokenization function's parameter storePermanently to false, if you choose not to store the token for subsequent payments, or to true otherwise.
        paymentProductFilters:
          $ref: '#/components/schemas/paymentProductFiltersHostedTokenization'
          description: Contains the payment product ids that will be used for manipulating the payment products available for the payment to the customer.
        creditCardSpecificInput:
          $ref: '#/components/schemas/creditCardSpecificInputHostedTokenization'
    createHostedTokenizationResponse:
      type: object
      properties:
        hostedTokenizationUrl:
          type: string
          description: The URL you can use in your JavaScript when instantiating the Tokenizer.
          example: https://payment.domain.com/hostedtokenization/tokenization/form/e18f459488504991858dcf624a94257f
        partialRedirectUrl:
          type: string
          description: >-
            Deprecated. Use hostedTokenizationUrl instead.


            The partial URL as generated by our system. You will need to add the protocol and the relevant subdomain to this URL, before redirecting your customer to this URL. A special 'payment' subdomain will always work so you can always add 'https://payment.' at the beginning of this response value to view your hosted pages.
          deprecated: true
          x-deprecated-by: hostedTokenizationUrl
        hostedTokenizationId:
          type: string
          description: The ID of the Hosted Tokenization Session
        invalidTokens:
          type: array
          description: >-
            Tokens that are submitted in the request are validated. Tokens that cannot be used in the current session are returned in this array. 

            These tokens might not be valid anymore. The validity of tokens can be verified using the [Get token](#operation/GetTokenApi) endpoint.
          items:
            type: string
          example: "['0a7042d6-bf24-40b6-85c5-dec24b9dd1d1','b7042dff-1fbd-46d2-9ecb-bddaa2c31165']"
        expiredCardTokens:
          type: array
          description: >-
            Tokens referencing expired cards are returned in this array. 

            These tokens can be used in the hosted tokenization session but you must ensure that the expiry date fields are displayed in the form in order to be updated.

            If you are using the option "hideTokenFields", these tokens should not be proposed to the customers.
          items:
            type: string
          example: "['0a7042d6-bf24-40b6-85c5-dec24b9dd1d1','b7042dff-1fbd-46d2-9ecb-bddaa2c31165']"
    cardPaymentMethodSpecificInputBase:
      type: object
      description: Object containing the specific input details for card payments
      properties:
        authorizationMode:
          $ref: '#/components/schemas/authorizationMode'
          description: >-
            Determines the type of the authorization that will be used. Allowed values: 
              * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
              * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
              * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

              Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
        initialSchemeTransactionId:
          $ref: '#/components/schemas/initialSchemeTransactionId'
          description: The unique scheme transactionId of the initial transaction that was performed with SCA. In case this is unknown a scheme transactionId of an earlier transaction part of the same sequence can be used as a fall-back. Strongly advised to be submitted for any MerchantInitiated or recurring transaction (a subsequent one).
        recurring:
          $ref: '#/components/schemas/cardRecurrenceDetails'
          description: Object containing data related to recurring
        token:
          $ref: '#/components/schemas/tokenIdInput'
          description: ID of the token to use to create the payment.
        tokenize:
          $ref: '#/components/schemas/tokenize'
          description: >-
            Indicates if this transaction should be tokenized
             * true - Tokenize the transaction. Note that a payment on the payment platform that results in a status REDIRECTED cannot be tokenized in this way.
             * false - Do not tokenize the transaction, unless it would be tokenized by other means such as auto-tokenization of recurring payments.
        transactionChannel:
          $ref: '#/components/schemas/transactionChannel'
          description: >-
            Indicates the channel via which the payment is created. Allowed values:
              * ECOMMERCE - The transaction is a regular E-Commerce transaction.
              * MOTO - The transaction is a Mail Order/Telephone Order.

              Defaults to ECOMMERCE.
        unscheduledCardOnFileRequestor:
          $ref: '#/components/schemas/unscheduledCardOnFileRequestor'
          description: >-
            Indicates which party initiated the unscheduled recurring transaction. Allowed values:
              * merchantInitiated - Merchant Initiated Transaction.
              * cardholderInitiated - Cardholder Initiated Transaction.
            Note:
              * This property is not allowed if isRecurring is true.
              * When a customer has chosen to use a token on a hosted checkout this property is set to "cardholderInitiated".
        unscheduledCardOnFileSequenceIndicator:
          $ref: '#/components/schemas/unscheduledCardOnFileSequenceIndicator'
          description: >-
            * first = This transaction is the first of a series of unscheduled recurring transactions

            * subsequent = This transaction is a subsequent transaction in a series of unscheduled recurring transactions

            Note: this property is not allowed if isRecurring is true.
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        threeDSecure:
          $ref: '#/components/schemas/threeDSecureBase'
          description: Object containing specific data regarding 3-D Secure
        paymentProduct5100SpecificInput:
          $ref: '#/components/schemas/paymentProduct5100SpecificInput'
          description: Object containing specific input required for Cpay payments.
        paymentProduct130SpecificInput:
          $ref: '#/components/schemas/paymentProduct130SpecificInput'
          description: Object containing specific input required for CB payments
        paymentProduct3012SpecificInput:
          $ref: '#/components/schemas/paymentProduct3012SpecificInput'
          description: Object containing specific input required for bancontact.
        paymentProduct3208SpecificInput:
          $ref: '#/components/schemas/paymentProduct3208SpecificInput'
          description: Object containing specific input required for OneyDuplo Leroy Merlin payments.
        paymentProduct3209SpecificInput:
          $ref: '#/components/schemas/paymentProduct3209SpecificInput'
          description: Object containing specific input required for OneyDuplo Alcampo payments.
        currencyConversionSpecificInput:
          $ref: '#/components/schemas/currencyConversionSpecificInput'
          description: Object containing specific input required for Dynamic Currency Conversion.
        allowDynamicLinking:
          $ref: '#/components/schemas/allowDynamicLinking'
          description: >-
            * true - Default - Allows subsequent payments to use PSD2 dynamic linking from this payment (including Card On File).

            * false - Indicates that the dynamic linking (including Card On File data) will be ignored.
        multiplePaymentInformation:
          $ref: '#/components/schemas/multiplePaymentInformation'
          description: Container announcing forecoming subsequent payments. Holds modalities of these subsequent payments.
    paymentProduct5100SpecificInput:
      type: object
      description: Object containing specific input required for Cpay payments.
      properties:
        brand:
          type: string
          description: Indicate whether to use a specific Cpay brand. Brands are configurable at the payment method level. See BackOffice Cpay configuration for allowed values.
    paymentProduct3012SpecificInput:
      type: object
      description: Object containing specific input required for bancontact.
      properties:
        isDeferredPayment:
          type: boolean
          description: Indicate whether its a deferred payment.
        isWipTransaction:
          type: boolean
          description: Indicate whether its wallet initiated payment.
        forceAuthentication:
          type: boolean
          description: Indicate whether 3D Secure authentication should be forced.
        wipMerchantAuthenticationMethod:
          type: string
          description: >-
            Indicates how the cardholder was authenticated to the Merchant Wallet in the context of the transaction to which the BEPAF is attached

            * 01 = Username/password or PIN login successfully performed by cardholder.

            * 02 = Authentication through Secret/Private Key in Secure Hardware Solution was successfully performed.

            * 04 = Authentication through Secret/Private Key in Secure Software Solution (for example, mobile App) was successfully performed.

            * 08 = Location-based Authentication was successfully performed.

            * 10 = Environmental Authentication in Secure Software Solution (mobile App) was successfully performed.

            * 20 = Behavioral Analysis was successfully performed.

            * 40 = Biometrics Authentication was successfully performed.

            * 80 = Out of band user authentication was successfully performed.
          minLength: 2
          maxLength: 2
    paymentProduct130SpecificInput:
      type: object
      description: Object containing specific input required for CB payments
      properties:
        threeDSecure:
          $ref: '#/components/schemas/paymentProduct130SpecificThreeDSecure'
          description: Object containing specific data regarding 3-D Secure
    paymentProduct130SpecificThreeDSecure:
      type: object
      description: Object containing specific data regarding 3-D Secure
      properties:
        usecase:
          type: string
          description: Indicates the type of payment for which an authentication is requested
          enum:
          - single-amount
          - fixed-amount-term-subscription
          - payment-by-instalments
          - payment-upon-shipment
          - other-recurring-payments
          example: fixed-amount-term-subscription
          x-enum-to-string: false
        numberOfItems:
          type: integer
          format: int32
          description: Number of purchased items or services. 99 if more than 99 items
          maximum: 99
          minimum: 0
        acquirerExemption:
          type: boolean
          description: Indicates the Acquirer TRA exemption
          example: true
        merchantScore:
          type: string
          description: Score calculated by the 3DS Requestor and provided to CB Scoring service only.
          example: 'Method 023 : A+'
          maxLength: 20
    cardRecurrenceDetails:
      type: object
      description: Object containing data related to recurring
      properties:
        recurringPaymentSequenceIndicator:
          type: string
          description: >-
            * first = This transaction is the first of a series of recurring transactions

            * recurring = This transaction is a subsequent transaction in a series of recurring transactions


            Note: For any first of a recurring the system will automatically create a token as you will need to use a token for any subsequent recurring transactions. In case a token already exists this is indicated in the response with a value of False for the isNewToken property in the response.
    threeDSecureBase:
      type: object
      description: Object containing specific data regarding 3-D Secure
      properties:
        challengeCanvasSize:
          $ref: '#/components/schemas/challengeCanvasSize'
          description: >-
            Dimensions of the challenge window that potentially will be displayed to the customer. The challenge content is formatted to appropriately render in this window to provide the best possible user experience. Preconfigured sizes are width x height in pixels of the window displayed in the customer browser window. Possible values are
               * 250x400 (default)
               * 390x400
               * 500x600
               * 600x400
               * full-screen
        challengeIndicator:
          $ref: '#/components/schemas/challengeIndicator'
          description: >-
            Allows you to indicate if you want the customer to be challenged for extra security on this transaction. Possible values:
             * no-preference - You have no preference whether or not to challenge the customer (default)
             * no-challenge-requested - you prefer the cardholder not to be challenged
             * challenge-requested - you prefer the customer to be challenged
             * challenge-required - you require the customer to be challenged
             * no-challenge-requested-risk-analysis-performed â€“ letting the issuer know that you have already assessed the transaction with fraud prevention tool 
             * no-challenge-requested-data-share-only â€“ sharing data only with the DS
             * no-challenge-requested-consumer-authentication-performed â€“ authentication already happened at your side â€“ when login in to your website
             * no-challenge-requested-use-whitelist-exemption â€“ cardholder has whitelisted you at with the issuer
             * challenge-requested-whitelist-prompt-requested â€“ cardholder is trying to whitelist you
             * request-scoring-without-connecting-to-acs â€“ sending information to CB DS for a fraud scoring
        priorThreeDSecureData:
          $ref: '#/components/schemas/threeDSecureData'
          description: Object containing data regarding the customer authentication that occurred prior to the current transaction
        skipAuthentication:
          $ref: '#/components/schemas/skipAuthentication'
          description: >-
            * true = 3D Secure authentication will be skipped for this transaction. This setting should be used when isRecurring is set to true and recurringPaymentSequenceIndicator is set to "recurring"

            * false = 3D Secure authentication will not be skipped for this transaction


            Note: This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction
        exemptionRequest:
          type: string
          description: >-
            In PSD2, the ExemptionRequest field is used by merchants requesting an exemption when not using authentication on a transaction, in order to keep the conversion up.

            * none = No exemption requested

            * transaction-risk-analysis = Fraud analysis has been done already by your own fraud module and transaction scored as low risk

            * low-value = Bellow 30 euros

            * whitelist = The cardholder has whitelisted you with their issuer
          enum:
          - none
          - transaction-risk-analysis
          - low-value
          - whitelist
          x-enum-to-string: false
        merchantFraudRate:
          $ref: '#/components/schemas/merchantFraudRate'
          description: >-
            Merchant fraud rate in the EEA (all EEA card fraud divided by all EEA card volumes) calculated as per PSD2 RTS. Mastercard will not calculate or validate the merchant fraud score

            Values accepted :

            * 1 - represents fraud rate less than or equal to 1 basis point [bp], which is 0.01%

            * 2 - represents fraud rate between 1 bp + - and 6 bps

            * 3 - represents fraud rate between 6 bps + - and 13 bps

            * 4 - represents fraud rate between 13 bps + - and 25 bps

            * 5 - represents fraud rate greater than 25 bps
        secureCorporatePayment:
          $ref: '#/components/schemas/secureCorporatePayment'
          description: >-
            Indicates dedicated payment processes and procedures were used, potential secure corporate payment exemption applies Logically this field should only be set to yes if the 

            acquirer exemption field is blank. A merchant cannot claim both acquirer exemption and  secure payment. However, the DS will not validate 

            the conditions in the extension. DS will pass data as presented.
        skipSoftDecline:
          $ref: '#/components/schemas/skipSoftDecline'
          description: >-
            * true = Soft Decline retry mechanism will be skipped for this transaction. The transaction will result in "Authorization Declined" status. This setting should be used when skipAuthentication is set to true and the merchant does not want to use Soft Decline retry mechanism.

            * false = Soft Decline retry mechanism will not be skipped for this transaction.


            Note: skipSoftDecline defaults to false if empty. This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction.
        authenticationAmount:
          $ref: '#/components/schemas/authenticationAmount'
          description: The amount to be authenticated. This field should be populated if the amount to be authenticated differs from the amount to be authorized (by default they are considered equal). Amount in cents and always having 2 decimals.
    threeDSecureData:
      type: object
      description: Object containing data regarding the customer authentication that occurred prior to the current transaction
      properties:
        acsTransactionId:
          type: string
          description: The ACS Transaction ID for a prior 3-D Secure authenticated transaction (for example, the first recurring transaction that was authenticated with the customer)
          maxLength: 36
        method:
          $ref: '#/components/schemas/priorAuthenticationMethod'
          description: >-
            Method of authentication used for this transaction. Possible values:
             * frictionless = The authentication went without a challenge
             * challenged = Cardholder was challenged
             * avs-verified = The authentication was verified by AVS
             * other = Another issuer method was used to authenticate this transaction
        utcTimestamp:
          type: string
          description: Timestamp in UTC (YYYYMMDDHHmm) of the 3-D Secure authentication of this transaction
          maxLength: 12
          pattern: ^(20\d{10})?$
    fraudFields:
      type: object
      description: Object containing additional data that will be used to assess the risk of fraud
      properties:
        blackListData:
          type: string
          description: Additional black list input
          maxLength: 50
        customerIpAddress:
          type: string
          description: >-
            Deprecated: Use order.customer.device.ipAddress instead.


            The IP Address of the customer that is making the payment
          maxLength: 45
          deprecated: true
          x-deprecated-by: order.customer.device.ipAddress
        productCategories:
          type: array
          description: List of product categories that are being purchased.
          items:
            type: string
    cardPaymentMethodSpecificInputForHostedCheckout:
      type: object
      description: Object containing card payment specific data for hosted checkout
      properties:
        groupCards:
          type: boolean
          description: >-
            * true - Hosted Checkout will allow to show cards grouped as one payment method

            * false - Default - Hosted Checkout will show cards as separate payment methods
        clickToPay:
          type: boolean
          description: >-
            * true - Hosted Checkout will show Click to Pay, with cards grouped as one payment method

            * false - Default - Hosted Checkout will show cards as separate payment methods without Click to Pay
        paymentProductPreferredOrder:
          type: array
          description: This array contains the payment product identifiers representing the brands. For co-badged cards, this displays their available brands in the order defined by this array, when groupCards is activated.
          items:
            $ref: '#/components/schemas/paymentProductId'
            description: Payment product identifier - Please see Products documentation for a full overview of possible values.
    hostedCheckoutSpecificInput:
      type: object
      description: Object containing hosted checkout specific data
      properties:
        isRecurring:
          type: boolean
          description: >-
            * true - Only payment products that support recurring payments will be shown. Any transactions created will also be tagged as being a first of a recurring sequence.

            * false - Only payment products that support one-off payments will be shown.

            The default value for this property is false.
        locale:
          $ref: '#/components/schemas/locale'
          description: 'Locale used in the GUI towards the consumer. '
        paymentProductFilters:
          $ref: '#/components/schemas/paymentProductFiltersHostedCheckout'
          description: Contains the payment product ids and payment product groups that will be used for manipulating the payment products available for the payment to the customer.
        returnUrl:
          $ref: '#/components/schemas/returnUrl'
          description: >-
            The URL that the customer is redirect to after the payment flow has finished. You can add any number of key value pairs in the query string that, for instance help you to identify the customer when they return to your site. Please note that we will also append some additional key value pairs that will also help you with this identification process.

            Note: The provided URL should be absolute and contain the protocol to use, e.g. http:// or https://. For use on mobile devices a custom protocol can be used in the form of protocol://. This protocol must be registered on the device first.

            URLs without a protocol will be rejected.
        showResultPage:
          type: boolean
          description: >-
            * true - Default - Hosted Checkout will show a result page to the customer when applicable.

            * false - Hosted Checkout will redirect the customer back to the provided returnUrl when this is possible.
        tokens:
          $ref: '#/components/schemas/hostedSessionTokens'
          description: String containing comma separated tokens (no spaces) associated with the customer of this hosted session. Valid tokens will be used to present the customer the option to re-use previously used payment details. This means the customer for instance does not have to re-enter their card details again, which a big plus when the customer is using their mobile phone to complete the operation.
        variant:
          $ref: '#/components/schemas/variant'
          description: It is possible to upload multiple templates of your payment pages using the Merchant Portal. You can force the use of a custom template by specifying it in the variant field. This allows you to test out the effect of certain changes to your payment pages in a controlled manner. Please note that you need to specify the filename of the template or customization.
        cardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificInputForHostedCheckout'
          description: Object containing card payment specific data for hosted checkout
        sessionTimeout:
          type: integer
          format: int32
          description: The number of minutes after which the session will expire. By default, the value is set to 180 minutes.
          maximum: 1440
          minimum: 1
        allowedNumberOfPaymentAttempts:
          type: integer
          format: int32
          description: The maximum number of times a customer can try to pay before the payment is definitely declined. The value must be between 1 and 10. By default, the value is set to 10 attempts.
          maximum: 10
          minimum: 1
    paymentProductFiltersHostedCheckout:
      type: object
      description: Contains the payment product ids and payment product groups that will be used for manipulating the payment products available for the payment to the customer.
      properties:
        exclude:
          $ref: '#/components/schemas/paymentProductFilter'
          description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
        restrictTo:
          $ref: '#/components/schemas/paymentProductFilter'
          description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
    paymentProductFilter:
      type: object
      description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
      properties:
        groups:
          type: array
          description: List containing all payment product groups that should either be restricted to in or excluded from the payment context. Currently, there is only one group, called 'cards'.
          items:
            type: string
          minItems: 0
          uniqueItems: true
        products:
          $ref: '#/components/schemas/products'
          description: List containing all payment product ids that should either be restricted to in or excluded from the payment context.
    paymentProductFiltersHostedTokenization:
      type: object
      description: Contains the payment product ids that will be used for manipulating the payment products available for the payment to the customer.
      properties:
        exclude:
          $ref: '#/components/schemas/paymentProductFilterHostedTokenization'
          description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
        restrictTo:
          $ref: '#/components/schemas/paymentProductFilterHostedTokenization'
          description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
    paymentProductFilterHostedTokenization:
      type: object
      description: The payment product ids to be be excluded or restricted to from the payment products available for the payment. Note that you can add exclusions on top of the 'restrictTo' filter.
      properties:
        products:
          $ref: '#/components/schemas/products'
          description: List containing all payment product ids that should either be restricted to in or excluded from the payment context.
    creditCardSpecificInputHostedTokenization:
      type: object
      properties:
        ValidationRules:
          $ref: '#/components/schemas/creditCardValidationRulesHostedTokenization'
        paymentProductPreferredOrder:
          type: array
          description: This array contains the payment product identifiers representing the brands. For co-badged cards, this displays their available brands in the order defined by this array.
          items:
            $ref: '#/components/schemas/paymentProductId'
            description: Payment product identifier - Please see Products documentation for a full overview of possible values.
    creditCardValidationRulesHostedTokenization:
      type: object
      properties:
        cvvMandatoryForNewToken:
          type: boolean
          description: Determines whether the Card Verification Value must be provided for new tokens. This option overrides the payment method configuration for the session.
        cvvMandatoryForExistingToken:
          type: boolean
          description: Determines whether the Card Verification Value must be provided for existing tokens. This option overrides the payment method configuration for the session.
    redirectPaymentMethodSpecificInput:
      type: object
      description: Object containing the specific input details for payments that involve redirects to 3rd parties to complete, like iDeal and PayPal
      properties:
        requiresApproval:
          $ref: '#/components/schemas/requiresApproval'
          description: >-
            * true = the payment requires approval before the funds will be captured using the Approve payment or Capture payment API

            * false = the payment does not require approval, and the funds will be captured automatically
        token:
          $ref: '#/components/schemas/tokenIdInput'
          description: ID of the token to use to create the payment.
        tokenize:
          type: boolean
          description: >-
            Indicates if this transaction should be tokenized
              * true - Tokenize the transaction.
              * false - Do not tokenize the transaction, unless it would be tokenized by other means such as auto-tokenization of recurring payments.
          example: false
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        paymentProduct809SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct809SpecificInput'
          description: Object containing specific input required for iDeal payments (Payment product ID 809)
        paymentProduct840SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct840SpecificInput'
          description: Object containing specific input required for PayPal payments (Payment product ID 840)
        paymentProduct3302SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct3302SpecificInput'
          description: Object containing specific input required for Klarna PayLater payment (Payment product ID 3302)
        paymentProduct3306SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct3306SpecificInput'
          description: Object containing specific input required for Klarna payments (Payment product ID 3306)
        paymentProduct5406SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct5406SpecificInput'
          description: Object containing specific input for EPS payments (Payment product ID 5406)
        paymentProduct5408SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct5408SpecificInput'
          description: Object containing specific input for Account to Account payments (Payment product ID 5408)
        paymentProduct3203SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct3203SpecificInput'
          description: Object containing specific input for PostFinancePay payments (Payment product ID 3203).
        paymentProduct5001SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct5001SpecificInput'
          description: Object containing specific input required for Bizum payments
        paymentProduct5300SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct5300SpecificInput'
          description: Pledg (payment product 5300) specific details
        paymentProduct5410SpecificInput:
          $ref: '#/components/schemas/redirectPaymentProduct5410SpecificInput'
          description: iDealin3 (payment product 5410) specific details
        redirectionData:
          $ref: '#/components/schemas/redirectionData'
          description: Object containing browser specific redirection related data
        paymentOption:
          $ref: '#/components/schemas/paymentOption'
          description: 'The specific payment option for the payment. To be used as a complement of the more generic paymentProductId (oney, banquecasino, cofidis), which allows to define a variation of the selected paymentProductId (ex: facilypay3x, banquecasino4x, cofidis3x-sansfrais, ...). List of modalities included in the payment product page.'
    sepaDirectDebitPaymentMethodSpecificInput:
      type: object
      description: Object containing the specific input details for SEPA direct debit payments
      properties:
        paymentProduct771SpecificInput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentProduct771SpecificInput'
          description: Object containing information specific to SEPA Direct Debit
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
    sepaDirectDebitPaymentMethodSpecificInputBase:
      type: object
      description: Object containing the specific input details for SEPA direct debit payments
      properties:
        paymentProduct771SpecificInput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentProduct771SpecificInputBase'
          description: Object containing information specific to SEPA Direct Debit
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
    sepaDirectDebitPaymentProduct771SpecificInput:
      type: object
      description: Object containing information specific to SEPA Direct Debit
      properties:
        existingUniqueMandateReference:
          $ref: '#/components/schemas/existingUniqueMandateReference'
          description: The unique reference of the existing mandate to use in this payment.
        mandate:
          $ref: '#/components/schemas/createMandateWithReturnUrl'
          description: Object containing information to create a SEPA Direct Debit mandate.
    sepaDirectDebitPaymentProduct771SpecificInputBase:
      type: object
      description: Object containing information specific to SEPA Direct Debit
      properties:
        existingUniqueMandateReference:
          $ref: '#/components/schemas/existingUniqueMandateReference'
          description: The unique reference of the existing mandate to use in this payment.
        mandate:
          $ref: '#/components/schemas/createMandateRequest'
          description: Object containing information to create a SEPA Direct Debit mandate.
    product5001SubsequentType:
      type: string
      description: >-
        Determines the type of the subsequent that will be used. Allowed values: 
          * recurring - Transactions processed at fixed, regular intervals not to exceed one year between Transactions, representing an agreement between a consumer and a merchant to purchase goods or services provided over a period of time. Note that a recurring MIT transaction is initiated by the merchant (payee) not the customer (payer) and so is out of scope of PSD2. Recurring transactions that are in scope of PSD2 (and therefore may benefit from the recurring transaction exemption) are those that are customer (payer) initiates, e.g. standing orders set up from a bank account. 
          * installment - Installment payments describe a single purchase of goods or services billed to a consumer in multiple transactions over a period of time agreed by the consumer and merchant. 
          * other - other cases
      enum:
      - installment
      - recurring
      - other
      x-enum-to-string: false
    redirectPaymentProduct5001SpecificInput:
      type: object
      description: Object containing specific input required for Bizum payments
      properties:
        subsequentType:
          $ref: '#/components/schemas/product5001SubsequentType'
          description: >-
            Determines the type of the subsequent that will be used. Allowed values: 
              * recurring - Transactions processed at fixed, regular intervals not to exceed one year between Transactions, representing an agreement between a consumer and a merchant to purchase goods or services provided over a period of time. Note that a recurring MIT transaction is initiated by the merchant (payee) not the customer (payer) and so is out of scope of PSD2. Recurring transactions that are in scope of PSD2 (and therefore may benefit from the recurring transaction exemption) are those that are customer (payer) initiates, e.g. standing orders set up from a bank account. 
              * installment - Installment payments describe a single purchase of goods or services billed to a consumer in multiple transactions over a period of time agreed by the consumer and merchant. 
              * other - other cases
    redirectPaymentProduct5300SpecificInput:
      type: object
      description: Pledg (payment product 5300) specific details
      properties:
        secondInstallmentPaymentDate:
          type: string
          format: date
          description: The date of the second installment (YYYYMMDD)
          example: 20261231
          pattern: ^20\d{6}$
        birthZipCode:
          type: string
          description: The zip code of the address where the customer was born
          example: 59800
          maxLength: 10
        birthCity:
          type: string
          description: The city of the address where the customer was born
          example: Lille
          maxLength: 40
        birthCountry:
          type: string
          description: ISO 3166-1 alpha-2 country code of the address where the customer was born
          example: FR
          maxLength: 2
        loyaltyCardNumber:
          type: string
          description: The number of customer's loyalty card or program
          example: a62400FR-123456 02
          maxLength: 30
        sessionDuration:
          type: integer
          format: int32
          description: The duration of the session in seconds
          example: 1800
          maximum: 36000
        channel:
          type: string
          description: The channel used by the customer
          enum:
          - desktop
          - mobile
          - call_center
          - point_of_sale
          example: desktop
    redirectPaymentProduct5410SpecificInput:
      type: object
      description: iDealin3 (payment product 5410) specific details
      properties:
        secondInstallmentPaymentDate:
          type: string
          format: date
          description: The date of the second installment (YYYYMMDD)
          example: 20261231
          pattern: ^20\d{6}$
    existingUniqueMandateReference:
      type: string
      description: The unique reference of the existing mandate to use in this payment.
      example: exampleMandateReference
    createMandateWithReturnUrl:
      type: object
      description: Object containing information to create a SEPA Direct Debit mandate.
      properties:
        alias:
          $ref: '#/components/schemas/mandateAlias'
          description: An alias for the mandate. This can be used to visually represent the mandate. Do not include any unmasked sensitive data in the alias. If this field is not provided the masked IBAN of the customer is used.
        customer:
          $ref: '#/components/schemas/mandateCustomer'
          description: >-
            Customer object containing customer specific inputs.

            Required for Create mandate and Create payment calls.
        customerReference:
          $ref: '#/components/schemas/mandateCustomerReference'
          description: The unique identifier of a customer
        language:
          $ref: '#/components/schemas/mandateLanguage'
          description: >-
            The language code of the customer. ISO 639-1, possible values are:

            * de

            * en

            * es

            * fr

            * it

            * nl

            * si

            * sk

            * sv
        recurrenceType:
          $ref: '#/components/schemas/mandateRecurrenceType'
          description: >-
            Specifies whether the mandate is for one-off or recurring payments. Possible values are:

            * UNIQUE

            * RECURRING
        returnUrl:
          $ref: '#/components/schemas/mandateReturnUrl'
          description: Return URL to use if the mandate signing requires redirection. Required for S2S Create Payment if and only if the signatureType is "SMS".
        signatureType:
          $ref: '#/components/schemas/mandateSignatureType'
          description: >-
            Specifies whether the mandate is tick box, unsigned or signed by SMS. Possible values are:

            * UNSIGNED

            * SMS

            * TICK_BOX - This option is only available for Equens Worldline
        uniqueMandateReference:
          $ref: '#/components/schemas/uniqueMandateReference'
          description: The unique identifier of the mandate
      required:
      - customerReference
      - recurrenceType
      - signatureType
    createMandateRequest:
      type: object
      description: Object containing information to create a SEPA Direct Debit mandate.
      properties:
        alias:
          $ref: '#/components/schemas/mandateAlias'
          description: An alias for the mandate. This can be used to visually represent the mandate. Do not include any unmasked sensitive data in the alias. If this field is not provided the masked IBAN of the customer is used.
        customer:
          $ref: '#/components/schemas/mandateCustomer'
          description: >-
            Customer object containing customer specific inputs.

            Required for Create mandate and Create payment calls.
        customerReference:
          $ref: '#/components/schemas/mandateCustomerReference'
          description: The unique identifier of a customer
        language:
          $ref: '#/components/schemas/mandateLanguage'
          description: >-
            The language code of the customer. ISO 639-1, possible values are:

            * de

            * en

            * es

            * fr

            * it

            * nl

            * si

            * sk

            * sv
        recurrenceType:
          $ref: '#/components/schemas/mandateRecurrenceType'
          description: >-
            Specifies whether the mandate is for one-off or recurring payments. Possible values are:

            * UNIQUE

            * RECURRING
        returnUrl:
          $ref: '#/components/schemas/mandateReturnUrl'
          description: Return URL to use if the mandate signing requires redirection. Required for S2S Create Payment if and only if the signatureType is "SMS".
        signatureType:
          $ref: '#/components/schemas/mandateSignatureType'
          description: >-
            Specifies whether the mandate is tick box, unsigned or signed by SMS. Possible values are:

            * UNSIGNED

            * SMS

            * TICK_BOX - This option is only available for Equens Worldline
        uniqueMandateReference:
          $ref: '#/components/schemas/uniqueMandateReference'
          description: The unique identifier of the mandate
      required:
      - customerReference
      - recurrenceType
      - signatureType
    mandateAlias:
      type: string
      description: An alias for the mandate. This can be used to visually represent the mandate. Do not include any unmasked sensitive data in the alias. If this field is not provided the masked IBAN of the customer is used.
      example: mandateAlias
    mandateCustomerReference:
      type: string
      description: The unique identifier of a customer
      example: uniqueCustomerReference123456789012
      maxLength: 35
    mandateLanguage:
      type: string
      description: >-
        The language code of the customer. ISO 639-1, possible values are:

        * de

        * en

        * es

        * fr

        * it

        * nl

        * si

        * sk

        * sv
      example: en
      maxLength: 2
    mandateRecurrenceType:
      type: string
      description: >-
        Specifies whether the mandate is for one-off or recurring payments. Possible values are:

        * UNIQUE

        * RECURRING
      enum:
      - UNIQUE
      - RECURRING
    mandateReturnUrl:
      type: string
      description: Return URL to use if the mandate signing requires redirection. Required for S2S Create Payment if and only if the signatureType is "SMS".
      example: https://example-mandate-signing-url.com
    mandateSignatureType:
      type: string
      description: >-
        Specifies whether the mandate is tick box, unsigned or signed by SMS. Possible values are:

        * UNSIGNED

        * SMS

        * TICK_BOX - This option is only available for Equens Worldline
      enum:
      - UNSIGNED
      - SMS
      - TICK_BOX
    uniqueMandateReference:
      type: string
      description: The unique identifier of the mandate
      example: exampleMandateReference
    mandatePdf:
      type: string
      description: The mandate PDF in base64 encoded string
      example: VGhlIG1hbmRhdGUgUERGIGluIGJhc2U2NCBlbmNvZGVkIHN0cmluZw==
    mandateCustomer:
      type: object
      description: >-
        Customer object containing customer specific inputs.

        Required for Create mandate and Create payment calls.
      properties:
        bankAccountIban:
          $ref: '#/components/schemas/bankAccountIban'
          description: Object containing IBAN information
        companyName:
          type: string
          description: Name of company, as a customer
          example: Customer Company Name
          maxLength: 40
        contactDetails:
          $ref: '#/components/schemas/mandateContactDetails'
          description: Object containing email address
        mandateAddress:
          $ref: '#/components/schemas/mandateAddress'
          description: >-
            Object containing consumer address details.

            Required for Create mandate and Create payment calls.

            Required for Create hostedCheckout calls where the IBAN is also provided.
        personalInformation:
          $ref: '#/components/schemas/mandatePersonalInformation'
          description: >-
            Object containing personal information of the customer.

            Required for Create mandate and Create payment calls.
    mandateCustomerResponse:
      type: object
      description: Customer object containing customer specific outputs.
      properties:
        bankAccountIban:
          $ref: '#/components/schemas/bankAccountIban'
          description: Object containing IBAN information
        companyName:
          type: string
          description: Name of company, as a customer
          example: Customer Company Name
        contactDetails:
          $ref: '#/components/schemas/mandateContactDetails'
          description: Object containing email address
        mandateAddress:
          $ref: '#/components/schemas/mandateAddressResponse'
          description: Object containing consumer address details
        personalInformation:
          $ref: '#/components/schemas/mandatePersonalInformationResponse'
          description: Object containing personal information of the customer
    bankAccountIban:
      type: object
      description: Object containing IBAN information
      properties:
        iban:
          type: string
          description: >-
            The IBAN is the International Bank Account Number. It is an internationally agreed format for the BBAN and includes the ISO country code and two check digits.

            Required for the creation of a Payout

            Required for Create and Update token.

            Required for Create mandate and Create payment with mandate calls.
          example: BE01 0123 4567 8910
          maxLength: 50
      required:
      - iban
    mandateContactDetails:
      type: object
      description: Object containing email address
      properties:
        emailAddress:
          type: string
          description: Email address of the customer
          example: example@customer.com
    mandateAddress:
      type: object
      description: >-
        Object containing consumer address details.

        Required for Create mandate and Create payment calls.

        Required for Create hostedCheckout calls where the IBAN is also provided.
      properties:
        city:
          type: string
          description: >-
            City

            Required for Create mandate and Create payment calls.

            Required for Create hostedCheckout calls where the IBAN is also provided.
          example: Monument Valley
          maxLength: 35
        countryCode:
          type: string
          description: >-
            ISO 3166-1 alpha-2 country code.

            Required for Create mandate and Create payment calls.

            Required for Create hostedCheckout calls where the IBAN is also provided.
          example: US
          maxLength: 2
        houseNumber:
          type: string
          description: House number
          example: 13
          maxLength: 15
        street:
          type: string
          description: >-
            Streetname.

            Required for Create mandate and Create payment calls.

            Required for Create hostedCheckout calls where the IBAN is also provided.
          example: Desertroad
          maxLength: 50
        zip:
          type: string
          description: >-
            Zip code.

            Required for Create mandate and Create payment calls.

            Required for Create hostedCheckout calls where the IBAN is also provided.
          example: 84536
          maxLength: 10
    mandateAddressResponse:
      type: object
      description: Object containing consumer address details
      properties:
        city:
          $ref: '#/components/schemas/city'
          description: City
        countryCode:
          type: string
          description: ISO 3166-1 alpha-2 country code.
          example: US
        houseNumber:
          $ref: '#/components/schemas/houseNumber'
          description: House number
        street:
          type: string
          description: Streetname
          example: Desertroad
        zip:
          type: string
          description: Zip code
          example: 84536
    mandatePersonalInformation:
      type: object
      description: >-
        Object containing personal information of the customer.

        Required for Create mandate and Create payment calls.
      properties:
        name:
          $ref: '#/components/schemas/mandatePersonalName'
          description: >-
            Object containing the name details of the customer.

            Required for Create mandate and Create payment calls.
        title:
          $ref: '#/components/schemas/customerHonorific'
          description: Object containing the title of the customer (Mr, Miss or Mrs)
    mandatePersonalInformationResponse:
      type: object
      description: Object containing personal information of the customer
      properties:
        name:
          $ref: '#/components/schemas/mandatePersonalNameResponse'
          description: Object containing the name details of the customer.
        title:
          $ref: '#/components/schemas/customerHonorific'
          description: Object containing the title of the customer (Mr, Miss or Mrs)
    mandatePersonalName:
      type: object
      description: >-
        Object containing the name details of the customer.

        Required for Create mandate and Create payment calls.
      properties:
        firstName:
          type: string
          description: >-
            Given name(s) or first name(s) of the customer.

            Required for Create mandate and Create payment calls.
          example: Jane
          maxLength: 15
        surname:
          type: string
          description: >-
            Surname(s) or last name(s) of the customer.

            Required for Create mandate and Create payment calls.
          example: Doe
          maxLength: 55
    mandatePersonalNameResponse:
      type: object
      description: Object containing the name details of the customer.
      properties:
        firstName:
          $ref: '#/components/schemas/customerFirstName'
          description: Given name(s) or first name(s) of the customer.
        surname:
          $ref: '#/components/schemas/customerSurname'
          description: Surname(s) or last name(s) of the customer.
    createMandateResponse:
      type: object
      description: Object containing the Create Mandate response
      properties:
        mandate:
          $ref: '#/components/schemas/mandateResponse'
          description: Object containing the created mandate.
        merchantAction:
          $ref: '#/components/schemas/mandateMerchantAction'
          description: Object that contains the action, including the needed data, that you should perform next, showing the redirect to a third party to complete the payment or like showing instructions.
    getMandateResponse:
      type: object
      description: Object containing the Get Mandate response
      properties:
        mandate:
          $ref: '#/components/schemas/mandateResponse'
          description: Object containing the created mandate.
    mandateResponse:
      type: object
      description: Object containing the created mandate.
      properties:
        alias:
          $ref: '#/components/schemas/mandateAlias'
          description: An alias for the mandate. This can be used to visually represent the mandate. Do not include any unmasked sensitive data in the alias. If this field is not provided the masked IBAN of the customer is used.
        customerReference:
          $ref: '#/components/schemas/mandateCustomerReference'
          description: The unique identifier of a customer
        customer:
          $ref: '#/components/schemas/mandateCustomerResponse'
          description: Customer object containing customer specific outputs.
        recurrenceType:
          type: string
          description: >-
            Specifies whether the mandate is for one-off or recurring payments. Possible values are:

            * UNIQUE

            * RECURRING
          enum:
          - UNIQUE
          - RECURRING
        status:
          type: string
          enum:
          - ACTIVE
          - EXPIRED
          - CREATED
          - REVOKED
          - WAITING_FOR_REFERENCE
          - BLOCKED
          - USED
        uniqueMandateReference:
          $ref: '#/components/schemas/uniqueMandateReference'
          description: The unique identifier of the mandate
        mandatePdf:
          $ref: '#/components/schemas/mandatePdf'
          description: The mandate PDF in base64 encoded string
    mandateMerchantAction:
      type: object
      description: Object that contains the action, including the needed data, that you should perform next, showing the redirect to a third party to complete the payment or like showing instructions.
      properties:
        actionType:
          type: string
          description: >-
            Action merchants needs to take in the online mandate process. Possible values are:

            * REDIRECT - The customer needs to be redirected using the details found in redirectData
          enum:
          - REDIRECT
        redirectData:
          $ref: '#/components/schemas/mandateRedirectData'
          description: Object containing all data needed to redirect the customer
    mandateRedirectData:
      type: object
      description: Object containing all data needed to redirect the customer
      properties:
        RETURNMAC:
          type: string
          description: A Message Authentication Code (MAC) is used to authenticate the redirection back to merchant after the payment.
          example: fecab85c-9b0e-42ee-a9d9-ebb69b0c2eb0
        redirectURL:
          type: string
          description: The URL that the customer should be redirected to. Be sure to redirect using the GET method.
    getPrivacyPolicyResponse:
      type: object
      description: Object containing the privacy policy.
      properties:
        htmlContent:
          type: string
          description: HTML content to be displayed to the user.
          example: <h2>Privacy Notice</h2>...
    getHostedCheckoutResponse:
      type: object
      properties:
        createdPaymentOutput:
          $ref: '#/components/schemas/createdPaymentOutput'
          description: This object will return the details of the payment after the payment is cancelled by the customer, rejected or authorized
        status:
          type: string
          description: >-
            This is the status of the hosted checkout. Possible values are:

            * IN_PROGRESS - The checkout is still in progress and has not finished yet

            * PAYMENT_CREATED - A payment has been created

            * CANCELLED_BY_CONSUMER - The HostedCheckout session have been cancelled by the customer
          enum:
          - PAYMENT_CREATED
          - IN_PROGRESS
          - CANCELLED_BY_CONSUMER
    getHostedTokenizationResponse:
      type: object
      properties:
        token:
          $ref: '#/components/schemas/tokenResponse'
        tokenStatus:
          $ref: '#/components/schemas/tokenStatus'
          description: >-
            This is the status of the token in the hosted tokenization session. Possible values are:

            * UNCHANGED - The token has not changed

            * CREATED - The token has been created

            * UPDATED - The token has been updated
    createdPaymentOutput:
      type: object
      description: This object will return the details of the payment after the payment is cancelled by the customer, rejected or authorized
      properties:
        payment:
          $ref: '#/components/schemas/paymentResponse'
          description: Object that holds the payment related properties
        paymentStatusCategory:
          type: string
          enum:
          - SUCCESSFUL
          - REJECTED
          - STATUS_UNKNOWN
    paymentResponse:
      type: object
      description: Object that holds the payment related properties
      properties:
        hostedCheckoutSpecificOutput:
          $ref: '#/components/schemas/hostedCheckoutSpecificOutput'
          description: Hosted Checkout specific information. Populated if the payment was created on the platform through a Hosted Checkout.
        paymentOutput:
          $ref: '#/components/schemas/paymentOutput'
          description: Object containing payment details
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/paymentStatusOutput'
          description: This object has the numeric representation of the current payment status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
        id:
          $ref: '#/components/schemas/paymentId'
          description: Our unique payment transaction identifier
    paymentDetailsResponse:
      type: object
      description: Object that holds the payment details properties
      properties:
        hostedCheckoutSpecificOutput:
          $ref: '#/components/schemas/hostedCheckoutSpecificOutput'
          description: Hosted Checkout specific information. Populated if the payment was created on the platform through a Hosted Checkout.
        paymentOutput:
          $ref: '#/components/schemas/paymentOutput'
          description: Object containing payment details
        Operations:
          type: array
          description: Object that contains the complete list of operations executed on the payment.
          items:
            $ref: '#/components/schemas/operationOutput'
            description: Object containing operation details
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/paymentStatusOutput'
          description: This object has the numeric representation of the current payment status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
        id:
          $ref: '#/components/schemas/paymentId'
          description: Our unique payment transaction identifier
    subsequentPaymentResponse:
      type: object
      description: Object that contains details on the created payment in case one has been created.
      properties:
        payment:
          $ref: '#/components/schemas/paymentResponse'
          description: Object that holds the payment related properties
    createPaymentResponse:
      type: object
      description: Object that contains details on the created payment in case one has been created.
      properties:
        creationOutput:
          $ref: '#/components/schemas/paymentCreationOutput'
          description: Object containing the details of the created payment.
        merchantAction:
          $ref: '#/components/schemas/merchantAction'
          description: Object that contains the action, including the needed data, that you should perform next, like showing instructions, showing the transaction results or redirect to a third party to complete the payment
        payment:
          $ref: '#/components/schemas/paymentResponse'
          description: Object that holds the payment related properties
    subsequentPaymentRequest:
      type: object
      properties:
        subsequentcardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/subsequentCardPaymentMethodSpecificInput'
          description: Object containing the specific input details for subsequent card payments
        subsequentPaymentProduct5001SpecificInput:
          $ref: '#/components/schemas/subsequentPaymentProduct5001SpecificInput'
          description: specific data required for Bizum subsequent payment
        order:
          $ref: '#/components/schemas/order'
          description: >-
            Order object containing order related data 
             Please note that this object is required to be able to submit the amount.
    subsequentPaymentProduct5001SpecificInput:
      type: object
      description: specific data required for Bizum subsequent payment
      properties:
        subsequentType:
          $ref: '#/components/schemas/product5001SubsequentType'
          description: >-
            Determines the type of the subsequent that will be used. Allowed values: 
              * recurring - Transactions processed at fixed, regular intervals not to exceed one year between Transactions, representing an agreement between a consumer and a merchant to purchase goods or services provided over a period of time. Note that a recurring MIT transaction is initiated by the merchant (payee) not the customer (payer) and so is out of scope of PSD2. Recurring transactions that are in scope of PSD2 (and therefore may benefit from the recurring transaction exemption) are those that are customer (payer) initiates, e.g. standing orders set up from a bank account. 
              * installment - Installment payments describe a single purchase of goods or services billed to a consumer in multiple transactions over a period of time agreed by the consumer and merchant. 
              * other - other cases
      required:
      - subsequentType
    createPaymentRequest:
      type: object
      properties:
        cardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificInput'
          description: Object containing the specific input details for card payments
        mobilePaymentMethodSpecificInput:
          $ref: '#/components/schemas/mobilePaymentMethodSpecificInput'
          description: Object containing the specific input details for mobile payments
        redirectPaymentMethodSpecificInput:
          $ref: '#/components/schemas/redirectPaymentMethodSpecificInput'
          description: Object containing the specific input details for payments that involve redirects to 3rd parties to complete, like iDeal and PayPal
        sepaDirectDebitPaymentMethodSpecificInput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentMethodSpecificInput'
          description: Object containing the specific input details for SEPA direct debit payments
        encryptedCustomerInput:
          type: string
          description: >-
            Data that was encrypted client side containing all customer entered data elements like card data.

            Note: Because this data can only be submitted once to our system and contains encrypted card data you should not store it. As the data was captured within the context of a client session you also need to submit it to us before the session has expired.
        fraudFields:
          $ref: '#/components/schemas/fraudFields'
          description: Object containing additional data that will be used to assess the risk of fraud
        order:
          $ref: '#/components/schemas/order'
          description: >-
            Order object containing order related data 
             Please note that this object is required to be able to submit the amount.
        hostedTokenizationId:
          type: string
          description: Use this field after a successful Hosted Tokenization session to create a payment with the tokenized payment method details.
        feedbacks:
          $ref: '#/components/schemas/feedbacks'
          description: This section will contain feedback Urls to provide feedback on the payment.
      required:
      - order
    cardPaymentMethodSpecificInput:
      type: object
      description: Object containing the specific input details for card payments
      properties:
        authorizationMode:
          $ref: '#/components/schemas/authorizationMode'
          description: >-
            Determines the type of the authorization that will be used. Allowed values: 
              * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
              * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
              * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

              Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
        initialSchemeTransactionId:
          $ref: '#/components/schemas/initialSchemeTransactionId'
          description: The unique scheme transactionId of the initial transaction that was performed with SCA. In case this is unknown a scheme transactionId of an earlier transaction part of the same sequence can be used as a fall-back. Strongly advised to be submitted for any MerchantInitiated or recurring transaction (a subsequent one).
        schemeReferenceData:
          $ref: '#/components/schemas/schemeReferenceData'
          description: This is the unique Scheme Reference Data from the initial transaction that was performed with a Strong Customer Authentication. In case this value is unknown, a Scheme Reference of an earlier transaction that was part of the same sequence can be used as a fall-back. Still, it is strongly advised to submit this value for any Merchant Initiated Transaction or any recurring transaction (hereby defined as "Subsequent").
        recurring:
          $ref: '#/components/schemas/cardRecurrenceDetails'
          description: Object containing data related to recurring
        skipAuthentication:
          type: boolean
          description: >-
            Deprecated: Use threeDSecure.skipAuthentication instead.
             * true = 3D Secure authentication will be skipped for this transaction. This setting should be used when isRecurring is set to true and recurringPaymentSequenceIndicator is set to recurring.
             * false = 3D Secure authentication will not be skipped for this transaction.

              Note: This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction.
          deprecated: true
          x-deprecated-by: threeDSecure.skipAuthentication
        token:
          $ref: '#/components/schemas/tokenIdInput'
          description: ID of the token to use to create the payment.
        tokenize:
          $ref: '#/components/schemas/tokenize'
          description: >-
            Indicates if this transaction should be tokenized
             * true - Tokenize the transaction. Note that a payment on the payment platform that results in a status REDIRECTED cannot be tokenized in this way.
             * false - Do not tokenize the transaction, unless it would be tokenized by other means such as auto-tokenization of recurring payments.
        transactionChannel:
          $ref: '#/components/schemas/transactionChannel'
          description: >-
            Indicates the channel via which the payment is created. Allowed values:
              * ECOMMERCE - The transaction is a regular E-Commerce transaction.
              * MOTO - The transaction is a Mail Order/Telephone Order.

              Defaults to ECOMMERCE.
        unscheduledCardOnFileRequestor:
          $ref: '#/components/schemas/unscheduledCardOnFileRequestor'
          description: >-
            Indicates which party initiated the unscheduled recurring transaction. Allowed values:
              * merchantInitiated - Merchant Initiated Transaction.
              * cardholderInitiated - Cardholder Initiated Transaction.
            Note:
              * This property is not allowed if isRecurring is true.
              * When a customer has chosen to use a token on a hosted checkout this property is set to "cardholderInitiated".
        unscheduledCardOnFileSequenceIndicator:
          $ref: '#/components/schemas/unscheduledCardOnFileSequenceIndicator'
          description: >-
            * first = This transaction is the first of a series of unscheduled recurring transactions

            * subsequent = This transaction is a subsequent transaction in a series of unscheduled recurring transactions

            Note: this property is not allowed if isRecurring is true.
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        card:
          $ref: '#/components/schemas/card'
          description: Object containing card details
        isRecurring:
          type: boolean
          description: >-
            * true - Indicates that the transactions is part of a scheduled recurring sequence. In addition, recurringPaymentSequenceIndicator indicates if the transaction is the first or subsequent in a recurring sequence. 

            * false - Indicates that the transaction is not part of a scheduled recurring sequence.

            The default value for this property is false.
        returnUrl:
          $ref: '#/components/schemas/returnUrl'
          description: >-
            The URL that the customer is redirect to after the payment flow has finished. You can add any number of key value pairs in the query string that, for instance help you to identify the customer when they return to your site. Please note that we will also append some additional key value pairs that will also help you with this identification process.

            Note: The provided URL should be absolute and contain the protocol to use, e.g. http:// or https://. For use on mobile devices a custom protocol can be used in the form of protocol://. This protocol must be registered on the device first.

            URLs without a protocol will be rejected.
        threeDSecure:
          $ref: '#/components/schemas/threeDSecure'
          description: Object containing specific data regarding 3-D Secure
        paymentProduct130SpecificInput:
          $ref: '#/components/schemas/paymentProduct130SpecificInput'
          description: Object containing specific input required for CB payments
        currencyConversion:
          $ref: '#/components/schemas/currencyConversionInput'
        paymentProduct3012SpecificInput:
          $ref: '#/components/schemas/paymentProduct3012SpecificInput'
          description: Object containing specific input required for bancontact.
        paymentProduct3208SpecificInput:
          $ref: '#/components/schemas/paymentProduct3208SpecificInput'
          description: Object containing specific input required for OneyDuplo Leroy Merlin payments.
        paymentProduct3209SpecificInput:
          $ref: '#/components/schemas/paymentProduct3209SpecificInput'
          description: Object containing specific input required for OneyDuplo Alcampo payments.
        cardOnFileRecurringFrequency:
          type: string
          description: >-
            Period of payment occurrence for recurring and installment payments. Allowed values:
              * Yearly
              * Quarterly
              * Monthly
              * Weekly
              * Daily
          enum:
          - Yearly
          - Quarterly
          - Monthly
          - Weekly
          - Daily
        cardOnFileRecurringExpiration:
          type: string
          description: >-
            The end date of the last scheduled payment in a series of transactions.

            Format YYYYMMDD
          maxLength: 8
          pattern: ^((19|20|21)\d{6})?$
        allowDynamicLinking:
          $ref: '#/components/schemas/allowDynamicLinking'
          description: >-
            * true - Default - Allows subsequent payments to use PSD2 dynamic linking from this payment (including Card On File).

            * false - Indicates that the dynamic linking (including Card On File data) will be ignored.
        multiplePaymentInformation:
          $ref: '#/components/schemas/multiplePaymentInformation'
          description: Container announcing forecoming subsequent payments. Holds modalities of these subsequent payments.
        cobrandSelectionIndicator:
          type: string
          description: >-
            For cobranded cards, this field indicates the brand selection method:
              * default - The holder implicitly accepted the default brand.
              * alternative - The holder explicitly selected an alternative brand.
              * notApplicable - The card is not cobranded.
          enum:
          - default
          - alternative
          - notApplicable
    paymentProduct3208SpecificInput:
      type: object
      description: Object containing specific input required for OneyDuplo Leroy Merlin payments.
      properties:
        merchantFinanceCode:
          $ref: '#/components/schemas/merchantFinanceCode'
          description: This field indicates the finance code provided by the merchant after the buyer has selected the proper financing option.
    paymentProduct3209SpecificInput:
      type: object
      description: Object containing specific input required for OneyDuplo Alcampo payments.
      properties:
        merchantFinanceCode:
          $ref: '#/components/schemas/merchantFinanceCode'
          description: This field indicates the finance code provided by the merchant after the buyer has selected the proper financing option.
    subsequentCardPaymentMethodSpecificInput:
      type: object
      description: Object containing the specific input details for subsequent card payments
      properties:
        authorizationMode:
          $ref: '#/components/schemas/authorizationMode'
          description: >-
            Determines the type of the authorization that will be used. Allowed values: 
              * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
              * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
              * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

              Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
        transactionChannel:
          $ref: '#/components/schemas/transactionChannel'
          description: >-
            Indicates the channel via which the payment is created. Allowed values:
              * ECOMMERCE - The transaction is a regular E-Commerce transaction.
              * MOTO - The transaction is a Mail Order/Telephone Order.

              Defaults to ECOMMERCE.
        subsequentType:
          $ref: '#/components/schemas/subsequentType'
          description: >-
            Determines the type of the subsequent that will be used. Allowed values: 
              * Recurring - Transactions processed at fixed, regular intervals not to exceed one year between Transactions, representing an agreement between a cardholder and a merchant to purchase goods or services provided over a period of time. Note that a recurring MIT transaction is initiated by the merchant (payee) not the customer (payer) and so is out of scope of PSD2. Recurring transactions that are in scope of PSD2 (and therefore may benefit from the recurring transaction exemption) are those that are customer (payer) initiates, e.g. standing orders set up from a bank account. 
              * Unscheduled - A transaction using a stored credential for a fixed or variable amount that does not occur on a scheduled or regularly occurring transaction date, where the cardholder has provided consent for the merchant to initiate one or more future transactions which are not initiated by the cardholder. This transaction type is based on an agreement with the cardholder and is not to be confused with cardholder initiated transactions performed with stored credentials (CITs are in scope of PSD2 whereas UCOF transactions are MITs and thus out of scope). 
              * Installment - Installment payments describe a single purchase of goods or services billed to a cardholder in multiple transactions over a period of time agreed by the cardholder and merchant. 
              * NoShow - A No-show is a transaction where the merchant is enabled to charge for services which the cardholder entered into an agreement to purchase but did not meet the terms of the agreement.
              * DelayedCharge - A delayed charge is typically used in hotel, cruise lines and vehicle rental payment scenarios to perform a supplemental account charge after original services are rendered.
              * PartialShipment - I-P e-Commerce scenario whereby credentials have been stored to enable subsequent MITs per shipment. For this type of use case, PartialShipment is expected on both the initial CIT and eventual subsequent MITs to complete the order.
              * Resubmission - This is an event that occurs when the original purchase occurred, but the merchant was not able to get authorization at the time the goods or services were provided. This is only applicable to contactless transit transactions.
        schemeReferenceData:
          type: string
          description: 'Deprecated: This is the unique Scheme Reference Data from the initial transaction that was performed with a Strong Customer Authentication. In case this value is unknown, a Scheme Reference of an earlier transaction that was part of the same sequence can be used as a fall-back. Still, it is strongly advised to submit this value for any Merchant Initiated Transaction or any recurring transaction (hereby defined as "Subsequent").'
          maxLength: 250
          deprecated: true
          x-deprecated-by: none
        token:
          type: string
          description: 'Deprecated: ID of the token to use to create the payment.'
          example: 0ca037cc-9079-4df7-8f6f-f2a3443ee521
          maxLength: 50
          deprecated: true
          x-deprecated-by: none
        paymentNumber:
          type: integer
          format: int32
          description: >-
            This payment's ordinal number in the sequence of payments. <br/> As the payments are numbered from 1 to the totalNumberOfPayments provided at initialization of the sequence in the multiplePaymentInformation container, the allowed values for this field actually depend on whether the initial call to CreatePayment or CreateHostedCheckout led to a payment or not. <br/>
              - if the initial call led to a payment, since it is implicitly numbered 1, then the allowed values for this field range from 2 to the totalNumberOfPayments.
              - if the initial call did not lead to a payment (e.g. this was a 0 amount operation for authentication), then the allowed values for this field range from 1 to the totalNumberOfPayments.
          minimum: 1
    card:
      type: object
      description: Object containing card details
      properties:
        cardholderName:
          $ref: '#/components/schemas/cardholderName'
          description: The card holder's name on the card.
        cardNumber:
          $ref: '#/components/schemas/cardNumber'
          description: >-
            The complete credit/debit card number (also know as the PAN)

            The card number is always obfuscated in any of our responses
        expiryDate:
          $ref: '#/components/schemas/expiryDate'
          description: >-
            Expiry date of the card

            Format: MMYY
        cvv:
          type: string
          description: Card Verification Value, a 3 or 4 digit code used as an additional security feature for card not present transactions.
          pattern: ^(\d{3,4})?$
    threeDSecure:
      type: object
      description: Object containing specific data regarding 3-D Secure
      properties:
        challengeCanvasSize:
          $ref: '#/components/schemas/challengeCanvasSize'
          description: >-
            Dimensions of the challenge window that potentially will be displayed to the customer. The challenge content is formatted to appropriately render in this window to provide the best possible user experience. Preconfigured sizes are width x height in pixels of the window displayed in the customer browser window. Possible values are
               * 250x400 (default)
               * 390x400
               * 500x600
               * 600x400
               * full-screen
        challengeIndicator:
          $ref: '#/components/schemas/challengeIndicator'
          description: >-
            Allows you to indicate if you want the customer to be challenged for extra security on this transaction. Possible values:
             * no-preference - You have no preference whether or not to challenge the customer (default)
             * no-challenge-requested - you prefer the cardholder not to be challenged
             * challenge-requested - you prefer the customer to be challenged
             * challenge-required - you require the customer to be challenged
             * no-challenge-requested-risk-analysis-performed â€“ letting the issuer know that you have already assessed the transaction with fraud prevention tool 
             * no-challenge-requested-data-share-only â€“ sharing data only with the DS
             * no-challenge-requested-consumer-authentication-performed â€“ authentication already happened at your side â€“ when login in to your website
             * no-challenge-requested-use-whitelist-exemption â€“ cardholder has whitelisted you at with the issuer
             * challenge-requested-whitelist-prompt-requested â€“ cardholder is trying to whitelist you
             * request-scoring-without-connecting-to-acs â€“ sending information to CB DS for a fraud scoring
        priorThreeDSecureData:
          $ref: '#/components/schemas/threeDSecureData'
          description: Object containing data regarding the customer authentication that occurred prior to the current transaction
        skipAuthentication:
          $ref: '#/components/schemas/skipAuthentication'
          description: >-
            * true = 3D Secure authentication will be skipped for this transaction. This setting should be used when isRecurring is set to true and recurringPaymentSequenceIndicator is set to "recurring"

            * false = 3D Secure authentication will not be skipped for this transaction


            Note: This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction
        redirectionData:
          $ref: '#/components/schemas/redirectionData'
          description: Object containing browser specific redirection related data
        externalCardholderAuthenticationData:
          $ref: '#/components/schemas/externalCardholderAuthenticationData'
          description: Object containing 3D secure details.
        exemptionRequest:
          $ref: '#/components/schemas/exemptionRequest'
          description: >-
            In PSD2, the ExemptionRequest field is used by merchants requesting an exemption when not using authentication on a transaction, in order to keep the conversion up.

            * none = No exemption requested

            * transaction-risk-analysis = Fraud analysis has been done already by your own fraud module and transaction scored as low risk

            * low-value = Bellow 30 euros

            * whitelist = The cardholder has whitelisted you with their issuer
        merchantFraudRate:
          $ref: '#/components/schemas/merchantFraudRate'
          description: >-
            Merchant fraud rate in the EEA (all EEA card fraud divided by all EEA card volumes) calculated as per PSD2 RTS. Mastercard will not calculate or validate the merchant fraud score

            Values accepted :

            * 1 - represents fraud rate less than or equal to 1 basis point [bp], which is 0.01%

            * 2 - represents fraud rate between 1 bp + - and 6 bps

            * 3 - represents fraud rate between 6 bps + - and 13 bps

            * 4 - represents fraud rate between 13 bps + - and 25 bps

            * 5 - represents fraud rate greater than 25 bps
        secureCorporatePayment:
          $ref: '#/components/schemas/secureCorporatePayment'
          description: >-
            Indicates dedicated payment processes and procedures were used, potential secure corporate payment exemption applies Logically this field should only be set to yes if the 

            acquirer exemption field is blank. A merchant cannot claim both acquirer exemption and  secure payment. However, the DS will not validate 

            the conditions in the extension. DS will pass data as presented.
        skipSoftDecline:
          $ref: '#/components/schemas/skipSoftDecline'
          description: >-
            * true = Soft Decline retry mechanism will be skipped for this transaction. The transaction will result in "Authorization Declined" status. This setting should be used when skipAuthentication is set to true and the merchant does not want to use Soft Decline retry mechanism.

            * false = Soft Decline retry mechanism will not be skipped for this transaction.


            Note: skipSoftDecline defaults to false if empty. This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction.
        authenticationAmount:
          $ref: '#/components/schemas/authenticationAmount'
          description: The amount to be authenticated. This field should be populated if the amount to be authenticated differs from the amount to be authorized (by default they are considered equal). Amount in cents and always having 2 decimals.
        deviceChannel:
          $ref: '#/components/schemas/deviceChannel'
          description: Determines whether the call is coming from an application or from a browser * AppBased - Call is coming from an application.  * Browser - Call is coming from a browser
    exemptionRequest:
      type: string
      description: >-
        In PSD2, the ExemptionRequest field is used by merchants requesting an exemption when not using authentication on a transaction, in order to keep the conversion up.

        * none = No exemption requested

        * transaction-risk-analysis = Fraud analysis has been done already by your own fraud module and transaction scored as low risk

        * low-value = Bellow 30 euros

        * whitelist = The cardholder has whitelisted you with their issuer
      enum:
      - none
      - transaction-risk-analysis
      - low-value
      - whitelist
    externalCardholderAuthenticationData:
      type: object
      description: Object containing 3D secure details.
      properties:
        cavv:
          type: string
          description: >-
            The CAVV (cardholder authentication verification value) or AAV (accountholder authentication value) provides an authentication validation value.

            Note:
              This is mandatory for ECI 2 and 5.
          maxLength: 50
        cavvAlgorithm:
          type: string
          description: >-
            The algorithm, from your 3D Secure provider, used to generate the authentication CAVV.

            Note:
              Required when
              * The 3D Secure authentication for the transaction is managed by a third-party 3D Secure authentication provider
              * You process the transaction through Atos
          maxLength: 1
        eci:
          type: integer
          format: int32
          description: >-
            Electronic Commerce Indicator provides authentication validation results returned after AUTHENTICATIONVALIDATION

            * 0 = No authentication, Internet (no liability shift, not a 3D Secure transaction)

            * 1 = Authentication attempted (MasterCard)

            * 2 = Successful authentication (MasterCard)

            * 5 = Successful authentication (Visa, Diners Club, Amex)

            * 6 = Authentication attempted (Visa, Diners Club, Amex)

            * 7 = No authentication, Internet (no liability shift, not a 3D Secure transaction)

            * (empty) = Not checked or not enrolled
          maxLength: 2
        threeDSecureVersion:
          type: string
          description: >-
            The 3-D Secure version used for the authentication. Possible values:

            * v1

            * v2

            * 1.0.2

            * 2.1.0

            * 2.2.0
        xid:
          type: string
          description: The transaction ID that is used for the 3D Authentication
          maxLength: 50
        directoryServerTransactionId:
          type: string
          description: >-
            The 3-D Secure Directory Server transaction ID that is used for the 3D Authentication

            Example: d4c849f8-24c6-4673-bf34-d0f822c81b16
          maxLength: 36
        schemeRiskScore:
          type: integer
          format: int32
          description: Global score calculated by the Carte Bancaire (130) Scoring platform. Possible values from 0 to 99.
          maxLength: 2
        acsTransactionId:
          type: string
          description: Identifier of the authenticated transaction at the ACS/Issuer.
          maxLength: 36
        appliedExemption:
          type: string
          description: Exemption code from Carte Bancaire (130) (unknown possible values so far -free format).
          maxLength: 4
        flow:
          $ref: '#/components/schemas/threeDSecureFlow'
          description: 3D Secure Flow used during this transaction.
    redirectionData:
      type: object
      description: Object containing browser specific redirection related data
      properties:
        returnUrl:
          type: string
          description: >-
            The URL that the customer is redirected to after the payment flow has finished. You can add any number of key value pairs in the query string that, for instance help you to identify the customer when they return to your site. Please note that we will also append some additional key value pairs that will also help you with this identification process.

            Note: The provided URL should be absolute and contain the protocol to use, e.g. http:// or https://. For use on mobile devices a custom protocol can be used in the form of protocol://. This protocol must be registered on the device first.

            URLs without a protocol will be rejected.
          maxLength: 200
      required:
      - returnUrl
    skipAuthentication:
      type: boolean
      description: >-
        * true = 3D Secure authentication will be skipped for this transaction. This setting should be used when isRecurring is set to true and recurringPaymentSequenceIndicator is set to "recurring"

        * false = 3D Secure authentication will not be skipped for this transaction


        Note: This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction
    skipSoftDecline:
      type: boolean
      description: >-
        * true = Soft Decline retry mechanism will be skipped for this transaction. The transaction will result in "Authorization Declined" status. This setting should be used when skipAuthentication is set to true and the merchant does not want to use Soft Decline retry mechanism.

        * false = Soft Decline retry mechanism will not be skipped for this transaction.


        Note: skipSoftDecline defaults to false if empty. This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction.
    authenticationAmount:
      type: integer
      format: int64
      description: The amount to be authenticated. This field should be populated if the amount to be authenticated differs from the amount to be authorized (by default they are considered equal). Amount in cents and always having 2 decimals.
    mobilePaymentMethodSpecificInput:
      type: object
      description: Object containing the specific input details for mobile payments
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        authorizationMode:
          $ref: '#/components/schemas/authorizationMode'
          description: >-
            Determines the type of the authorization that will be used. Allowed values: 
              * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
              * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
              * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

              Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
        decryptedPaymentData:
          $ref: '#/components/schemas/decryptedPaymentData'
          description: The payment data if you do the decryption of the encrypted payment data yourself.
        encryptedPaymentData:
          type: string
          description: >-
            The payment data if we will do the decryption of the encrypted payment data. Typically you'd use encryptedCustomerInput in the root of the create payment request to provide the encrypted payment data instead.

            * For Apple Pay, the encrypted payment data can be found in property data of the PKPayment.token.paymentData property.
        publicKeyHash:
          type: string
          description: >-
            Public Key Hash

            A unique identifier to retrieve key used by Apple to encrypt information.
        ephemeralKey:
          type: string
          description: >-
            Ephemeral Key

            A unique generated key used by Apple to encrypt data.
        requiresApproval:
          $ref: '#/components/schemas/requiresApproval'
          description: >-
            * true = the payment requires approval before the funds will be captured using the Approve payment or Capture payment API

            * false = the payment does not require approval, and the funds will be captured automatically
        paymentProduct320SpecificInput:
          $ref: '#/components/schemas/mobilePaymentProduct320SpecificInput'
          description: Object containing information specific to Google Pay. Required for payments with product 320.
    mobilePaymentMethodHostedCheckoutSpecificInput:
      type: object
      description: Object containing the specific input details for mobile payments
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        authorizationMode:
          $ref: '#/components/schemas/authorizationMode'
          description: >-
            Determines the type of the authorization that will be used. Allowed values: 
              * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
              * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
              * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

              Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
        paymentProduct320SpecificInput:
          $ref: '#/components/schemas/mobilePaymentProduct320SpecificInput'
          description: Object containing information specific to Google Pay. Required for payments with product 320.
        paymentProduct302SpecificInput:
          $ref: '#/components/schemas/mobilePaymentProduct302SpecificInput'
          description: Object containing information specific to Apple Pay. Required for payments with product 302.
    decryptedPaymentData:
      type: object
      description: The payment data if you do the decryption of the encrypted payment data yourself.
      properties:
        cardholderName:
          type: string
          description: >-
            Card holder's name on the card. 
             * For Apple Pay, maps to the cardholderName property in the encrypted payment data.
          x-trim-at: 50
        cryptogram:
          type: string
          description: >-
            The 3D secure online payment cryptogram.

            * For Apple Pay, maps to the paymentData.onlinePaymentCryptogram property in the encrypted payment data.

            * For Google Pay, maps to the paymentMethodDetails.3dsCryptogram property in the encrypted payment data.

            Not allowed for Google Pay if the paymentMethod is CARD.
          maxLength: 80
        dpan:
          type: string
          description: >-
            The device specific PAN. 
             * For Apple Pay, maps to the applicationPrimaryAccountNumber property in the encrypted payment.
          maxLength: 19
        eci:
          type: integer
          format: int32
          description: >-
            Electronic Commerce Indicator. 
             * For Apple Pay, maps to the paymentData.eciIndicator property in the encrypted payment data.
        expiryDate:
          type: string
          description: >-
            Expiry date of the card Format: MMYY. 
             * For Apple Pay, maps to the applicationExpirationDate property in the encrypted payment data. This property is formatted as YYMMDD, so this needs to be converted to get a correctly formatted expiry date
          example: 0529
          maxLength: 4
      required:
      - expiryDate
    mobilePaymentProduct320SpecificInput:
      type: object
      description: Object containing information specific to Google Pay. Required for payments with product 320.
      properties:
        threeDSecure:
          $ref: '#/components/schemas/gPayThreeDSecure'
          description: Object containing specific data regarding 3-D Secure
    mobilePaymentProduct302SpecificInput:
      type: object
      description: Object containing information specific to Apple Pay. Required for payments with product 302.
      properties:
        applePayRecurringPaymentRequest:
          $ref: '#/components/schemas/applePayRecurringPaymentRequest'
          description: Object containing information specific to Apple Pay recurrung request.
    applePayRecurringPaymentRequest:
      type: object
      description: Object containing information specific to Apple Pay recurrung request.
      properties:
        paymentDescription:
          type: string
          description: A description of the recurring payment that Apple Pay displays to the user in the payment sheet.
        regularBilling:
          $ref: '#/components/schemas/applePayLineItem'
          description: Object containing specific data regarding Apple Pay recurring regular payment
        trialBilling:
          $ref: '#/components/schemas/applePayLineItem'
          description: Object containing specific data regarding Apple Pay recurring trial payment
        billingAgreement:
          type: string
          description: A localized billing agreement that the payment sheet displays to the user before the user authorizes the payment.
        managementUrl:
          type: string
          description: A URL to a web page where the user can update or delete the payment method for the recurring payment.
      required:
      - paymentDescription
      - regularBilling
      - managementUrl
    gPayThreeDSecure:
      type: object
      description: Object containing specific data regarding 3-D Secure
      properties:
        challengeCanvasSize:
          $ref: '#/components/schemas/challengeCanvasSize'
          description: >-
            Dimensions of the challenge window that potentially will be displayed to the customer. The challenge content is formatted to appropriately render in this window to provide the best possible user experience. Preconfigured sizes are width x height in pixels of the window displayed in the customer browser window. Possible values are
               * 250x400 (default)
               * 390x400
               * 500x600
               * 600x400
               * full-screen
        challengeIndicator:
          $ref: '#/components/schemas/challengeIndicator'
          description: >-
            Allows you to indicate if you want the customer to be challenged for extra security on this transaction. Possible values:
             * no-preference - You have no preference whether or not to challenge the customer (default)
             * no-challenge-requested - you prefer the cardholder not to be challenged
             * challenge-requested - you prefer the customer to be challenged
             * challenge-required - you require the customer to be challenged
             * no-challenge-requested-risk-analysis-performed â€“ letting the issuer know that you have already assessed the transaction with fraud prevention tool 
             * no-challenge-requested-data-share-only â€“ sharing data only with the DS
             * no-challenge-requested-consumer-authentication-performed â€“ authentication already happened at your side â€“ when login in to your website
             * no-challenge-requested-use-whitelist-exemption â€“ cardholder has whitelisted you at with the issuer
             * challenge-requested-whitelist-prompt-requested â€“ cardholder is trying to whitelist you
             * request-scoring-without-connecting-to-acs â€“ sending information to CB DS for a fraud scoring
        exemptionRequest:
          $ref: '#/components/schemas/exemptionRequest'
          description: >-
            In PSD2, the ExemptionRequest field is used by merchants requesting an exemption when not using authentication on a transaction, in order to keep the conversion up.

            * none = No exemption requested

            * transaction-risk-analysis = Fraud analysis has been done already by your own fraud module and transaction scored as low risk

            * low-value = Bellow 30 euros

            * whitelist = The cardholder has whitelisted you with their issuer
        redirectionData:
          $ref: '#/components/schemas/redirectionData'
          description: Object containing browser specific redirection related data
        skipAuthentication:
          $ref: '#/components/schemas/skipAuthentication'
          description: >-
            * true = 3D Secure authentication will be skipped for this transaction. This setting should be used when isRecurring is set to true and recurringPaymentSequenceIndicator is set to "recurring"

            * false = 3D Secure authentication will not be skipped for this transaction


            Note: This is only possible if your account in our system is setup for 3D Secure authentication and if your configuration in our system allows you to override it per transaction
    applePayLineItem:
      type: object
      description: Object containing specific data regarding Apple Pay recurring payment.
      properties:
        label:
          type: string
          description: A required value thatâ€™s a short, localized description of the line item.
          example: Subscription
        amount:
          type: string
          description: A required value thatâ€™s the monetary amount of the line item.
          example: 20.00
        paymentTiming:
          type: string
          description: The time that the payment occurs as part of a successful transaction.
          enum:
          - immediate
          - recurring
          example: immediate
        recurringPaymentStartDate:
          type: string
          description: The date of the first payment. Example 2022-01-01T00:00:00
          example: 2022-01-01T00:00:00
          pattern: ^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$
        recurringPaymentIntervalUnit:
          type: string
          description: The amount of time â€” in calendar units, such as day, month, or year â€” that represents a fraction of the total payment interval.
          enum:
          - year
          - month
          - day
          - hour
          - minute
          example: month
        recurringPaymentIntervalCount:
          type: integer
          format: int64
          description: The number of interval units that make up the total payment interval.
          example: 1
        recurringPaymentEndDate:
          type: string
          description: The date of the final payment. Example 2022-01-01T00:00:00
          example: 2022-01-01T00:00:00
          pattern: ^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$
      required:
      - label
      - amount
    redirectPaymentProduct809SpecificInput:
      type: object
      description: Object containing specific input required for iDeal payments (Payment product ID 809)
      properties:
        issuerId:
          $ref: '#/components/schemas/issuerId'
          description: Unique ID of the issuing bank of the customer
    redirectPaymentProduct840SpecificInput:
      type: object
      description: Object containing specific input required for PayPal payments (Payment product ID 840)
      properties:
        addressSelectionAtPayPal:
          type: boolean
          description: >-
            Indicates whether to use PayPal Express Checkout Shortcut.
             * true = When shortcut is enabled, the consumer can select a shipping address during PayPal checkout.
             * false = When shortcut is disabled, the consumer cannot change the shipping address.
            Default value is false.

            Please note that this field is ignored when order.additionalInput.typeInformation.purchaseType is set to "digital"
        custom:
          type: string
          description: Free text field that you can use to support reconciliation flow.
        payLater:
          type: boolean
          description: >-
            Indicates whether to allow PayPal Pay Later option.
             * true = When option is enabled, the consumer can select the PayPal PayLater button given that the merchant meets the eligibility criteria from PayPal.
             * false = When option is disabled, the consumer cannot select the PayPal PayLater button.
            Default value is true.
        JavaScriptSdkFlow:
          type: boolean
          description: >-
            To be enabled when Javascript SDK integration is implemented on S2S flow
             * false = When this flag is disabled the field RedirectionURL is returned by CreatePayment call and should be used in merchant implementation to redirect buyer to PayPal.
             * true = When this flag is enabled the field orderID is returned by CreatePayment call and should be utilized in case merchant has integrated JS SDK button on their S2S implementation

            Default value is false.
    redirectPaymentProduct3302SpecificInput:
      type: object
      description: Object containing specific input required for Klarna PayLater payment (Payment product ID 3302)
      properties:
        organizationEntityType:
          type: string
          description: This parameter defines the legal form of a business  and is mandatory in B2B transactions,  Accurate classification ensures compliance and optimized payment handling.
          enum:
          - LIMITED_COMPANY
          - PUBLIC_LIMITED_COMPANY
          - ENTREPRENEURIAL_COMPANY
          - LIMITED_PARTNERSHIP_LIMITED_COMPANY
          - LIMITED_PARTNERSHIP
          - GENERAL_PARTNERSHIP
          - REGISTERED_SOLE_TRADER
          - SOLE_TRADER
          - CIVIL_LAW_PARTNERSHIP
          - PUBLIC_INSTITUTION
          - OTHER
        vatId:
          type: string
          description: Tax identification number used to validate a business's VAT compliance. Mandatory in B2B transactions
          example: DE812345678
          maxLength: 15
        organizationRegistrationId:
          type: string
          description: Unique identifier given by relevant authority verifying a business's legal registration. Mandatory in B2B transactions
          example: ABC 123456
          maxLength: 20
    redirectPaymentProduct3306SpecificInput:
      type: object
      description: Object containing specific input required for Klarna payments (Payment product ID 3306)
      properties:
        extraMerchantData:
          type: string
          description: >-
            In some cases, Klarna require additional information regarding the customer and the purchase in order to make 

            a correct risk assessment. This information, called extra merchant data (EMD), may consist of data 

            about the customer performing the transaction, the product/services associated with the transaction, 

            or the seller and their affiliates.
          maxLength: 20000
    redirectPaymentProduct5406SpecificInput:
      type: object
      description: Object containing specific input for EPS payments (Payment product ID 5406)
      properties:
        customerBankAccount:
          $ref: '#/components/schemas/customerBankAccount'
          description: Data of customer bank account
    redirectPaymentProduct5408SpecificInput:
      type: object
      description: Object containing specific input for Account to Account payments (Payment product ID 5408)
      properties:
        customerBankAccount:
          $ref: '#/components/schemas/customerBankAccount'
          description: Data of customer bank account
        instantPaymentOnly:
          type: boolean
          description: >-
            * true - consumer can only use instant payment for Account to Account Bank transfer payments

            * false - consumer can only use standard payment for Account to Account Bank transfer payments
    redirectPaymentProduct3203SpecificInput:
      type: object
      description: Object containing specific input for PostFinancePay payments (Payment product ID 3203).
      properties:
        checkoutType:
          $ref: '#/components/schemas/checkoutTypeValue'
          description: >-
            Determines the type of the checkout that will be used for PostFinancePay. Allowed values:
              * default - The user will be redirected to the PostFinancePay application to complete the payment.
              * expressCheckout -  In order to accelerate the payment process, the shipping and billing addresses are requested 
                                   from the payer's PostFinancePay account. These will be returned in the API response in 
                                   paymentProduct3203SpecificOutput. Note that the payer must accept to provide his 
                                   billing and shipping address during checkout in the PostFinancePay application. 
                                   If not, the payment will be declined.
    operationOutput:
      type: object
      description: Object containing operation details
      properties:
        id:
          $ref: '#/components/schemas/paymentId'
          description: Our unique payment transaction identifier
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/paymentStatusOutput'
          description: This object has the numeric representation of the current payment status, timestamp of last status change and performable action on the current payment resource. In case of failed payments and negative scenarios, detailed error information is listed.
        paymentMethod:
          $ref: '#/components/schemas/paymentMethod'
          description: Payment method identifier used by the our payment engine.
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
          deprecated: true
          x-deprecated-by: operationReferences
        operationReferences:
          $ref: '#/components/schemas/operationPaymentReferences'
          description: Object that holds all reference properties that are linked to this transaction
    refundResponse:
      type: object
      description: This object has the numeric representation of the current refund status, timestamp of last status change and performable action on the current refund resource. In case of a rejected refund, detailed error information is listed.
      properties:
        refundOutput:
          $ref: '#/components/schemas/refundOutput'
          description: Object containing refund details
        status:
          $ref: '#/components/schemas/statusValue'
          description: Current high-level status of the payment in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/orderStatusOutput'
        id:
          $ref: '#/components/schemas/paymentId'
          description: Our unique payment transaction identifier
          example: 3066019730_1
    refundOutput:
      type: object
      description: Object containing refund details
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        merchantParameters:
          $ref: '#/components/schemas/merchantParameters'
          description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
          deprecated: true
          x-deprecated-by: references/merchantParameters
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
          deprecated: true
          x-deprecated-by: operationReferences
        operationReferences:
          $ref: '#/components/schemas/operationPaymentReferences'
          description: Object that holds all reference properties that are linked to this transaction
        amountPaid:
          type: integer
          format: int64
        cardRefundMethodSpecificOutput:
          $ref: '#/components/schemas/refundCardMethodSpecificOutput'
        redirectRefundMethodSpecificOutput:
          $ref: '#/components/schemas/refundRedirectMethodSpecificOutput'
        eWalletRefundMethodSpecificOutput:
          $ref: '#/components/schemas/refundEWalletMethodSpecificOutput'
        mobileRefundMethodSpecificOutput:
          $ref: '#/components/schemas/refundMobileMethodSpecificOutput'
        paymentMethod:
          $ref: '#/components/schemas/paymentMethod'
          description: Payment method identifier used by the our payment engine.
    refundCardMethodSpecificOutput:
      type: object
      properties:
        totalAmountPaid:
          $ref: '#/components/schemas/totalAmount'
        totalAmountRefunded:
          $ref: '#/components/schemas/totalAmount'
        currencyConversion:
          $ref: '#/components/schemas/currencyConversion'
    refundRedirectMethodSpecificOutput:
      type: object
      properties:
        totalAmountPaid:
          $ref: '#/components/schemas/totalAmount'
        totalAmountRefunded:
          $ref: '#/components/schemas/totalAmount'
    refundEWalletMethodSpecificOutput:
      type: object
      properties:
        totalAmountPaid:
          $ref: '#/components/schemas/totalAmount'
        totalAmountRefunded:
          $ref: '#/components/schemas/totalAmount'
        paymentProduct840SpecificOutput:
          $ref: '#/components/schemas/refundPaymentProduct840SpecificOutput'
    refundPaymentProduct840SpecificOutput:
      type: object
      properties:
        customerAccount:
          $ref: '#/components/schemas/refundPaymentProduct840CustomerAccount'
    refundPaymentProduct840CustomerAccount:
      type: object
      properties:
        customerAccountStatus:
          type: string
        customerAddressStatus:
          type: string
        payerId:
          $ref: '#/components/schemas/payerId'
          description: The unique identifier of a PayPal account and will never change in the life cycle of a PayPal account
    refundMobileMethodSpecificOutput:
      type: object
      properties:
        totalAmountPaid:
          $ref: '#/components/schemas/totalAmount'
        totalAmountRefunded:
          $ref: '#/components/schemas/totalAmount'
        network:
          $ref: '#/components/schemas/network'
          description: The card network that was used for a mobile payment method operation
    statusValue:
      type: string
      description: Current high-level status of the payment in a human-readable form.
      enum:
      - CREATED
      - CANCELLED
      - REJECTED
      - REJECTED_CAPTURE
      - REDIRECTED
      - PENDING_PAYMENT
      - PENDING_COMPLETION
      - PENDING_CAPTURE
      - AUTHORIZATION_REQUESTED
      - CAPTURE_REQUESTED
      - CAPTURED
      - REVERSED
      - REFUND_REQUESTED
      - REFUNDED
    statusCategoryValue:
      type: string
      description: Highlevel status of the payment, payout or refund.
      enum:
      - CREATED
      - UNSUCCESSFUL
      - PENDING_PAYMENT
      - PENDING_MERCHANT
      - PENDING_CONNECT_OR_3RD_PARTY
      - COMPLETED
      - REVERSED
      - REFUNDED
    checkoutTypeValue:
      type: string
      description: >-
        Determines the type of the checkout that will be used for PostFinancePay. Allowed values:
          * default - The user will be redirected to the PostFinancePay application to complete the payment.
          * expressCheckout -  In order to accelerate the payment process, the shipping and billing addresses are requested 
                               from the payer's PostFinancePay account. These will be returned in the API response in 
                               paymentProduct3203SpecificOutput. Note that the payer must accept to provide his 
                               billing and shipping address during checkout in the PostFinancePay application. 
                               If not, the payment will be declined.
      enum:
      - default
      - expressCheckout
    orderStatusOutput:
      type: object
      properties:
        errors:
          type: array
          items:
            $ref: '#/components/schemas/aPIError'
            description: Contains detailed information on one single error.
          minItems: 0
          uniqueItems: false
        isCancellable:
          type: boolean
          description: >-
            Flag indicating if the payment can be cancelled 
             * true 
             * false
        statusCategory:
          $ref: '#/components/schemas/statusCategoryValue'
          description: Highlevel status of the payment, payout or refund.
        statusCode:
          $ref: '#/components/schemas/statusCode'
          description: Numeric status code of the legacy API. The value can also be found in the BackOffice and in report files.
        statusCodeChangeDateTime:
          $ref: '#/components/schemas/statusCodeChangeDateTime'
          description: Timestamp of the latest status change
    refundErrorResponse:
      type: object
      properties:
        errorId:
          type: string
        errors:
          type: array
          items:
            $ref: '#/components/schemas/aPIError'
            description: Contains detailed information on one single error.
        refundResult:
          $ref: '#/components/schemas/refundResponse'
          description: 'Deprecated: This field is not used by any payment product'
          deprecated: true
          x-deprecated-by: none
    refundRequest:
      type: object
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
          deprecated: true
          x-deprecated-by: operationReferences
        operationReferences:
          $ref: '#/components/schemas/operationPaymentReferences'
          description: Object that holds all reference properties that are linked to this transaction
        captureId:
          type: string
          description: The identifier of the capture that is used for partial refund. CaptureId is only necessary for Paypal/PostfinancePay multi-capture payments.
    refundsResponse:
      type: object
      properties:
        refunds:
          type: array
          description: The list of all refunds performed on the requested payment.
          items:
            $ref: '#/components/schemas/refundResponse'
            description: This object has the numeric representation of the current refund status, timestamp of last status change and performable action on the current refund resource. In case of a rejected refund, detailed error information is listed.
    testConnection:
      type: object
      properties:
        result:
          type: string
    validateCredentialsResponse:
      type: object
      properties:
        result:
          type: string
          description: The webhooks validation was OK (Valid) or not OK (Invalid).
          enum:
          - Valid
          - Invalid
    sessionResponse:
      type: object
      properties:
        assetUrl:
          type: string
          description: The datacenter-specific base url for assets. This value needs to be passed to the Client SDK to make sure that the client software connects to the right datacenter.
        clientApiUrl:
          type: string
          description: The datacenter-specific base url for client requests. This value needs to be passed to the Client SDK to make sure that the client software connects to the right datacenter.
        clientSessionId:
          type: string
          description: The identifier of the session that has been created.
        customerId:
          type: string
          description: The session is built up around the customer in the form of the customerId. All client APIs use this customerId in the URI to identify the customer.
        invalidTokens:
          $ref: '#/components/schemas/invalidTokens'
          description: Tokens that are submitted in the request are validated. In case any of the tokens can't be used anymore they are returned in this array. You should most likely remove those tokens from your system.
    validateCredentialsRequest:
      type: object
      properties:
        key:
          type: string
          description: The webhook key and without any change applied to it.
          example: bfa99cffb98dcad6c7f15824f5eb37
        secret:
          type: string
          description: Send here the hashed webhooks key secret in the same way as the check is done in your system. The only difference is instead of providing the current body of the message, use an empty string as body while hashing it.
          example: 0mw3LBod/3yPuI+UOmjpVmzL8U+YaTDC44haC6bzQpw=
    sendTestRequest:
      type: object
      properties:
        url:
          type: string
          description: Url to which the dummy webhook would be sent. If the parameter is not sent, It will be sent as default to the webhook url configured in the backoffice.
          example: https://example-webhook-url.com
    sessionRequest:
      type: object
      properties:
        tokens:
          type: array
          description: List of previously stored tokens linked to the customer that wants to checkout.
          items:
            type: string
          maxItems: 10
    tokenResponse:
      type: object
      properties:
        card:
          $ref: '#/components/schemas/tokenCard'
          description: Object containing card details
        eWallet:
          $ref: '#/components/schemas/tokenEWallet'
          description: Object containing eWallet details
        id:
          $ref: '#/components/schemas/id'
          description: ID of the token
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        externalTokenLinked:
          $ref: '#/components/schemas/externalTokenLinked'
        isTemporary:
          type: boolean
          description: Temporary tokens have a lifespan of two hours and can only be used once.
      required:
      - id
      - paymentProductId
    externalTokenLinked:
      type: object
      properties:
        GTSComputedToken:
          type: string
          description: 'Deprecated: Use the field ComputedToken instead.'
          deprecated: true
          x-deprecated-by: ComputedToken
        ComputedToken:
          type: string
          description: The computed token
        GeneratedToken:
          type: string
          description: The generated token
    tokenCard:
      type: object
      description: Object containing card details
      properties:
        alias:
          type: string
          description: An alias for the token. This can be used to visually represent the token.
          example: 483247XXXXXX0249
        data:
          $ref: '#/components/schemas/tokenCardData'
    tokenEWallet:
      type: object
      description: Object containing eWallet details
      properties:
        alias:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            An alias for the token. This can be used to visually represent the token.
          example: customer-account@email.com
          deprecated: true
          x-deprecated-by: none
        customer:
          $ref: '#/components/schemas/customerToken'
    customerToken:
      type: object
      properties:
        companyInformation:
          $ref: '#/components/schemas/companyInformation'
          description: Object containing company information
        billingAddress:
          $ref: '#/components/schemas/address'
          description: Object containing billing address details.
        personalInformation:
          $ref: '#/components/schemas/personalInformationToken'
    personalInformationToken:
      type: object
      properties:
        name:
          $ref: '#/components/schemas/personalNameToken'
    personalNameToken:
      type: object
      properties:
        firstName:
          type: string
          example: John
        surname:
          type: string
          example: Doe
    tokenCardData:
      type: object
      properties:
        cardWithoutCvv:
          $ref: '#/components/schemas/cardWithoutCvv'
        cobrandSelectionIndicator:
          $ref: '#/components/schemas/cobrandSelectionIndicator'
          description: >-
            For cobranded cards, this field indicates the brand selection method:
              * default - The holder implicitly accepted the default brand.
              * alternative - The holder explicitly selected an alternative brand.
              * notApplicable - The card is not cobranded.
    unscheduledCardOnFileRequestor:
      type: string
      description: >-
        Indicates which party initiated the unscheduled recurring transaction. Allowed values:
          * merchantInitiated - Merchant Initiated Transaction.
          * cardholderInitiated - Cardholder Initiated Transaction.
        Note:
          * This property is not allowed if isRecurring is true.
          * When a customer has chosen to use a token on a hosted checkout this property is set to "cardholderInitiated".
      enum:
      - merchantInitiated
      - cardholderInitiated
      x-enum-to-string: false
    unscheduledCardOnFileSequenceIndicator:
      type: string
      description: >-
        * first = This transaction is the first of a series of unscheduled recurring transactions

        * subsequent = This transaction is a subsequent transaction in a series of unscheduled recurring transactions

        Note: this property is not allowed if isRecurring is true.
      enum:
      - first
      - subsequent
      x-enum-to-string: false
    authorizationMode:
      type: string
      description: >-
        Determines the type of the authorization that will be used. Allowed values: 
          * FINAL_AUTHORIZATION - The payment creation results in an authorization that is ready for capture. Final authorizations can't be reversed and need to be captured for the full amount within 7 days. 
          * PRE_AUTHORIZATION - The payment creation results in a pre-authorization that is ready for capture. Pre-authortizations can be reversed and can be captured within 30 days. The capture amount can be lower than the authorized amount. 
          * SALE - The payment creation results in an authorization that is already captured at the moment of approval. 

          Only used with some acquirers, ignored for acquirers that do not support this. In case the acquirer does not allow this to be specified the authorizationMode is 'unspecified', which behaves similar to a final authorization.
      enum:
      - FINAL_AUTHORIZATION
      - PRE_AUTHORIZATION
      - SALE
      x-enum-to-string: false
    transactionChannel:
      type: string
      description: >-
        Indicates the channel via which the payment is created. Allowed values:
          * ECOMMERCE - The transaction is a regular E-Commerce transaction.
          * MOTO - The transaction is a Mail Order/Telephone Order.

          Defaults to ECOMMERCE.
      enum:
      - ECOMMERCE
      - MOTO
    subsequentType:
      type: string
      description: >-
        Determines the type of the subsequent that will be used. Allowed values: 
          * Recurring - Transactions processed at fixed, regular intervals not to exceed one year between Transactions, representing an agreement between a cardholder and a merchant to purchase goods or services provided over a period of time. Note that a recurring MIT transaction is initiated by the merchant (payee) not the customer (payer) and so is out of scope of PSD2. Recurring transactions that are in scope of PSD2 (and therefore may benefit from the recurring transaction exemption) are those that are customer (payer) initiates, e.g. standing orders set up from a bank account. 
          * Unscheduled - A transaction using a stored credential for a fixed or variable amount that does not occur on a scheduled or regularly occurring transaction date, where the cardholder has provided consent for the merchant to initiate one or more future transactions which are not initiated by the cardholder. This transaction type is based on an agreement with the cardholder and is not to be confused with cardholder initiated transactions performed with stored credentials (CITs are in scope of PSD2 whereas UCOF transactions are MITs and thus out of scope). 
          * Installment - Installment payments describe a single purchase of goods or services billed to a cardholder in multiple transactions over a period of time agreed by the cardholder and merchant. 
          * NoShow - A No-show is a transaction where the merchant is enabled to charge for services which the cardholder entered into an agreement to purchase but did not meet the terms of the agreement.
          * DelayedCharge - A delayed charge is typically used in hotel, cruise lines and vehicle rental payment scenarios to perform a supplemental account charge after original services are rendered.
          * PartialShipment - I-P e-Commerce scenario whereby credentials have been stored to enable subsequent MITs per shipment. For this type of use case, PartialShipment is expected on both the initial CIT and eventual subsequent MITs to complete the order.
          * Resubmission - This is an event that occurs when the original purchase occurred, but the merchant was not able to get authorization at the time the goods or services were provided. This is only applicable to contactless transit transactions.
      enum:
      - Recurring
      - Unscheduled
      - Installment
      - NoShow
      - DelayedCharge
      - PartialShipment
      - Resubmission
      x-enum-to-string: false
    getPublicKeyResponse:
      type: object
      properties:
        keyId:
          type: string
          description: The identifier of the key that is used to encrypt sensitive data
        publicKey:
          type: string
          description: The public key that is used to encrypt the sensitive data with. Only we have the private key and will be able to decrypt the encrypted payment details
    getPaymentProductGroupsResponse:
      type: object
      description: The response contains an array of payment product groups that match the filters supplied in the request.
      properties:
        paymentProductGroups:
          $ref: '#/components/schemas/paymentProductGroups'
          description: Array containing payment product groups and their characteristics
    paymentProductGroups:
      type: array
      description: Array containing payment product groups and their characteristics
      items:
        $ref: '#/components/schemas/paymentProductGroup'
    paymentProductGroup:
      type: object
      properties:
        id:
          type: string
          description: The ID of the payment product group in our system
        displayHints:
          $ref: '#/components/schemas/paymentProductDisplayHints'
          description: 'Deprecated: field is replaced by displayHintsList'
          deprecated: true
          x-deprecated-by: displayHintsList
        displayHintsList:
          $ref: '#/components/schemas/paymentProductDisplayHintsList'
          description: List of display hints
        accountOnFile:
          $ref: '#/components/schemas/accountOnFile'
    getPaymentProductsResponse:
      type: object
      description: The response contains an array of payment products that match the filters supplied in the request.
      properties:
        paymentProducts:
          $ref: '#/components/schemas/paymentProducts'
          description: Array containing payment products and their characteristics
    paymentProducts:
      type: array
      description: Array containing payment products and their characteristics
      items:
        $ref: '#/components/schemas/paymentProduct'
        description: Payment product
    paymentProductNetworksResponse:
      type: object
      description: 'Array containing network entries for a payment product. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
      properties:
        networks:
          $ref: '#/components/schemas/networks'
          description: 'Array containing network entries for a payment product. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
    networks:
      type: array
      description: 'Array containing network entries for a payment product. The strings that represent the networks in the array are identical to the strings that the payment product vendors use in their documentation. For instance: "Visa" for Apple Pay, and "VISA" for Google Pay.'
      items:
        type: string
    paymentProduct:
      type: object
      description: Payment product
      properties:
        accountsOnFile:
          type: array
          description: List of tokens for that payment product
          items:
            $ref: '#/components/schemas/accountOnFile'
        allowsRecurring:
          type: boolean
          description: >-
            Indicates if the product supports recurring payments

            * true - This payment product supports recurring payments

            * false - This payment product does not support recurring transactions and can only be used for one-off payments
        allowsTokenization:
          type: boolean
          description: >-
            Indicates if the payment details can be tokenized for future re-use

            * true - Payment details from payments done with this payment product can be tokenized for future re-use

            * false - Payment details from payments done with this payment product can not be tokenized
        displayHints:
          $ref: '#/components/schemas/paymentProductDisplayHints'
          description: 'Deprecated: field is replaced by displayHintsList'
          deprecated: true
          x-deprecated-by: displayHintsList
        displayHintsList:
          $ref: '#/components/schemas/paymentProductDisplayHintsList'
          description: List of display hints
        fields:
          type: array
          description: Object containing all the fields and their details that are associated with this payment product. If you are not interested in the data on the fields you should have us filter them our (using filter=fields in the query-string)
          items:
            $ref: '#/components/schemas/paymentProductField'
        id:
          type: integer
          format: int32
          description: The ID of the payment product in our system
        paymentMethod:
          $ref: '#/components/schemas/paymentMethod'
          description: Payment method identifier used by the our payment engine.
        paymentProductGroup:
          type: string
          description: >-
            The payment product group that has this payment product, if there is any. Not populated otherwise. Currently only one payment product group is supported:

            * cards
        usesRedirectionTo3rdParty:
          type: boolean
          description: >-
            Indicates whether the payment product requires redirection to a third party to complete the payment. You can use this to filter out products that require a redirect if you do not want to support that.

            * true - Redirection is required

            * false - No redirection is required
        paymentProduct302SpecificData:
          $ref: '#/components/schemas/paymentProduct302SpecificData'
          description: Apple Pay (payment product 302) specific details.
        paymentProduct320SpecificData:
          $ref: '#/components/schemas/paymentProduct320SpecificData'
          description: Google Pay (payment product 320) specific details.
        allowsAuthentication:
          type: boolean
          description: True when 3DS authentication is supported or required for the product
      required:
      - id
    accountOnFile:
      type: object
      properties:
        attributes:
          type: array
          items:
            $ref: '#/components/schemas/accountOnFileAttribute'
            description: Array containing the details of the stored token
        displayHints:
          $ref: '#/components/schemas/accountOnFileDisplayHints'
          description: Object containing information for the client on how best to display this field
        id:
          type: integer
          format: int32
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
    accountOnFileAttribute:
      type: object
      description: Array containing the details of the stored token
      properties:
        key:
          type: string
          description: Name of the key or property
        mustWriteReason:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            The reason why the status is MUST_WRITE. Currently only "IN_THE_PAST" is possible as value (for expiry date), but this can be extended with new values in the future.
          enum:
          - IN_THE_PAST
          deprecated: true
          x-deprecated-by: none
        status:
          type: string
          description: >-
            Possible values:

            * READ_ONLY - attribute cannot be updated and should be presented in that way to the user

            * CAN_WRITE - attribute can be updated and should be presented as an editable field, for example an expiration date that will expire very soon

            * MUST_WRITE - attribute should be updated and must be presented as an editable field, for example an expiration date that has already expired

            Any updated values that are entered for CAN_WRITE or MUST_WRITE will be used to update the values stored in the token.
          enum:
          - READ_ONLY
          - CAN_WRITE
          - MUST_WRITE
        value:
          type: string
          description: Value of the key or property
    accountOnFileDisplayHints:
      type: object
      description: Object containing information for the client on how best to display this field
      properties:
        labelTemplate:
          type: array
          description: Array of attribute keys and their mask
          items:
            $ref: '#/components/schemas/labelTemplateElement'
            description: Array of attribute keys and their mask
        logo:
          $ref: '#/components/schemas/logo'
          description: Partial URL that you can reference for the image of this payment product. You can use our server-side resize functionality by appending '?size={{width}}x{{height}}' to the full URL, where width and height are specified in pixels. The resized image will always keep its correct aspect ratio.
    labelTemplateElement:
      type: object
      description: Array of attribute keys and their mask
      properties:
        attributeKey:
          type: string
          description: Name of the attribute that is shown to the customer on selection pages or screens
        mask:
          type: string
          description: >-
            Regular mask for the attributeKey

            Note: The mask is optional as not every field has a mask
    paymentProductDisplayHints:
      type: object
      description: Object containing display hints like the order of the product when shown in a list, the name of the product and the logo
      properties:
        displayOrder:
          type: integer
          format: int32
          description: Determines the order in which the payment products and groups should be shown (sorted ascending)
        label:
          type: string
          description: Name of the payment product or group based on the locale that was included in the request
        logo:
          $ref: '#/components/schemas/logo'
          description: Partial URL that you can reference for the image of this payment product. You can use our server-side resize functionality by appending '?size={{width}}x{{height}}' to the full URL, where width and height are specified in pixels. The resized image will always keep its correct aspect ratio.
    paymentProductDisplayHintsList:
      type: array
      description: List of display hints
      items:
        $ref: '#/components/schemas/paymentProductDisplayHints'
        description: Object containing display hints like the order of the product when shown in a list, the name of the product and the logo
    paymentProductField:
      type: object
      properties:
        dataRestrictions:
          $ref: '#/components/schemas/paymentProductFieldDataRestrictions'
          description: Object containing data restrictions that apply to this field, like minimum and/or maximum length
        displayHints:
          $ref: '#/components/schemas/paymentProductFieldDisplayHints'
          description: Object containing display hints for this field, like the order, mask, preferred keyboard
        id:
          type: string
        type:
          type: string
          enum:
          - string
          - numericstring
          - date
          - expirydate
          - integer
          - boolean
    paymentProductFieldDataRestrictions:
      type: object
      description: Object containing data restrictions that apply to this field, like minimum and/or maximum length
      properties:
        isRequired:
          type: boolean
          description: >-
            * true - Indicates that this field is required

            * false - Indicates that this field is optional
        validators:
          $ref: '#/components/schemas/paymentProductFieldValidators'
          description: Object containing the details of the validations on the field
    paymentProductFieldValidators:
      type: object
      description: Object containing the details of the validations on the field
      properties:
        emailAddress:
          $ref: '#/components/schemas/emptyValidator'
        expirationDate:
          $ref: '#/components/schemas/emptyValidator'
        fixedList:
          $ref: '#/components/schemas/fixedListValidator'
        iban:
          $ref: '#/components/schemas/emptyValidator'
        length:
          $ref: '#/components/schemas/lengthValidator'
        luhn:
          $ref: '#/components/schemas/emptyValidator'
        range:
          $ref: '#/components/schemas/rangeValidator'
        regularExpression:
          $ref: '#/components/schemas/regularExpressionValidator'
        termsAndConditions:
          $ref: '#/components/schemas/emptyValidator'
    emptyValidator:
      type: object
    fixedListValidator:
      type: object
      properties:
        allowedValues:
          type: array
          items:
            type: string
    lengthValidator:
      type: object
      properties:
        maxLength:
          type: integer
          format: int32
        minLength:
          type: integer
          format: int32
    rangeValidator:
      type: object
      properties:
        maxValue:
          type: integer
          format: int32
        minValue:
          type: integer
          format: int32
    regularExpressionValidator:
      type: object
      properties:
        regularExpression:
          type: string
    paymentProductFieldDisplayHints:
      type: object
      description: Object containing display hints for this field, like the order, mask, preferred keyboard
      properties:
        alwaysShow:
          type: boolean
          description: >-
            * true - Indicates that this field is advised to be captured to increase the success rates even-though it isn't marked as required. Please note that making the field required could hurt the success rates negatively. This boolean only indicates our advise to always show this field to the customer.

            * false - Indicates that this field is not to be shown unless it is a required field.
        displayOrder:
          type: integer
          format: int32
          description: The order in which the fields should be shown (ascending)
        formElement:
          $ref: '#/components/schemas/paymentProductFieldFormElement'
          description: Object detailing the type of form element that should be used to present the field
        label:
          type: string
          description: Label/Name of the field to be used in the user interface
        link:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Link that should be used to replace the '{link}' variable in the label.
          deprecated: true
          x-deprecated-by: none
        mask:
          type: string
          description: >-
            A mask that can be used in the input field. You can use it to inject additional characters to provide a better user experience and to restrict the accepted character set (illegal characters will be ignored during typing).

            <br/>* is used for wildcards (and also chars)

            <br/>9 is used for numbers

            <br/>Everything outside {{ and }} is used as-is.
        obfuscate:
          type: boolean
          description: >-
            * true - The data in this field should be obfuscated as it is entered, just like a password field

            * false - The data in this field does not need to be obfuscated
        placeholderLabel:
          type: string
          description: A placeholder value for the form element
        preferredInputType:
          type: string
          description: >-
            The type of keyboard that can best be used to fill out the value of this field. Possible values are:

            * PhoneNumberKeyboard - Keyboard that is normally used to enter phone numbers

            * StringKeyboard - Keyboard that is used to enter strings

            * IntegerKeyboard - Keyboard that is used to enter only numerical values

            * EmailAddressKeyboard - Keyboard that allows easier entry of email addresses
        tooltip:
          $ref: '#/components/schemas/paymentProductFieldTooltip'
          description: Object that contains an optional tooltip to assist the customer
    paymentProductFieldFormElement:
      type: object
      description: Object detailing the type of form element that should be used to present the field
      properties:
        type:
          type: string
          description: >-
            Type of form element to be used. The following types can be returned:

            * text - A normal text input field

            * list - A list of values that the customer needs to choose from, is detailed in the valueMapping array

            * currency - Currency fields should be split into two fields, with the second one being specifically for the cents

            * boolean - Boolean fields should offer the customer a choice, like accepting the terms and conditions of a product.

            * date - let the customer pick a date.
        valueMapping:
          type: array
          description: 'Deprecated: This field is not used by any payment product'
          items:
            $ref: '#/components/schemas/valueMappingElement'
            description: An array of values and displayNames that should be used to populate the list object in the GUI
          deprecated: true
          x-deprecated-by: none
    valueMappingElement:
      type: object
      description: An array of values and displayNames that should be used to populate the list object in the GUI
      properties:
        displayElements:
          type: array
          items:
            $ref: '#/components/schemas/paymentProductFieldDisplayElement'
            description: List of extra data of the value.
        value:
          type: string
          description: Value corresponding to the key
    paymentProductFieldDisplayElement:
      type: object
      description: List of extra data of the value.
      properties:
        id:
          type: string
          description: The ID of the display element.
        label:
          type: string
          description: The label of the display element.
        type:
          type: string
          description: >-
            The type of the display element. Indicates how the value should be presented. Possible values are:

            * STRING - as plain text

            * CURRENCY - as an amount in cents displayed with a decimal separator and the currency of the payment

            * PERCENTAGE - as a number with a percentage sign

            * INTEGER - as an integer

            * URI - as a link
          enum:
          - STRING
          - CURRENCY
          - PERCENTAGE
          - INTEGER
          - URI
        value:
          type: string
          description: the value of the display element.
    paymentProductFieldTooltip:
      type: object
      description: Object that contains an optional tooltip to assist the customer
      properties:
        image:
          type: string
          description: >-
            Deprecated: This field is not used by any payment product

            Relative URL that can be used to retrieve an image for the tooltip image.
          deprecated: true
          x-deprecated-by: none
        label:
          type: string
          description: A text explaining the field in more detail. This is meant to be used for displaying to the customer.
    productDirectory:
      type: object
      properties:
        entries:
          type: array
          description: List of entries in the directory
          items:
            $ref: '#/components/schemas/directoryEntry'
    directoryEntry:
      type: object
      properties:
        issuerId:
          $ref: '#/components/schemas/issuerId'
          description: Unique ID of the issuing bank of the customer
        issuerList:
          type: string
          description: >-
            To be used to sort the issuers.
              short - These issuers should be presented at the top of the list
              long - These issuers should be presented after the issuers marked as short
              Note this is only filled if supported by the payment product. Currently only iDeal (809) support this. Sorting within the groups should be done alphabetically
          example: short
        issuerName:
          type: string
          description: Name of the issuing bank as it should be presented to the customer
          example: Rabobank
    challengeIndicator:
      type: string
      description: >-
        Allows you to indicate if you want the customer to be challenged for extra security on this transaction. Possible values:
         * no-preference - You have no preference whether or not to challenge the customer (default)
         * no-challenge-requested - you prefer the cardholder not to be challenged
         * challenge-requested - you prefer the customer to be challenged
         * challenge-required - you require the customer to be challenged
         * no-challenge-requested-risk-analysis-performed â€“ letting the issuer know that you have already assessed the transaction with fraud prevention tool 
         * no-challenge-requested-data-share-only â€“ sharing data only with the DS
         * no-challenge-requested-consumer-authentication-performed â€“ authentication already happened at your side â€“ when login in to your website
         * no-challenge-requested-use-whitelist-exemption â€“ cardholder has whitelisted you at with the issuer
         * challenge-requested-whitelist-prompt-requested â€“ cardholder is trying to whitelist you
         * request-scoring-without-connecting-to-acs â€“ sending information to CB DS for a fraud scoring
    hostedCheckoutId:
      type: string
      description: The ID of the Hosted Checkout Session in which the payment was made.
      example: 3066019730
    locale:
      type: string
      description: 'Locale used in the GUI towards the consumer. '
      maxLength: 6
    variant:
      type: string
      description: It is possible to upload multiple templates of your payment pages using the Merchant Portal. You can force the use of a custom template by specifying it in the variant field. This allows you to test out the effect of certain changes to your payment pages in a controlled manner. Please note that you need to specify the filename of the template or customization.
      example: my-custom-template.html
    paymentMethod:
      type: string
      description: Payment method identifier used by the our payment engine.
    merchantReference:
      type: string
      description: >-
        Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

        It is highly recommended to provide a single MerchantReference per unique order on your side
      example: your-order-6372
      maxLength: 40
    paymentProductId:
      type: integer
      format: int32
      description: Payment product identifier - Please see Products documentation for a full overview of possible values.
      example: 840
      maximum: 99999
      minimum: 0
    authorisationCode:
      type: string
      description: Card Authorization code as returned by the acquirer
    initialSchemeTransactionId:
      type: string
      description: The unique scheme transactionId of the initial transaction that was performed with SCA. In case this is unknown a scheme transactionId of an earlier transaction part of the same sequence can be used as a fall-back. Strongly advised to be submitted for any MerchantInitiated or recurring transaction (a subsequent one).
      maxLength: 100
    schemeReferenceData:
      type: string
      description: This is the unique Scheme Reference Data from the initial transaction that was performed with a Strong Customer Authentication. In case this value is unknown, a Scheme Reference of an earlier transaction that was part of the same sequence can be used as a fall-back. Still, it is strongly advised to submit this value for any Merchant Initiated Transaction or any recurring transaction (hereby defined as "Subsequent").
      maxLength: 250
    paymentAccountReference:
      type: string
      description: The Payment Account Reference is a unique alphanumeric identifier that links a PAN with all subsequent PANs for the same payment account (e.g., following card replacement) and all EMV payment tokens associated with that account. On its own Payment Account Reference cannot be used to start financial transactions, but it does allow for complying with regulatory requirements, performing risk analysis & supporting loyalty programs. Please note that the Payment Account Reference is a value returned after an authorization & only if provided by the acquirer and/or the issuer.
      maxLength: 29
    tokenIdInput:
      type: string
      description: ID of the token to use to create the payment.
      example: 0ca037cc-9079-4df7-8f6f-f2a3443ee521
      maxLength: 50
    tokenIdOutput:
      type: string
      description: ID of the token. This property is populated when the payment was done with a token or when the payment was tokenized.
      example: 0ca037cc-9079-4df7-8f6f-f2a3443ee521
      maxLength: 50
    paymentOption:
      type: string
      description: 'The specific payment option for the payment. To be used as a complement of the more generic paymentProductId (oney, banquecasino, cofidis), which allows to define a variation of the selected paymentProductId (ex: facilypay3x, banquecasino4x, cofidis3x-sansfrais, ...). List of modalities included in the payment product page.'
      maxLength: 64
    additionalInfo:
      type: string
      description: Second line of street or additional address information
      example: floor 9
      x-trim-at: 50
    city:
      type: string
      description: City
      example: Zaventem
      x-trim-at: 40
    countryCode:
      type: string
      description: ISO 3166-1 alpha-2 country code
      example: BE
      maxLength: 2
    houseNumber:
      type: string
      description: House number
      example: 3
      x-trim-at: 10
    state:
      type: string
      description: ISO 3166-2 country subdivision code
      x-trim-at: 50
    street:
      type: string
      description: Street name
      example: Da Vinci street
      x-trim-at: 50
    payerId:
      type: string
      description: The unique identifier of a PayPal account and will never change in the life cycle of a PayPal account
      example: RRCYJUTFJGZTA
    statusCode:
      type: integer
      format: int32
      description: Numeric status code of the legacy API. The value can also be found in the BackOffice and in report files.
    statusCodeChangeDateTime:
      type: string
      description: Timestamp of the latest status change
    dateOfBirth:
      type: string
      description: >-
        The date of birth of the customer of the recipient of the loan.

        Format YYYYMMDD
      maxLength: 8
      pattern: ^((19|20|21)\d{6})?$
    method:
      type: string
      description: >-
        Authentication used by the customer on your website

        Possible values are
         * guest = no login occurred, customer is logged in as guest
         * merchant-credentials = the customer logged in using credentials that are specific to you
         * federated-id = the customer logged in using a federated ID
         * issuer-credentials = the customer logged in using credentials from the card issuer (of the card used in this transaction)
         * third-party-authentication = the customer logged in using third-party authentication
         * fido-authentication = the customer logged in using a FIDO authenticator
         * cico-b-connect-token = the customer logged in using Check-in/Check-out b.connect
    priorAuthenticationMethod:
      type: string
      description: >-
        Method of authentication used for this transaction. Possible values:
         * frictionless = The authentication went without a challenge
         * challenged = Cardholder was challenged
         * avs-verified = The authentication was verified by AVS
         * other = Another issuer method was used to authenticate this transaction
    challengeCanvasSize:
      type: string
      description: >-
        Dimensions of the challenge window that potentially will be displayed to the customer. The challenge content is formatted to appropriately render in this window to provide the best possible user experience. Preconfigured sizes are width x height in pixels of the window displayed in the customer browser window. Possible values are
           * 250x400 (default)
           * 390x400
           * 500x600
           * 600x400
           * full-screen
    isFinal:
      type: boolean
      description: This property indicates whether this will be the final operation. The default value for this property is false.
    issuerId:
      type: string
      description: Unique ID of the issuing bank of the customer
      example: RABONL2U
      maxLength: 11
    totalAmount:
      type: integer
      format: int64
    fraudServiceResult:
      type: string
      description: >-
        Resulting advice of the fraud prevention checks. Possible values are:

        * accepted - Based on the checks performed the transaction can be accepted

        * challenged - Based on the checks performed the transaction should be manually reviewed

        * denied - Based on the checks performed the transaction should be rejected

        * no-advice - No fraud check was requested/performed

        * error - The fraud check resulted an error. Note that the fraud check was thus not performed.
    logo:
      type: string
      description: Partial URL that you can reference for the image of this payment product. You can use our server-side resize functionality by appending '?size={{width}}x{{height}}' to the full URL, where width and height are specified in pixels. The resized image will always keep its correct aspect ratio.
    invalidTokens:
      type: array
      description: Tokens that are submitted in the request are validated. In case any of the tokens can't be used anymore they are returned in this array. You should most likely remove those tokens from your system.
      items:
        type: string
      example: "['0a7042d6-bf24-40b6-85c5-dec24b9dd1d1', 'b7042dff-1fbd-46d2-9ecb-bddaa2c31165']"
    redirectUrl:
      type: string
      description: The full hosted checkout URL as generated by our system. Use this URL to redirect your customer to the hosted checkout page.
    partialRedirectUrl:
      type: string
      description: The partial URL as generated by our system. You will need to add the protocol and the relevant subdomain to this URL, before redirecting your customer to this URL. A special 'payment' subdomain will always work so you can always add 'https://payment.' at the beginning of this response value to view your hosted pages.
    hostedSessionTokens:
      type: string
      description: String containing comma separated tokens (no spaces) associated with the customer of this hosted session. Valid tokens will be used to present the customer the option to re-use previously used payment details. This means the customer for instance does not have to re-enter their card details again, which a big plus when the customer is using their mobile phone to complete the operation.
      example: 0ca037cc-9079-4df7-8f6f-f2a3443ee521,bece04aa-5131-4b96-adb9-c0b2d62bb38a,ae8b1b5c-fdb7-40ed-b483-432017a85cc9
    secureCorporatePayment:
      type: boolean
      description: >-
        Indicates dedicated payment processes and procedures were used, potential secure corporate payment exemption applies Logically this field should only be set to yes if the 

        acquirer exemption field is blank. A merchant cannot claim both acquirer exemption and  secure payment. However, the DS will not validate 

        the conditions in the extension. DS will pass data as presented.
    merchantFraudRate:
      type: integer
      format: int32
      description: >-
        Merchant fraud rate in the EEA (all EEA card fraud divided by all EEA card volumes) calculated as per PSD2 RTS. Mastercard will not calculate or validate the merchant fraud score

        Values accepted :

        * 1 - represents fraud rate less than or equal to 1 basis point [bp], which is 0.01%

        * 2 - represents fraud rate between 1 bp + - and 6 bps

        * 3 - represents fraud rate between 6 bps + - and 13 bps

        * 4 - represents fraud rate between 13 bps + - and 25 bps

        * 5 - represents fraud rate greater than 25 bps
      maximum: 99
      minimum: 0
    createTokenRequest:
      type: object
      description: Object containing the token details
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        card:
          $ref: '#/components/schemas/tokenCardSpecificInput'
          description: Object containing the token details for a card
    tokenCardSpecificInput:
      type: object
      description: Object containing the token details for a card
      properties:
        data:
          $ref: '#/components/schemas/tokenData'
          description: Object containing the token details for a card
    tokenData:
      type: object
      description: Object containing the token details for a card
      properties:
        card:
          $ref: '#/components/schemas/card'
          description: Object containing card details
        cobrandSelectionIndicator:
          $ref: '#/components/schemas/cobrandSelectionIndicator'
          description: >-
            For cobranded cards, this field indicates the brand selection method:
              * default - The holder implicitly accepted the default brand.
              * alternative - The holder explicitly selected an alternative brand.
              * notApplicable - The card is not cobranded.
    createdTokenResponse:
      type: object
      properties:
        card:
          $ref: '#/components/schemas/cardWithoutCvv'
        token:
          $ref: '#/components/schemas/id'
          description: ID of the token
        tokenStatus:
          $ref: '#/components/schemas/tokenStatus'
          description: >-
            This is the status of the token in the hosted tokenization session. Possible values are:

            * UNCHANGED - The token has not changed

            * CREATED - The token has been created

            * UPDATED - The token has been updated
        isNewToken:
          $ref: '#/components/schemas/isNewToken'
          description: >-
            Indicates if a new token was created 
             * true - A new token was created 
             * false - A token with the same card number already exists and is returned. Please note that the existing token has not been updated. When you want to update other data then the card number, you need to update data stored in the token explicitly, as data is never updated during the creation of a token.
        externalTokenLinked:
          $ref: '#/components/schemas/externalTokenLinked'
      required:
      - token
      - card
    createPayoutRequest:
      type: object
      description: Object containing the payout details
      properties:
        descriptor:
          $ref: '#/components/schemas/descriptor'
          description: >-
            Descriptive text that is used towards to customer, either during an online checkout at a third party and/or on the statement of the customer. For card transactions this is usually referred to as a Soft Descriptor. The maximum allowed length varies per card acquirer:
             * AIB - 22 characters
             * American Express - 25 characters
             * Atos Origin BNP - 15 characters
             * Barclays - 25 characters
             * Catella - 22 characters
             * CBA - 20 characters
             * Elavon - 25 characters
             * First Data - 25 characters
             * INICIS (INIPAY) - 22-30 characters
             * JCB - 25 characters
             * Merchant Solutions - 22-25 characters
             * Payvision (EU & HK) - 25 characters
             * SEB Euroline - 22 characters
             * Sub1 Argentina - 15 characters
             * Wells Fargo - 25 characters

            Note that we advise you to use 22 characters as the max length as beyond this our experience is that issuers will start to truncate. We currently also only allow per API call overrides for AIB and Barclays

            For alternative payment products the maximum allowed length varies per payment product:
             * 402 e-Przelewy - 30 characters
             * 404 INICIS - 80 characters
             * 802 Nordea ePayment Finland - 234 characters
             * 809 iDeal - 32 characters
             * 836 SOFORT - 42 characters
             * 840 PayPal - 127 characters
             * 841 WebMoney - 175 characters
             * 849 Yandex - 64 characters
             * 861 Alipay - 256 characters
             * 863 WeChat Pay - 32 characters
             * 880 BOKU - 20 characters
             * 8580 Qiwi - 255 characters
             * 1504 Konbini - 80 characters

            All other payment products do not support a descriptor.
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        cardPayoutMethodSpecificInput:
          $ref: '#/components/schemas/cardPayoutMethodSpecificInput'
          description: Object containing the payout details for a card
        omnichannelPayoutSpecificInput:
          $ref: '#/components/schemas/omnichannelPayoutSpecificInput'
          description: Object containing the additional payout details for a Omnichannel merchants
        references:
          $ref: '#/components/schemas/paymentReferences'
          description: 'Object that holds all reference properties that are linked to this transaction. **Deprecated for capture/refund**: Use operationReferences instead.'
        feedbacks:
          $ref: '#/components/schemas/feedbacks'
          description: This section will contain feedback Urls to provide feedback on the payment.
    cardPayoutMethodSpecificInput:
      type: object
      description: Object containing the payout details for a card
      properties:
        card:
          $ref: '#/components/schemas/card'
          description: Object containing card details
        token:
          $ref: '#/components/schemas/id'
          description: ID of the token
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        payoutReason:
          $ref: '#/components/schemas/payoutReason'
          description: >-
            Allows you to additionally specify the reason for initiating the payout for authorization purposes. If this field is not specified, authorisation of the payment will be made according to your merchant profile. Possible values are:
              * Gambling
              * Refund
              * Loyalty
    omnichannelPayoutSpecificInput:
      type: object
      description: Object containing the additional payout details for a Omnichannel merchants
      properties:
        paymentId:
          type: string
          description: The Payment Id of the transaction (either in-store or online), from which you request to make a refund.
    payoutStatus:
      type: string
      description: Current high-level status of the payout in a human-readable form.
      enum:
      - CREATED
      - PENDING_APPROVAL
      - REJECTED
      - PAYOUT_REQUESTED
      - ACCOUNT_CREDITED
      - REJECTED_CREDIT
      - CANCELLED
      - REVERSED
    payoutReason:
      type: string
      description: >-
        Allows you to additionally specify the reason for initiating the payout for authorization purposes. If this field is not specified, authorisation of the payment will be made according to your merchant profile. Possible values are:
          * Gambling
          * Refund
          * Loyalty
      enum:
      - Gambling
      - Refund
      - Loyalty
    payoutResponse:
      type: object
      properties:
        id:
          type: string
        payoutOutput:
          $ref: '#/components/schemas/payoutOutput'
        status:
          $ref: '#/components/schemas/payoutStatus'
          description: Current high-level status of the payout in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/payoutStatusOutput'
    payoutErrorResponse:
      type: object
      properties:
        errorId:
          type: string
        errors:
          $ref: '#/components/schemas/errors'
          description: Contains the set of errors
        payoutResult:
          $ref: '#/components/schemas/payoutResult'
    payoutResult:
      type: object
      properties:
        id:
          type: string
        payoutOutput:
          $ref: '#/components/schemas/payoutOutput'
        status:
          $ref: '#/components/schemas/payoutStatus'
          description: Current high-level status of the payout in a human-readable form.
        statusOutput:
          $ref: '#/components/schemas/payoutStatusOutput'
    payoutOutput:
      type: object
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        payoutReason:
          $ref: '#/components/schemas/payoutReason'
          description: >-
            Allows you to additionally specify the reason for initiating the payout for authorization purposes. If this field is not specified, authorisation of the payment will be made according to your merchant profile. Possible values are:
              * Gambling
              * Refund
              * Loyalty
    payoutStatusOutput:
      type: object
      properties:
        isCancellable:
          type: boolean
          description: >-
            Flag indicating if the payout can be cancelled 
             * true 
             * false
        statusCategory:
          $ref: '#/components/schemas/statusCategoryValue'
          description: Highlevel status of the payment, payout or refund.
        statusCode:
          $ref: '#/components/schemas/statusCode'
          description: Numeric status code of the legacy API. The value can also be found in the BackOffice and in report files.
    tokenStatus:
      type: string
      description: >-
        This is the status of the token in the hosted tokenization session. Possible values are:

        * UNCHANGED - The token has not changed

        * CREATED - The token has been created

        * UPDATED - The token has been updated
      enum:
      - UNCHANGED
      - CREATED
      - UPDATED
    paymentContext:
      type: object
      properties:
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        countryCode:
          type: string
          description: The country the payment takes place in
        isRecurring:
          type: boolean
          description: True if the payment is recurring
    getIINDetailsRequest:
      type: object
      description: Input for the retrieval of the IIN details request
      properties:
        bin:
          type: string
          description: The first digits of the credit card number from left to right with a minimum of 6 digits. Providing additional digits (up to 19) can result in more co-brands being returned.
          pattern: ^\d{6,19}$
        paymentContext:
          $ref: '#/components/schemas/paymentContext'
          description: Optional payment context to refine the IIN lookup to filter out payment products not applicable to your payment.
      required:
      - bin
    getIINDetailsResponse:
      type: object
      properties:
        coBrands:
          type: array
          description: List of IIN details
          items:
            $ref: '#/components/schemas/iINDetail'
        countryCode:
          type: string
          description: The ISO 3166-1 alpha-2 country code of the country where the card was issued. If we do not know where the card was issued, then the countryCode will return the value '99'.
        isAllowedInContext:
          $ref: '#/components/schemas/isAllowedInContext'
          description: >-
            Populated only if you submitted a payment context.

            * true - The payment product is allowed in the submitted context.

            * false - The payment product is not allowed in the submitted context. Note that in this case, none of the brands of the card will be allowed in the submitted context.
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        cardType:
          $ref: '#/components/schemas/cardType'
          description: >-
            The card's type as categorised by the payment method. Possible values are:
              * Credit
              * Debit
              * Prepaid
    iINDetail:
      type: object
      properties:
        isAllowedInContext:
          $ref: '#/components/schemas/isAllowedInContext'
          description: >-
            Populated only if you submitted a payment context.

            * true - The payment product is allowed in the submitted context.

            * false - The payment product is not allowed in the submitted context. Note that in this case, none of the brands of the card will be allowed in the submitted context.
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        cardType:
          $ref: '#/components/schemas/cardType'
          description: >-
            The card's type as categorised by the payment method. Possible values are:
              * Credit
              * Debit
              * Prepaid
    cardType:
      type: string
      description: >-
        The card's type as categorised by the payment method. Possible values are:
          * Credit
          * Debit
          * Prepaid
      enum:
      - Credit
      - Debit
      - Prepaid
    paymentProduct302SpecificData:
      type: object
      properties:
        networks:
          type: array
          description: The networks that can be used in the current payment context. The strings that represent the networks in the array are identical to the strings that Apple uses in their documentation. For instance "Visa".
          items:
            type: string
    paymentProduct320SpecificData:
      type: object
      properties:
        networks:
          type: array
          description: The networks that can be used in the current payment context. The strings that represent the networks in the array are identical to the strings that GooglePay uses in their documentation. For instance "Visa".
          items:
            type: string
        gateway:
          type: string
          description: The gateway identifier. You should use this when creating a [tokenization specification](https://developers.google.com/pay/api/android/reference/request-objects#Gateway) .
    surchargeSpecificInput:
      type: object
      description: Object containing specific input required to apply surcharging to an order.
      properties:
        mode:
          $ref: '#/components/schemas/surchargeMode'
          description: The surcharge mode to be applied to an order.
        surchargeAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: The surcharge amount of money to be applied to an order given that the merchant is in pass-through mode.
    surchargeSpecificOutput:
      type: object
      description: Object containing specific surcharging attributes applied to an order.
      properties:
        mode:
          $ref: '#/components/schemas/surchargeMode'
          description: The surcharge mode applied to an order.
        surchargeAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: The surcharge amount of money applied to an order.
        surchargeRate:
          $ref: '#/components/schemas/surchargeRate'
          description: A summary of surcharge details used in the calculation of the surcharge amount.  Null if result = NO_SURCHARGE
    surchargeForPaymentLink:
      type: object
      description: Object containing details how surcharge will be applied to a payment link.
      properties:
        surchargeMode:
          $ref: '#/components/schemas/surchargeMode'
          description: >-
            The surcharge mode which defines how a merchant will apply surcharging.

            * pass-through - Merchant to define and apply surcharge amount for a transaction for processing. This mode is not supported on Create Hosted Checkout Session.

            * on-behalf-of - Merchant to instruct the payment platform to calculate and apply a surcharge amount to a transaction, based on the merchantâ€™s surcharge configuration, net amount, and payment product type.
    surchargeMode:
      type: string
      description: >-
        The surcharge mode which defines how a merchant will apply surcharging.

        * pass-through - Merchant to define and apply surcharge amount for a transaction for processing. This mode is not supported on Create Hosted Checkout Session.

        * on-behalf-of - Merchant to instruct the payment platform to calculate and apply a surcharge amount to a transaction, based on the merchantâ€™s surcharge configuration, net amount, and payment product type.
      enum:
      - pass-through
      - on-behalf-of
    merchantParameters:
      type: string
      description: It allows you to store additional parameters for the transaction in the format you prefer (e.g.-> key-value query string, JSON, etc.) These parameters are then echoed back to you in API GET calls and Webhook notifications. This field must not contain any personal data.
      example: SessionID=126548354&ShopperID=73541312
      maxLength: 1000
    currencyConversionInput:
      type: object
      properties:
        acceptedByUser:
          $ref: '#/components/schemas/acceptedByUser'
          description: Dynamic Currency Conversion(DCC) Proposal accepted by user
        dccSessionId:
          type: string
          description: Dynamic Currency Conversion(DCC) Session Id that was previously returned by rate enquiry (/dccrate).
          example: 5cd02469177743fb8a0b2c78937ee25f
      required:
      - acceptedByUser
      - dccSessionId
    currencyConversion:
      type: object
      properties:
        acceptedByUser:
          $ref: '#/components/schemas/acceptedByUser'
          description: Dynamic Currency Conversion(DCC) Proposal accepted by user
        proposal:
          $ref: '#/components/schemas/dccProposal'
          description: Details of currency conversion to be proposed to the cardholder
      required:
      - acceptedByUser
    dccProposal:
      type: object
      description: Details of currency conversion to be proposed to the cardholder
      properties:
        baseAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        targetAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        rate:
          $ref: '#/components/schemas/rateDetails'
          description: Rate details given by the Dynamic Currency Conversion(DCC) provider
        disclaimerReceipt:
          type: string
          description: Card scheme disclaimer to print within cardholder receipt
          example: ''
          minLength: 1
          maxLength: 2000
        disclaimerDisplay:
          type: string
          description: Card scheme disclaimer to present to the cardholder
          example: Make sure you understand the costs of currency conversion as they may be different ...
          minLength: 1
          maxLength: 2000
      required:
      - baseAmount
      - targetAmount
    rateDetails:
      type: object
      properties:
        exchangeRate:
          type: number
          format: decimal
          description: Expressed as a percentage, applied to convert the original amount into the resulting amount without charge
          example: 1.57
        invertedExchangeRate:
          type: number
          format: decimal
          description: Exchange rate, expressed as a percentage, applied to convert the resulting amount into the original amount
          example: 0.63694
        markUpRate:
          type: number
          format: decimal
          description: The markup is the percentage added to the exchange rate by a provider when they sell you currency.
          example: 3.12
        quotationDateTime:
          type: string
          description: Date and time at which the exchange rate has been quoted
          example: 2021-07-21T08:00:00Z
          minLength: 20
          maxLength: 20
          pattern: ^([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])T([01][0-9]|2[0-3]):[0-5]\d:[0-5]\d)(\.\d+)?(([+-](\d{4}|(\d{2}\:\d{2})))|Z)?$
        source:
          type: string
          description: Indicates the exchange rate source name. The rate source is supplied for receipt printing purposes and to meet regulatory requirements where applicable
          example: European Central Bank
          minLength: 1
          maxLength: 2000
      required:
      - exchangeRate
      - markUpRate
      - quotationDateTime
      - source
    transaction:
      type: object
      properties:
        amount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
      required:
      - amount
    clientCurrencyConversionRequest:
      type: object
      properties:
        cardSource:
          $ref: '#/components/schemas/dccClientCardSource'
        transaction:
          $ref: '#/components/schemas/transaction'
      required:
      - cardSource
      - transaction
    currencyConversionRequest:
      type: object
      properties:
        cardSource:
          $ref: '#/components/schemas/dccCardSource'
        transaction:
          $ref: '#/components/schemas/transaction'
      required:
      - cardSource
      - transaction
    cardInfo:
      type: object
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        cardNumber:
          $ref: '#/components/schemas/cardNumber'
          description: Provide the complete credit/debit card number (also known as the PAN) for the most accurate results.
      required:
      - cardNumber
    dccCardSource:
      type: object
      properties:
        card:
          $ref: '#/components/schemas/cardInfo'
        token:
          $ref: '#/components/schemas/token'
          description: An identifier that represents card details that have previously been stored
        hostedTokenizationId:
          $ref: '#/components/schemas/hostedTokenizationId'
          description: An Id of a hosted tokenization session
        encryptedCustomerInput:
          type: string
          description: Data that was encrypted client-side that contains all customer-entered data elements, such as card data.
    dccClientCardSource:
      type: object
      properties:
        card:
          $ref: '#/components/schemas/cardInfo'
        token:
          $ref: '#/components/schemas/token'
          description: An identifier that represents card details that have previously been stored
    currencyConversionResponse:
      type: object
      description: Payload of the response to a rate inquiry request
      properties:
        dccSessionId:
          type: string
          description: The identifier of the Dynamic Currency Conversion(DCC) session that has been created. 'dccSessionId' will be populated exclusively when the result is "Allowed" for other outcomes such as "InvalidCard", "InvalidMerchant", "NoRate" or "NotAvailable" this field value will be an empty string.
          example: 5cd02469177743fb8a0b2c78937ee25f
        result:
          $ref: '#/components/schemas/currencyConversionResult'
          description: Result of a requested currency conversion
        proposal:
          $ref: '#/components/schemas/dccProposal'
          description: Details of currency conversion to be proposed to the cardholder
      required:
      - result
    currencyConversionResult:
      type: object
      description: Result of a requested currency conversion
      properties:
        result:
          type: string
          description: >-
            Functional response to the request:
             * Allowed: Dynamic currency conversion may be offered to the cardholder
             * InvalidCard: The card is not valid for dynamic currency conversion
             * InvalidMerchant: The card acceptor has not been recognised
             * NoRate: Exchange rates are not available
             * NotAvailable: Dynamic currency conversion is not available for other reason
          enum:
          - Allowed
          - InvalidCard
          - InvalidMerchant
          - NoRate
          - NotAvailable
          example: Allowed
        resultReason:
          type: string
          description: Plain text explaining the result of the currency conversion request
          example: Accepted
      required:
      - result
    currencyConversionSpecificInput:
      type: object
      description: Object containing specific input required for Dynamic Currency Conversion.
      properties:
        dccEnabled:
          type: boolean
          description: >-
            Indicates if this transaction is Dynamic Currency Conversion (DCC) enabled.

            * true - Dynamic Currency Conversion (DCC) is enabled in this transaction.

            * false - Dynamic Currency Conversion (DCC) is disabled in this transaction. The default value for this property is false.
    network:
      type: string
      description: The card network that was used for a mobile payment method operation
    calculateSurchargeRequest:
      type: object
      properties:
        cardSource:
          $ref: '#/components/schemas/cardSource'
          description: Contains elements from which card number can be obtained.
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
      required:
      - cardSource
      - amountOfMoney
    clientCalculateSurchargeRequest:
      type: object
      properties:
        cardSource:
          $ref: '#/components/schemas/clientCardSource'
          description: Contains elements from which card number can be obtained.
        amountOfMoney:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
      required:
      - cardSource
      - amountOfMoney
    calculateSurchargeResponse:
      type: object
      properties:
        surcharges:
          $ref: '#/components/schemas/surcharges'
          description: List of surcharge calculations matching the bin and paymentProductId if supplied
    surcharges:
      type: array
      description: List of surcharge calculations matching the bin and paymentProductId if supplied
      items:
        $ref: '#/components/schemas/surcharge'
    surcharge:
      type: object
      properties:
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
        result:
          type: string
          description: Token describing result. OK - A Surcharge Amount was successfully calculated, NO_SURCHARGE - A configured surcharge rate could not be found for the payment product
          enum:
          - OK
          - NO_SURCHARGE
          example: OK
        netAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: The amount of money to be charged to a payer not including any surcharge amount.
        surchargeAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: The amount of money to be charged to a payer, in addition to the net amount to cover the cost of processing that payment.  This value is calculated on the payment amount provided in the request, and the applicable ad valorem and/or specific surcharge rate configured for the merchant, for that payment.
        totalAmount:
          $ref: '#/components/schemas/amountOfMoney'
          description: The amount of money to be charged to a payer including any applicable surcharge. If you intend to apply additional services to the transaction before processing payment (such as DCC- Dynamic Currency Conversion), it is important to use this amount containing the surcharge instead of the net amount.
        surchargeRate:
          $ref: '#/components/schemas/surchargeRate'
          description: A summary of surcharge details used in the calculation of the surcharge amount.  Null if result = NO_SURCHARGE
    surchargeRate:
      type: object
      description: A summary of surcharge details used in the calculation of the surcharge amount. null if result = NO_SURCHARGE
      properties:
        surchargeProductTypeId:
          type: string
          description: The name of the applicable surcharge rates for the relevant payment product
          example: VISA_DOMESTIC_DEBIT
        surchargeProductTypeVersion:
          type: string
          description: A specific version identifier of the surcharge rates as applied for this request
          example: 8667F70E-9DDB-41FF-8822-A00FC43FCCAF
        adValoremRate:
          type: number
          format: decimal
          description: A percentage rate defined on a merchant's configuration used in the calculation of a surcharge amount.
          example: 2.5
        specificRate:
          type: integer
          format: int32
          description: A specific, fixed rate in cents defined on a merchant's configuration that is used in the calculation of a surcharge amount.
          example: 20
          minimum: 0
    cardNumber:
      type: string
      description: >-
        The complete credit/debit card number (also know as the PAN)

        The card number is always obfuscated in any of our responses
      maxLength: 19
      pattern: ^[1-9]\d{5}\d{0,13}$
    cardNumberObfuscated:
      type: string
      description: The obfuscated card number
      maxLength: 19
    cardSource:
      type: object
      description: Contains elements from which card number can be obtained.
      properties:
        card:
          $ref: '#/components/schemas/surchargeCalculationCard'
          description: An object containing card number and payment product id, which is used to determine surcharge product type
        token:
          $ref: '#/components/schemas/surchargeCalculationToken'
          description: An identifier that represents card details that have been previously stored
        hostedTokenizationId:
          $ref: '#/components/schemas/hostedTokenizationId'
          description: An Id of a hosted tokenization session
        encryptedCustomerInput:
          type: string
          description: Data that was encrypted client side containing all customer entered data elements like card data.
    clientCardSource:
      type: object
      description: Contains elements from which card number can be obtained.
      properties:
        card:
          $ref: '#/components/schemas/surchargeCalculationCard'
          description: An object containing card number and payment product id, which is used to determine surcharge product type
        token:
          $ref: '#/components/schemas/surchargeCalculationToken'
          description: An identifier that represents card details that have been previously stored
    surchargeCalculationCard:
      type: object
      description: An object containing card number and payment product id, which is used to determine surcharge product type
      properties:
        cardNumber:
          $ref: '#/components/schemas/cardNumber'
          description: >-
            The complete credit/debit card number (also know as the PAN)

            The card number is always obfuscated in any of our responses
        paymentProductId:
          $ref: '#/components/schemas/paymentProductId'
          description: Payment product identifier - Please see Products documentation for a full overview of possible values.
      required:
      - cardNumber
    surchargeCalculationToken:
      type: string
      description: An identifier that represents card details that have been previously stored
      maxLength: 50
    createPaymentLinkRequest:
      type: object
      description: An object containing the Create PaymentLink request.
      properties:
        expirationDate:
          type: string
          format: date-time
          description: >-
            The date after which the payment link will not be usable to complete the payment. The date sent cannot be more than 6 months in the future or a past date. It must also contain the UTC offset.


            Deprecated: Use `paymentLinkSpecificInput/expirationDate` instead.
          deprecated: true
          x-deprecated-by: paymentLinkSpecificInput/expirationDate
        description:
          type: string
          description: >-
            A note related to the created payment link.


            Deprecated: Use `paymentLinkSpecificInput/description` instead.
          maxLength: 1000
          deprecated: true
          x-deprecated-by: paymentLinkSpecificInput/description
        paymentLinkOrder:
          $ref: '#/components/schemas/paymentLinkOrderInput'
          description: >-
            An object containing the details of the related payment input.


            Deprecated: All properties in `paymentLinkOrder` are deprecated.  

            Use corresponding values as noted below:  

            | Property | Replacement |

            | - | - |

            | merchantReference | `references/merchantReference` |  

            | amount | `order/amountOfMoney` |  

            | surchargeSpecificInput | `order/surchargeSpecificInput` |
          deprecated: true
          x-deprecated-by: order
        recipientName:
          type: string
          description: >-
            The payment link recipient name.


            Deprecated: Use `paymentLinkSpecificInput/recipientName` instead.
          deprecated: true
          x-deprecated-by: paymentLinkSpecificInput/recipientName
        paymentLinkSpecificInput:
          $ref: '#/components/schemas/paymentLinkSpecificInput'
          description: An object containing details specific to payment link creation
        order:
          $ref: '#/components/schemas/order'
          description: >-
            Order object containing order related data 
             Please note that this object is required to be able to submit the amount.
        cardPaymentMethodSpecificInput:
          $ref: '#/components/schemas/cardPaymentMethodSpecificInputBase'
          description: Object containing the specific input details for card payments
        hostedCheckoutSpecificInput:
          $ref: '#/components/schemas/hostedCheckoutSpecificInput'
          description: Object containing hosted checkout specific data
        redirectPaymentMethodSpecificInput:
          $ref: '#/components/schemas/redirectPaymentMethodSpecificInput'
          description: Object containing the specific input details for payments that involve redirects to 3rd parties to complete, like iDeal and PayPal
        mobilePaymentMethodSpecificInput:
          $ref: '#/components/schemas/mobilePaymentMethodHostedCheckoutSpecificInput'
          description: Object containing the specific input details for mobile payments
        sepaDirectDebitPaymentMethodSpecificInput:
          $ref: '#/components/schemas/sepaDirectDebitPaymentMethodSpecificInputBase'
          description: Object containing the specific input details for SEPA direct debit payments
        fraudFields:
          $ref: '#/components/schemas/fraudFields'
          description: Object containing additional data that will be used to assess the risk of fraud
        feedbacks:
          $ref: '#/components/schemas/feedbacks'
          description: This section will contain feedback Urls to provide feedback on the payment.
    paymentLinkResponse:
      type: object
      description: An object representing a payment link.
      properties:
        expirationDate:
          type: string
          format: date-time
          description: The date after which the payment link will not be usable to complete the payment. The date will contain the UTC offset.
        paymentLinkOrder:
          $ref: '#/components/schemas/paymentLinkOrderOutput'
          description: An object containing the details of the related payment output.
        status:
          type: string
          description: >-
            The state of the payment link:
              * ACTIVE: The payment link is ready to be used.
              * PAID: The payment has been completed.
              * CANCELLED: The payment link has been manually cancelled.
              * EXPIRED: The payment link is not usable anymore.
          enum:
          - ACTIVE
          - PAID
          - CANCELLED
          - EXPIRED
        redirectionUrl:
          type: string
          description: The URL that will redirect the customer to the Hosted Checkout page to process the payment.
        recipientName:
          $ref: '#/components/schemas/recipientName'
          description: The payment link recipient name.
        paymentLinkId:
          type: string
          description: The unique link identifier.
        paymentId:
          type: string
          description: The unique payment transaction identifier. This id is only set when a payment was processed with this payment link.
        paymentLinkEvents:
          type: array
          items:
            $ref: '#/components/schemas/paymentLinkEvent'
            description: Changes related to a payment link status or usage.
      required:
      - expirationDate
      - paymentLinkOrder
      - status
      - redirectionUrl
      - paymentLinkId
    paymentLinkSpecificInput:
      type: object
      description: An object containing details specific to payment link creation
      properties:
        expirationDate:
          $ref: '#/components/schemas/expirationDate'
          description: The date after which the payment link will not be usable to complete the payment. The date sent cannot be more than 6 months in the future or a past date. It must also contain the UTC offset.
        description:
          $ref: '#/components/schemas/description'
          description: A note related to the created payment link.
        recipientName:
          $ref: '#/components/schemas/recipientName'
          description: The payment link recipient name.
    paymentLinkOrderInput:
      type: object
      description: >-
        An object containing the details of the related payment input.


        Deprecated: All properties in `paymentLinkOrder` are deprecated.  

        Use corresponding values as noted below:  

        | Property | Replacement |

        | - | - |

        | merchantReference | `references/merchantReference` |  

        | amount | `order/amountOfMoney` |  

        | surchargeSpecificInput | `order/surchargeSpecificInput` |
      properties:
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
        amount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        surchargeSpecificInput:
          $ref: '#/components/schemas/surchargeForPaymentLink'
          description: Object containing details how surcharge will be applied to a payment link.
      deprecated: true
      x-deprecated-by: order
    paymentLinkOrderOutput:
      type: object
      description: An object containing the details of the related payment output.
      properties:
        merchantReference:
          $ref: '#/components/schemas/merchantReference'
          description: >-
            Your unique reference of the transaction that is also returned in our report files. This is almost always used for your reconciliation of our report files.

            It is highly recommended to provide a single MerchantReference per unique order on your side
        amount:
          $ref: '#/components/schemas/amountOfMoney'
          description: Object containing amount and ISO currency code attributes
        surchargeSpecificOutput:
          $ref: '#/components/schemas/surchargeForPaymentLink'
          description: Object containing details how surcharge will be applied to a payment link.
      required:
      - merchantReference
      - amount
    paymentLinkEvent:
      type: object
      description: Changes related to a payment link status or usage.
      properties:
        dateTime:
          type: string
          description: The date and time the change occurred. The date will contain the UTC offset.
        type:
          type: string
          description: The type of event that occurred.
          enum:
          - CREATED
          - CLICKED
          - PAID
          - EXPIRED
          - CANCELLED
          - EMAIL_SENT
          - SMS_SENT
        details:
          type: string
          description: 'Details of the events. Ex.: email address or phone number of the recipient.'
      required:
      - dateTime
      - type
    id:
      type: string
      description: ID of the token
      example: 43763dd9-6b6b-4b05-914a-5953b14dcbba
    recipientName:
      type: string
      description: The payment link recipient name.
    buyerCompliantBankMessage:
      type: string
      description: This field indicates the text that must be returned and shown to the buyer to be compliant with the law regulating this payment product.
    errors:
      type: array
      description: Contains the set of errors
      items:
        $ref: '#/components/schemas/aPIError'
        description: Contains detailed information on one single error.
    isNewToken:
      type: boolean
      description: >-
        Indicates if a new token was created 
         * true - A new token was created 
         * false - A token with the same card number already exists and is returned. Please note that the existing token has not been updated. When you want to update other data then the card number, you need to update data stored in the token explicitly, as data is never updated during the creation of a token.
    expiryDate:
      type: string
      description: >-
        Expiry date of the card

        Format: MMYY
      example: 0529
      maxLength: 4
    cardholderName:
      type: string
      description: The card holder's name on the card.
      x-trim-at: 50
    legFare:
      type: integer
      format: int32
      description: >-
        Fee for this leg of the trip

        This field is used by the following payment products: 840
      minimum: 0
    tokenize:
      type: boolean
      description: >-
        Indicates if this transaction should be tokenized
         * true - Tokenize the transaction. Note that a payment on the payment platform that results in a status REDIRECTED cannot be tokenized in this way.
         * false - Do not tokenize the transaction, unless it would be tokenized by other means such as auto-tokenization of recurring payments.
    allowDynamicLinking:
      type: boolean
      description: >-
        * true - Default - Allows subsequent payments to use PSD2 dynamic linking from this payment (including Card On File).

        * false - Indicates that the dynamic linking (including Card On File data) will be ignored.
    returnUrl:
      type: string
      description: >-
        The URL that the customer is redirect to after the payment flow has finished. You can add any number of key value pairs in the query string that, for instance help you to identify the customer when they return to your site. Please note that we will also append some additional key value pairs that will also help you with this identification process.

        Note: The provided URL should be absolute and contain the protocol to use, e.g. http:// or https://. For use on mobile devices a custom protocol can be used in the form of protocol://. This protocol must be registered on the device first.

        URLs without a protocol will be rejected.
      maxLength: 200
    products:
      type: array
      description: List containing all payment product ids that should either be restricted to in or excluded from the payment context.
      items:
        type: integer
        format: int32
        maximum: 99999
        minimum: 0
      minItems: 0
      uniqueItems: true
    requiresApproval:
      type: boolean
      description: >-
        * true = the payment requires approval before the funds will be captured using the Approve payment or Capture payment API

        * false = the payment does not require approval, and the funds will be captured automatically
    merchantFinanceCode:
      type: string
      description: This field indicates the finance code provided by the merchant after the buyer has selected the proper financing option.
      minLength: 3
      maxLength: 3
    isAllowedInContext:
      type: boolean
      description: >-
        Populated only if you submitted a payment context.

        * true - The payment product is allowed in the submitted context.

        * false - The payment product is not allowed in the submitted context. Note that in this case, none of the brands of the card will be allowed in the submitted context.
    acceptedByUser:
      type: boolean
      description: Dynamic Currency Conversion(DCC) Proposal accepted by user
      example: true
    token:
      type: string
      description: An identifier that represents card details that have previously been stored
      maxLength: 50
    hostedTokenizationId:
      type: string
      description: An Id of a hosted tokenization session
    expirationDate:
      type: string
      format: date-time
      description: The date after which the payment link will not be usable to complete the payment. The date sent cannot be more than 6 months in the future or a past date. It must also contain the UTC offset.
    description:
      type: string
      description: A note related to the created payment link.
      maxLength: 1000
    paymentId:
      type: string
      description: Our unique payment transaction identifier
      example: 3066019730_0
    multiplePaymentInformation:
      type: object
      description: Container announcing forecoming subsequent payments. Holds modalities of these subsequent payments.
      properties:
        paymentPattern:
          type: string
          description: >-
            Typology of multiple payment. Allowed values:
              * PartialShipment
          enum:
          - PartialShipment
        totalNumberOfPayments:
          type: integer
          format: int32
          description: Total number of payments. If a payment is implied by this call, it implicitly has ordinal number 1.
      required:
      - paymentPattern
    cobrandSelectionIndicator:
      type: string
      description: >-
        For cobranded cards, this field indicates the brand selection method:
          * default - The holder implicitly accepted the default brand.
          * alternative - The holder explicitly selected an alternative brand.
          * notApplicable - The card is not cobranded.
      enum:
      - default
      - alternative
      - notApplicable
    deviceChannel:
      type: string
      description: Determines whether the call is coming from an application or from a browser * AppBased - Call is coming from an application.  * Browser - Call is coming from a browser
      enum:
      - appBased
      - browser
    descriptor:
      type: string
      description: >-
        Descriptive text that is used towards to customer, either during an online checkout at a third party and/or on the statement of the customer. For card transactions this is usually referred to as a Soft Descriptor. The maximum allowed length varies per card acquirer:
         * AIB - 22 characters
         * American Express - 25 characters
         * Atos Origin BNP - 15 characters
         * Barclays - 25 characters
         * Catella - 22 characters
         * CBA - 20 characters
         * Elavon - 25 characters
         * First Data - 25 characters
         * INICIS (INIPAY) - 22-30 characters
         * JCB - 25 characters
         * Merchant Solutions - 22-25 characters
         * Payvision (EU & HK) - 25 characters
         * SEB Euroline - 22 characters
         * Sub1 Argentina - 15 characters
         * Wells Fargo - 25 characters

        Note that we advise you to use 22 characters as the max length as beyond this our experience is that issuers will start to truncate. We currently also only allow per API call overrides for AIB and Barclays

        For alternative payment products the maximum allowed length varies per payment product:
         * 402 e-Przelewy - 30 characters
         * 404 INICIS - 80 characters
         * 802 Nordea ePayment Finland - 234 characters
         * 809 iDeal - 32 characters
         * 836 SOFORT - 42 characters
         * 840 PayPal - 127 characters
         * 841 WebMoney - 175 characters
         * 849 Yandex - 64 characters
         * 861 Alipay - 256 characters
         * 863 WeChat Pay - 32 characters
         * 880 BOKU - 20 characters
         * 8580 Qiwi - 255 characters
         * 1504 Konbini - 80 characters

        All other payment products do not support a descriptor.
      example: 22 character long desc
      maxLength: 256