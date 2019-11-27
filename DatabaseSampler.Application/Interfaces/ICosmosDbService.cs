using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.Interfaces
{
    public interface ICosmosDbService
    {
        Task<IList<Expense>> GetItemsAsync(string queryString);
    }
}
