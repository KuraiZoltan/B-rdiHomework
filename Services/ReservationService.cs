using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class ReservationService
    {
        public async Task InitiateReservation(SeatData seat)
        {
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET status='reserved', reservation_time=CURRENT_TIMESTAMP WHERE id=@seats.id;", connection);
            command.Parameters.AddWithValue("@seats.id", seat.Id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
