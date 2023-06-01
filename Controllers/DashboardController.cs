using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AZRM2023v1.Controllers
{
    [Authorize]

    public class DashboardController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }
    }
}
