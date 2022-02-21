using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlinePayments.Sdk
{
    /// <summary>
    /// Represents a set of request parameters.
    /// </summary>
    abstract public class AbstractParamRequest
    {
        abstract public IEnumerable<RequestParam> ToRequestParameters();
    }
}
