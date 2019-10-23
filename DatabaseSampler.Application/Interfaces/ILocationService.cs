using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.Interfaces
{
    public interface ILocationService
    {
        Task<Location> LookupPostcodeAsync(string postcode);
    }
}
