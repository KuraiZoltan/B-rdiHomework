using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class PaymentService
    {
        private readonly IConfiguration _configuration;
        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task PayForSeat(SeatData seat, string payedBy)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET reserved_by=@payedBy, status='occupied', purchase_done=1 WHERE id=@seatId", connection);
            command.Parameters.AddWithValue("@seatId", seat.Id);
            command.Parameters.AddWithValue("@payedBy", payedBy);
            await command.ExecuteNonQueryAsync();
        }
    }
}
