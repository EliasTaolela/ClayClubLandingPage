using LandingPage.Data;
using LandingPage.Models;
using LandingPage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    [Authorize]
    public class SubscribeController : Controller
    {

        private readonly ISubscribeService _service;

        public SubscribeController(ISubscribeService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(string email, string user)
        {
            var result = _service.AddAsync(email, user);

            return Ok(result);
        }
    }
}
