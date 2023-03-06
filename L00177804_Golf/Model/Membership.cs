using System.ComponentModel.DataAnnotations.Schema;

namespace L00177804_Golf.Model
{

    public class Membership
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Gender { get; set; }

        public int Handicap { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
