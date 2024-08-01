using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.PurchaseMasterDetail.ViewModel
{
    public class PurchaseReportVM
    {
        public int InvoiceNumber {  get; set; }
        public DateTime PurchaseDate {  get; set; }
        public string VenderName { get; set; }
        public string ItemName {  get; set; }
        public int Quentity {  get; set; }
        public decimal QuentityPrice {  get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount {  get; set; }

    }
}
