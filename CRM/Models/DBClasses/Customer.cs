using System.ComponentModel.DataAnnotations;

namespace CRM.Models.DBClasses
{
    public class Customer
    {
        [Key]
        public int Number { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string SurName { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        [StringLength(10)]
        public string? PostalCode { get; set; }
        [StringLength(100)]
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(100)]
        public string UserId { get; set; }
    }
}
