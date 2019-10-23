using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseSampler.Application.ViewModels
{
    public class PostcodeLookupViewModel
    {
        public string Postcode { get; set; }

        public bool UseCachedResponse { get; set; }

        public bool FoundInCache { get; set; }

        public TimeSpan? ElapsedTimeForLookup { get; set; }
    }
}
