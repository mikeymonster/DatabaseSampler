using System;
using System.Collections.Generic;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.ViewModels
{
    public class CosmosItemsViewModel
    {
        public IList<Expense> Expenses { get; set; }
    }
}
