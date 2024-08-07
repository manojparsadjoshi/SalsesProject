﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalsesProject.Models
{
    public class SalesMasterModel
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public CustomerModel Customer { get; set; }
        [Required]
        public int InvoiceNumber { get; set; }

        public decimal BillAmount { get; set; }
        [Required]
        public decimal Discount { get; set; }

        public decimal NetAmount { get; set; }
    }
}
