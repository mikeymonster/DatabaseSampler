using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseSampler.Controllers
{
    [AllowAnonymous]
    public class CosmosController : Controller
    {
        public CosmosController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
