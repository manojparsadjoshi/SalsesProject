using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sales.Entity
{
    public  class PurchaseMasterModel
    {
        public int Id { get; set; }
        public int VenderId {  get; set; }
        [ForeignKey("VenderId")]
        [JsonIgnore]

        public VenderModel Vender { get; set; }
        public int InvoiceNumber {  get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount {  get; set; }
        public decimal NetAmount { get; set; }

    }
}
