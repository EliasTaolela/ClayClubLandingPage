using LandingPage.Models;
using LandingPage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LandingPage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscribeService _subscribeService;

        public AdminController(UserManager<ApplicationUser> userManager, ISubscribeService subscribeService)
        {
            _userManager = userManager;
            _subscribeService = subscribeService;
        }

        public IActionResult UserData(string id)
        {
            var data = _subscribeService.GetUserSubscriptions(id);

            return View(data);
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userRoles = new List<(ApplicationUser User, IList<string> Roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add((user, roles));
            }

            return View(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
                if (user.Id != _userManager.GetUserId(User))
                {
                    await _userManager.DeleteAsync(user);
                }

                return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
