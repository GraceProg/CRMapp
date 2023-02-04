using System.ComponentModel.DataAnnotations;

namespace CRMapp.Models
{
    public class CustomerCall
    {
        [Key]
        public int Id { get; set; }

        public DateOnly DateOfCall { get; set; }

        public TimeOnly TimeOfCall { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
