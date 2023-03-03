using System.ComponentModel.DataAnnotations.Schema;

namespace L00177804_Golf.Model
{
    public class Booking
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BookingDateTime { get; set; }

        [NotMapped]
        public string BookingDate => BookingDateTime.ToString("dd/mm/yy");

        [NotMapped]
        public string BookingTime => BookingDateTime.ToString("hh:mm tt");
    }
}
