using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseSampler.Application.ViewModels
{
    public class PostcodeLookupViewModel
    {
        [Required(ErrorMessage = "You must enter a postcode")]
        public string Postcode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool IsTerminated { get; set; }

        public short? TerminatedYear { get; set; }

        public short? TerminatedMonth { get; set; }


        public bool UseCachedResponse { get; set; }

        public bool FoundInCache { get; set; }

        public TimeSpan? ElapsedTimeForLookup { get; set; }
    }
}
