using System.Text.Json.Serialization;

namespace DatabaseSampler.Application.Messages
{
    public class TerminatedPostcodeLookupResultData
    {
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("year_terminated")]
        public string TerminatedYear { get; set; }

        [JsonPropertyName("month_terminated")]
        public string TerminatedMonth { get; set; }
    }
}