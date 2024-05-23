using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class PurchaseList
    {
        public List<PurchasesInvoice> Invoices { get; set; }
    }

    public class PurchasesInvoice
    {
        public PurchaseInvoice Invoice { get; set; }
    }
    public class PurchaseInvoice
    {

        public string BIN { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string ID { get; set; }
        public string VendorCode { get; set; }
        public string InvoiceDate { get; set; }
        public string ReceiveDate { get; set; }
        public string RebateDate { get; set; }
        public string IsRebate { get; set; }
        public string LCNo { get; set; }
        public string BENumber { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionType { get; set; }
        public string WithVDS { get; set; }
        public string Post { get; set; }
        public string Comments { get; set; }
        public List<PurchaseDetsils> Items { get; set; }
    }
    public class PurchaseDetsils
    {
        public string ItemCode { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Type { get; set; }
        public decimal CDAmount { get; set; }
        public decimal RDAmount { get; set; }
        public decimal SDAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal ATAmount { get; set; }
        public decimal AITAmount { get; set; }
        public decimal OthersAmount { get; set; }
        public string Remarks { get; set; }
    }


}
