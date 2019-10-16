using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.Services
{
    public class LocationService : ILocationService
    {
        //TODO: Consider adding this to config
        public const string PostcodeRetrieverBaseUrl = "https://postcodes.io/";

        private readonly HttpClient _httpClient;
        private readonly string _postcodeRetrieverBaseUrl;
        
        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _postcodeRetrieverBaseUrl = PostcodeRetrieverBaseUrl.TrimEnd('/');
        }

        public Location LookupPostcode(string postcode)
        {
            //https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/

            return new Location
            {
                Postcode = postcode
            };
        }
    }
}
