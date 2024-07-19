using SalsesProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sales.Entity
{
    public class PurchaseDetailModel
    {
        public int Id { get; set; }
        public int ItemId {  get; set; }
        [ForeignKey("ItemId")]
        public ItemsModel Item { get; set; }
        public string Unit { get; set; }
        public int Quentity {  get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("PurchaseMasterId")]
        public int PurchaseMasterId {  get; set; }
        [JsonIgnore]

        public PurchaseMasterModel PurchaseMaster { get; set; }
    }
}
