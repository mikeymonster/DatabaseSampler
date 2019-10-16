using System.Diagnostics;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseSampler.Controllers
{
    [AllowAnonymous]
    public class CachingController : Controller
    {
        private readonly ILocationService _locationService;
        public CachingController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            return View(new PostcodeLookupViewModel());
        }

        [HttpPost]
        public IActionResult LookupPostcode(PostcodeLookupViewModel viewModel)
        {
            //TODO: Validation

            var stopwatch = Stopwatch.StartNew();
            var result = _locationService.LookupPostcode(viewModel.Postcode);
            stopwatch.Stop();

            viewModel.ElapsedTimeForLookup = stopwatch.Elapsed;
            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult LookupPostcodeWithCaching(PostcodeLookupViewModel viewModel)
        {
            return LookupPostcode(viewModel);
        }

    }
}
