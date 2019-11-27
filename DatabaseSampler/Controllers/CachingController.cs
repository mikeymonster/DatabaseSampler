using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.Models;
using DatabaseSampler.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DatabaseSampler.Controllers
{
    [AllowAnonymous]
    public class CachingController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ILocationService _locationService;

        public CachingController(IMemoryCache cache, ILocationService locationService)
        {
            _cache = cache;
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            return View(new PostcodeLookupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LookupPostcode(PostcodeLookupViewModel viewModel)
        {
            //TODO: Validation
            //TODO: Handle UseCachedResponse correctly

            var stopwatch = Stopwatch.StartNew();
            var result = await _locationService.LookupPostcodeAsync(viewModel.Postcode);
            stopwatch.Stop();

            viewModel.Postcode = result.Postcode;
            viewModel.ElapsedTimeForLookup = stopwatch.Elapsed;

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LookupPostcodeWithCaching(PostcodeLookupViewModel viewModel)
        {
            viewModel.UseCachedResponse = true;
            var foundInCache = true;

            var stopwatch = Stopwatch.StartNew();

            var cacheKey = $"Postcode_{viewModel.Postcode.Replace(" ", "")}";

            // Look for cache key.
            if (!_cache.TryGetValue(cacheKey, out Location result))
            {
                foundInCache = false;
                result = await _locationService.LookupPostcodeAsync(viewModel.Postcode);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _cache.Set(cacheKey, result, cacheEntryOptions);
            }

            stopwatch.Stop();

            viewModel.Postcode = result.Postcode;
            viewModel.FoundInCache = foundInCache;
            viewModel.ElapsedTimeForLookup = stopwatch.Elapsed;

            return View("Index", viewModel);
        }
    }
}
