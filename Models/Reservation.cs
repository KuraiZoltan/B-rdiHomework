using System.ComponentModel.DataAnnotations.Schema;

namespace BárdiHomework.Models
{
    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}