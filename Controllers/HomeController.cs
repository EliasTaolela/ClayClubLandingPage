using LandingPage.Models;
using LandingPage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LandingPage.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscribeService _subscribeService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            ISubscribeService subscribeService)
        {
            _userManager = userManager;
            _subscribeService = subscribeService;
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // 🔥 SHOW USER-SPECIFIC DATA
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var data = _subscribeService.GetUserSubscriptions(user.Id);

            return View(data); // pass to view
        }

        // 🔥 SAVE DATA (POST)
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            var user = await _userManager.GetUserAsync(User);

            await _subscribeService.AddAsync(email, user.Id);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous] // allow error page without login
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}