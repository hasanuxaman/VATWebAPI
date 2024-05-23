using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class SalesList
    {
        public List<SaleInvoice> Invoices { get; set; }
    }

    public class SaleInvoice
    {
        public Invoice Invoice { get; set; }
    }
    public class Invoice
    {

        public string BIN { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string ID { get; set; }
        public string CustomerCode { get; set; }
        public string DeliveryAddress { get; set; }
        public string InvoiceDateTime { get; set; }
        public string ReferenceNo { get; set; }
        public string CurrencyCode { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string SaleType { get; set; }
        public string TransactionType { get; set; }
        public string IsPrint { get; set; }
        public string Post { get; set; }
        public string LCNumber { get; set; }
        public string TenderId { get; set; }
        public string PreviousInvoiceNo { get; set; }
        public string Comments { get; set; }
        public string LCBank { get; set; }
        public string LCDate { get; set; }
        public string PINo { get; set; }
        public string PIDate { get; set; }
        public string BOe { get; set; }
        public string EXPFormDate { get; set; }
        public List<SaleDetsils> Items { get; set; }
    }
    public class SaleDetsils
    {
        public string ItemCode { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal NBRPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SDRate { get; set; }
        public decimal SDAmount { get; set; }
        public decimal VATRate { get; set; }
        public decimal VATAmount { get; set; }
        public string Type { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PromotionalQuantity { get; set; }
        public string VATName { get; set; }
        public string CommercialName { get; set; }
        public string Weight { get; set; }
        public string CPCName { get; set; }
        public string CommentsD { get; set; }
        public string PreviousInvoiceDateTime { get; set; }
        public decimal PreviousNBRPrice { get; set; }
        public decimal PreviousQuantity { get; set; }
        public decimal PreviousSubTotal { get; set; }
        public decimal PreviousSD { get; set; }
        public decimal PreviousSDAmount { get; set; }
        public decimal PreviousVATRate { get; set; }
        public decimal PreviousVATAmount { get; set; }
        public string PreviousUOM { get; set; }
        public string ReasonOfReturn { get; set; }
    }


}
