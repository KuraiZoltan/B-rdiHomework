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
        private EmailService _emailService;
        public SeatController(SeatService seatService, ReservationService reservationService, PaymentService paymentService, EmailService emailService) 
        {
            _seatService = seatService;
            _reservationService = reservationService;
            _paymentService = paymentService;
            _emailService = emailService;
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
                    Task.Run(async () =>
                    {
                        await Task.Delay(120000);
                        await _reservationService.ValidatePayment(seat);
                    });
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
        public async Task<IActionResult> PayForSeats([FromBody] PaymentForm paymentForm)
        {
            var seats = await _seatService.GetSeatsBySeatName(paymentForm.Seats);
            foreach (var seat in seats)
            {
                if (seat.SeatStatus != "free")
                {
                    await _paymentService.PayForSeat(seat, paymentForm.Email);
                    _emailService.SendConfirmationEmail(paymentForm);
                }
                else
                {
                    return BadRequest("Reservation has expired");
                }

            }
            return Ok();
        }
    }
}
