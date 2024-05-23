using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class TransferList
    {
        public List<TransfersInvoice> Invoices { get; set; }
    }

    public class TransfersInvoice
    {
        public TransferInvoice Invoice { get; set; }
    }
    public class TransferInvoice
    {

        public string BIN { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string ID { get; set; }
        public string TransferToBranchCode { get; set; }
        public string TransactionDateTime { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionType { get; set; }
        public string Post { get; set; }
        public string Comments { get; set; }
        public List<TransferDetsils> Items { get; set; }
    }
    public class TransferDetsils
    {
        public string ItemCode { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal VATRate { get; set; }
        public string Weight { get; set; }
        public string CommentsD { get; set; }
    }


}
