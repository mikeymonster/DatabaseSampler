using System.Net.Http;
using DatabaseSampler.Application.Services;
using FluentAssertions;
using Xunit;

namespace DatabaseSampler.Application.Tests
{
    public class LocationTests
    {
        [Fact]
        public void Lookup_Postcode_Returns_Expected_Result()
        {
            //TODO: Use the factory
            var httpClient = new HttpClient();
            var service = new LocationService(httpClient);

            //https://api.postcodes.io/postcodes/SW1A%202AA

            var result = service.LookupPostcode("SW1A 2AA");

            result.Should().NotBeNull();

            result.Postcode.Should().Be("SW1A 2AA");
            result.Latitude.Should().Be(51.50354M);
            result.Longitude.Should().Be(-0.127695M);
            result.DistrictCode.Should().Be("E09000033");
            result.IsTerminated.Should().BeFalse();
            result.TerminatedYear.Should().BeNull();
            result.TerminatedMonth.Should().BeNull();
        }

        [Fact]
        public void Lookup_Terminated_Postcode_Returns_Expected_Result()
        {
            //TODO: Use the factory
            var httpClient = new HttpClient();
            var service = new LocationService(httpClient);

            //https://api.postcodes.io/terminated_postcodes/AB1%200AX

            var result = service.LookupPostcode("AB1 0AX");

            result.Should().NotBeNull();

            result.Postcode.Should().Be("AB1 0AX");
            result.Latitude.Should().Be(57.095679M);
            result.Longitude.Should().Be(-2.24788M);
            result.DistrictCode.Should().BeNull();
            result.IsTerminated.Should().BeTrue();
            result.TerminatedYear.Should().Be(1996);
            result.TerminatedMonth.Should().Be(6);
        }
    }
}
