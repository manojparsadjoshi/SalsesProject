using System.ComponentModel.DataAnnotations;

namespace SalsesProject.Models.VM
{
    public class SalesMasterVM
    {
        [Key]
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public int CustomerId { get; set; }
        public int InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public List<SalesDetailsVM> Sales { get; set; }
    }
    public class SalesDetailsVM
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Unit {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
        public decimal Amount { get; set; }
    }
}
