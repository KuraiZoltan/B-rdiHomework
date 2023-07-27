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
            using var command = new MySqlCommand("SELECT * FROM reservations;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SeatData seat = new SeatData() 
                { 
                    Id = (int)reader.GetValue(0),
                    ReservedBy = (string)reader.GetValue(1),
                    SeatName = (string)reader.GetValue(2),
                    SeatStatus = (string)reader.GetValue(3),
                    TimeOfReservation = (DateTime)reader.GetValue(4),
                    IsPaid = (bool)reader.GetValue(5),
                };
                seats.Add(seat);
            }
            return seats;
        }
    }
}
