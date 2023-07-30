using Microsoft.AspNetCore.Mvc;
using BárdiHomework.Services;
using BárdiHomework.Models;
using System.Linq;

namespace BárdiHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeatController : ControllerBase
    {
        private SeatService _seatService;
        private ReservationService _reservationService;
        public SeatController(SeatService seatService, ReservationService reservationService) 
        {
            _seatService = seatService;
            _reservationService = reservationService;
        }
        [HttpGet]
        [Route("getSeats")]
        public async Task<IEnumerable<SeatData>> GetSeats()
        {
            return await _seatService.GetSeats();
        }

        [HttpPost]
        [Route("reserveSeats")]
        public async Task<IActionResult> ReserveSeats([FromBody] Dictionary<string, string> seatNumbers)
        {
            var seats = await _seatService.GetSeatsBySeatName(seatNumbers);
            foreach (var seat in seats)
            {
                if (seat.SeatStatus == "free")
                {
                    await _reservationService.InitiateReservation(seat);
                }
            }
            return Ok(seats);
        }
    }
}
