using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalsesProject.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }

        [StringLength(10)]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
