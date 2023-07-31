using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class ReservationService
    {
        private readonly IConfiguration _configuration;
        public ReservationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> InitiateReservation(SeatData seat, Dictionary<string, int> seatNumbers)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET status='reserved', reservation_time=CURRENT_TIMESTAMP, version=version+1 WHERE id=@seats.id AND version=@version;", connection);
            command.Parameters.AddWithValue("@seats.id", seat.Id);
            command.Parameters.AddWithValue("@version", seatNumbers[$"{seat.SeatName}"]);
            var output = await command.ExecuteNonQueryAsync();
            return output == 1;
        }

        public async Task ValidatePayment(SeatData seat)
        {
            if (! await CheckForPayment(seat))
            {
                await RemoveReservation(seat);
            }
        }

        private async Task<bool> CheckForPayment(SeatData seat)
        {
            Boolean purchaseDone = false;
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));

            await connection.OpenAsync();
            using var command = new MySqlCommand("SELECT purchase_done FROM seats WHERE id=@seatId;", connection);
            command.Parameters.AddWithValue("seatId", seat.Id);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                purchaseDone = reader.GetBoolean("purchase_done");
            }
            return purchaseDone;
        }

        private async Task RemoveReservation(SeatData seat)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET status='free', reserved_by='none', reservation_time=CURRENT_TIMESTAMP WHERE id=@seats.id;", connection);
            command.Parameters.AddWithValue("@seats.id", seat.Id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
