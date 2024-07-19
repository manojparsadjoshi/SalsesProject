using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.PurchaseMasterDetail.ViewModel
{
    public class PurchaseMasterVM
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string ? VendorName { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public List<PurchaseDetailVM> PurchaseDetail { get; set; }
    }
    public class PurchaseDetailVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ? ItemName { get; set; }
        public String Unit { get; set; }
        public int Quentity {  get; set; }
        public decimal Amount { get; set; }
    }
}
