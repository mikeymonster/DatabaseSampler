using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using DatabaseSampler.Application.Messages;
using Sfa.Tl.Matching.Models.Dto;

namespace DatabaseSampler.Application.Tests.TestHttpFactories
{
    public class PostcodesTestHttpClientFactory : TestHttpClientFactory
    {
        public HttpClient Get(string requestPostcode, PostcodeLookupResultData responseData)
        {
            var response = new PostcodeLookupResponse
            {
                Result = responseData,
                Status = "OK"
            };

            return CreateClient(response, $"https://example.com/postcodes/{requestPostcode.Replace(" ", "")}");
        }
    }
}