using System;
using System.Collections.Generic;
using System.Net.Http;
using DatabaseSampler.Application.Messages;

namespace DatabaseSampler.Application.Tests.TestHttpFactories
{
    public class PostcodesTestHttpClientFactory : TestHttpClientFactory
    {
        public HttpClient Get(string requestPostcode,
            bool isValidPostcode,
            PostcodeLookupResultData postcodeResponseData,
            bool isTerminatedPostcode,
            TerminatedPostcodeLookupResultData terminatedPostcodeResponseData)
        {
            var requests = new List<RequestWrapper>();

            if (isValidPostcode && postcodeResponseData != null)
            {
                requests.Add(new RequestWrapper
                {
                    Uri = $"https://api.postcodes.io/postcodes/{Uri.EscapeDataString(requestPostcode)}",
                    ResponseObject = 
                        //(isValidPostcode && postcodeResponseData != null) ?
                        (object)new PostcodeLookupResponse
                        {
                            Result = postcodeResponseData,
                            Status = "OK"
                        } 
                        //:
                        //new ErrorResponse
                        //{
                        //    Error = "Invalid postcode",
                        //    Status = "NotFound"
                        //}
                });
            }

            if (isTerminatedPostcode && terminatedPostcodeResponseData != null)
            {
                requests.Add(new RequestWrapper
                {
                    Uri = $"https://api.postcodes.io/terminated_postcodes/{Uri.EscapeDataString(requestPostcode)}",
                    ResponseObject = 
                        //(isTerminatedPostcode && terminatedPostcodeResponseData != null) ?
                        (object)new TerminatedPostcodeLookupResponse
                        {
                            Result = terminatedPostcodeResponseData,
                            Status = "OK"
                        } 
                        //:
                        //new ErrorResponse
                        //{
                        //    Error = "Terminated postcode not found",
                        //    Status = "NotFound"
                        //}
                });
            }

            return CreateClient(requests);
        }
    }
}