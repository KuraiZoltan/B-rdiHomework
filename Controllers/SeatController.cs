using Microsoft.AspNetCore.Mvc;
using BárdiHomework.Services;
using BárdiHomework.Models;

namespace BárdiHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeatController : ControllerBase
    {
        private SeatService _reservationService;
        public SeatController(SeatService service) 
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
