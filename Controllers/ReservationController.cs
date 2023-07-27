using Microsoft.AspNetCore.Mvc;

namespace BárdiHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        [Route("checkReservation")]
        public string CheckReservation()
        {
            return "working";
        }
    }
}
