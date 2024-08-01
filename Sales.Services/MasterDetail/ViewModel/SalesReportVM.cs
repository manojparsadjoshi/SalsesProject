using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.MasterDetail.ViewModel
{
    public  class SalesReportVM
    {
        public DateTime SalesDate { get; set; }
        public int InvoiceNumber { get; set; }
        public string ? CustomerName  {  get; set; }
        public string ? ItemName {  get; set; }
        public decimal QuentityPrice {  get; set; }
        public int Quentity {  get; set; }
        public decimal DiscountAmount {  get; set; }
        public decimal QuentityAmount {  get; set; }
        public decimal NetAmount {  get; set; }
        public decimal BillAmount {  get; set; }

    }
}
