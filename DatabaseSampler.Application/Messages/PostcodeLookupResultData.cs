﻿// ReSharper disable UnusedMember.Global

using System.Text.Json.Serialization;

namespace DatabaseSampler.Application.Messages
{
    public class PostcodeLookupResultData
    {
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("outcode")]
        public string Outcode { get; set; }

        //[JsonPropertyName("admin_district")]
        //public string AdminDistrict { get; set; }

        [JsonPropertyName("admin_county")]
        public string AdminCounty { get; set; }

        [JsonPropertyName("codes")]
        public PostcodeLookupLocationCodesData Codes { get; set; }
    }
}