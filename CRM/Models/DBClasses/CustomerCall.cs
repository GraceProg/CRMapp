using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models.DBClasses
{
    public class CustomerCall
    {
        [Key]
        public Guid Id { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime TimeofCall { get; set; }
        public string? Notes { get; set; }
        [NotMapped]
        public string? CustomerName { get; internal set; }
    }
}
