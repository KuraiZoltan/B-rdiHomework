using Microsoft.AspNetCore.Mvc;

namespace BárdiHomework.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
