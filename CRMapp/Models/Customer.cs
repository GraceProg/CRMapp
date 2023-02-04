using System.ComponentModel.DataAnnotations;

namespace CRMapp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public int PostCode { get; set; }

        public string Country { get; set; }

        public DateOnly DateOfBirth { get; set; }

    }
}
