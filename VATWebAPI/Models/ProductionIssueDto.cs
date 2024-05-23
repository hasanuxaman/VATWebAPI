using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class ProductionIssueList
    {
        public List<IssuesInvoice> Invoices { get; set; }
    }

    public class IssuesInvoice
    {
        public IssueInvoice Invoice { get; set; }
    }
    public class IssueInvoice
    {

        public string BIN { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string ID { get; set; }
        public string IssueDateTime { get; set; }
        public string ReferenceNo { get; set; }
        public string ReturnId { get; set; }
        public string Post { get; set; }
        public string Comments { get; set; }
        public List<IssueDetsils> Items { get; set; }
    }
    public class IssueDetsils
    {
        public string ItemCode { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
    }


}
