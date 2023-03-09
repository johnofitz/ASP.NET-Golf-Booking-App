using System.ComponentModel.DataAnnotations;

namespace L00177804_Golf.Model
{
    public class Booking
    {
     
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
  
        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public int Handicap { get; set; }
        public TimeOnly BookingTimeOnly { get; set; }

        public DateOnly BookingDateOnly { get; set; }

        [NotMapped]
        public string BookingDate => BookingDateOnly.ToString("dd/MM/yyyy");

        [NotMapped]
        public string BookingTime => BookingTimeOnly.ToString("hh:mm tt");

   



    }
}
