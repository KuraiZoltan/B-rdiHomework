using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class PaymentService
    {
        public async Task PayForSeat(SeatData seat, string payedBy)
        {
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET reserved_by=@payedBy, status='occupied', purchase_done=1", connection);
            command.Parameters.AddWithValue("@payedBy", payedBy);
            await command.ExecuteNonQueryAsync();
        }
    }
}
