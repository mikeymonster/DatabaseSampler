using System;
using System.Collections.Generic;
using System.Text;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.Interfaces
{
    public interface ILocationService
    {
        Location LookupPostcode(string postcode);
    }
}
