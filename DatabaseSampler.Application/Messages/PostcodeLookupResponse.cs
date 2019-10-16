using System.Text.Json.Serialization;
using DatabaseSampler.Application.Messages;

namespace Sfa.Tl.Matching.Models.Dto
{
    public class PostcodeLookupResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("result")]
        public PostcodeLookupResultData Result { get; set; }
    }
}