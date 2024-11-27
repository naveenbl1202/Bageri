using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkaftoBageriWMS.Controllers
{
    public class DashboardController : Controller
    {
        // This action will render the dashboard page after successful login
        [Authorize]  // Ensure only authenticated users can access the dashboard
        public IActionResult Index()
        {
            return View();
        }
    }
}
