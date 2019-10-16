namespace DatabaseSampler.Application.Models
{
    public class Location
    {
        public string Postcode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
        
        public string DistrictCode { get; set; }

        public bool IsTerminated { get; set; }

        public short? TerminatedYear { get; set; }

        public short? TerminatedMonth { get; set; }
    }
}
