using BárdiHomework.Models;
using MySqlConnector;

namespace BárdiHomework.Services
{
    public class ReservationService
    {
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var Reservation = new Reservation();
            var reservations = new List<Reservation>();
            using var connection = new MySqlConnection("Server=localhost;User ID=Zolika1022;Password=Zolika1022;Database=seats");

            await connection.OpenAsync();
            using var command = new MySqlCommand("SELECT * FROM reservations;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Reservation reservation = new Reservation() 
                { 
                    ReservationId = reader.GetInt32(0),
                    ReservationStart = (DateTime)reader.GetValue(1),
                    ReservationEnd = (DateTime)reader.GetValue(2)
                };
                Reservation.ReservationStart = (DateTime)reader.GetValue(1);
                Reservation.ReservationEnd = (DateTime)reader.GetValue(2);
                reservations.Add(reservation);
            }
            return reservations;
        }
    }
}
