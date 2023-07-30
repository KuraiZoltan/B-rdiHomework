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
        private PaymentService _paymentService;
        public SeatController(SeatService seatService, ReservationService reservationService, PaymentService paymentService) 
        {
            _seatService = seatService;
            _reservationService = reservationService;
            _paymentService = paymentService;
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
                    return Ok(seats);
                }
                else
                {
                    return BadRequest("The seat(s) are already occupied.");
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("payForSeats")]
        public async Task PayForSeats([FromBody] Dictionary<string, string> seatNumbers, string payedBy)
        {
            var seats = await _seatService.GetSeatsBySeatName(seatNumbers);
            foreach (var seat in seats)
            {
                await _paymentService.PayForSeat(seat, payedBy);
            }
        }
    }
}
