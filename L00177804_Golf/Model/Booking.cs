using System.ComponentModel.DataAnnotations.Schema;

namespace L00177804_Golf.Model
{
    public class Booking
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public TimeOnly BookingTimeOnly { get; set; }

        public DateOnly BookingDateOnly { get; set; }

        [NotMapped]
        public string BookingDate => BookingDateOnly.ToString("dd/MM/yyyy");

        [NotMapped]
        public string BookingTime => BookingTimeOnly.ToString("hh:mm tt");

   



    }
}
