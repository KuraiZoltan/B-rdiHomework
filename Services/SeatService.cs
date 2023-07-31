using BárdiHomework.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Configuration;

namespace BárdiHomework.Services
{
    public class SeatService
    {
        private readonly IConfiguration _configuration;
        public SeatService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<SeatData>> GetSeats()
        {
            var seats = new List<SeatData>();
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));

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
                    IsPaid = reader.GetBoolean("purchase_done"),
                    Version = reader.GetInt32("version")
                };
                seats.Add(seat);
            }
            return seats;
        }

        public async Task<IEnumerable<SeatData>> GetSeatsBySeatName(Dictionary<string, int> seatNumbers)
        {
            var seats = new List<SeatData>();
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));

            await connection.OpenAsync();
            foreach (var seatName in seatNumbers)
            {
                using var command = new MySqlCommand($"SELECT * FROM seats WHERE seat_number = '{seatName.Key}';", connection);
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
                        IsPaid = reader.GetBoolean("purchase_done"),
                        Version = reader.GetInt32("version")
                    };
                    seats.Add(seat);
                }
            }
            
            return seats;
        }
    }
}
