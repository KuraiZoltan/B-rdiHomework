namespace BárdiHomework.Models
{
    public class SeatData
    {
        public int Id { get; set; }
        public string ReservedBy { get; set; }
        public string SeatName { get; set; }
        public string SeatStatus { get; set; }
        public DateTime TimeOfReservation { get; set; }
        public Boolean IsPaid { get; set; }
        public int Version { get; set; }
    }
}
