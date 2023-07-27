using Microsoft.AspNetCore.Mvc;
using BárdiHomework.Services;
using BárdiHomework.Models;

namespace BárdiHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private ReservationService _reservationService;
        public ReservationController(ReservationService service) 
        {
            _reservationService = service;
        }
        [HttpGet]
        [Route("checkReservation")]
        public async Task<IEnumerable<Reservation>> CheckReservation()
        {
            return await _reservationService.GetReservations();
        }
    }
}
