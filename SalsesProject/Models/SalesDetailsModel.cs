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

        public string Unit { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey("SalesMasterId")]
        public int SalesMasterId { get; set; }
        [JsonIgnore]
        public SalesMasterModel SalesMaster { get; set; }
    }
}
