using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Controllers
{
    public class ManageController : Controller
    {
        private UserManager<IdentityUser> userManager;
        public ManageController(UserManager<IdentityUser> _userManager)
        {
            userManager = _userManager;
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet]
        public IActionResult Index()
        {
            List<IdentityUser> users = userManager.Users.ToList<IdentityUser>();          
            ViewBag.users = users;
            return View();
        }

        [Route("Manage/GiveRole/{userId}/{role}")]
        [HttpPost]
        public async Task<IActionResult> GiveRole(string userId, string role)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);
            await userManager.RemoveFromRoleAsync(user, "user").ConfigureAwait(false);
            await userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
            
            return RedirectToAction("Index");
        }

        [Route("Manage/DepriveRole/{userId}/{role}")]
        [HttpPost]
        public async Task<IActionResult> DepriveRole(string userId, string role)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            await userManager.RemoveFromRoleAsync(user, role);
            await userManager.AddToRoleAsync(user, "user");

            return RedirectToAction("Index");
        }

        [Route("Manage/DeleteUser/{userId}/{role}")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
