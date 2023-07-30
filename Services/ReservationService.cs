using BárdiHomework.Models;
using MySqlConnector;
using System.Threading;

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
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");

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
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");
            await connection.OpenAsync();
            using var command = new MySqlCommand("UPDATE seats SET status='free', reserved_by='none', reservation_time=CURRENT_TIMESTAMP WHERE id=@seats.id;", connection);
            command.Parameters.AddWithValue("@seats.id", seat.Id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
