using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class SeatService
    {
        public async Task<IEnumerable<SeatData>> GetSeats()
        {
            var seats = new List<SeatData>();
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");

            await connection.OpenAsync();
            using var command = new MySqlCommand("SELECT * FROM seats;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SeatData seat = new SeatData() 
                { 
                    Id = (int)reader.GetValue(0),
                    ReservedBy = (string)reader.GetValue(1),
                    SeatStatus = reader.GetString("status"),
                    SeatName = reader.GetString("seat_number"),
                    TimeOfReservation = (DateTime)reader.GetValue(4),
                    IsPaid = reader.GetBoolean("purchase_done")
                };
                seats.Add(seat);
            }
            return seats;
        }
    }
}
