using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalsesProject.Models
{
    public class SalesDetailsModel
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        [JsonIgnore]
        public int ItemId { get; set; }
        public ItemsModel Item { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("SalesMasterId")]
        public int SalesMasterId { get; set; }
        [JsonIgnore]
        public SalesMasterModel SalesMaster { get; set; }
    }
}
