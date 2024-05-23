using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProductionIssuesModel
    {
        public ProductionIssuesModel()
        {
            Result = new ResultModel();
        }
        public ResultModel Result { get; set; }
        public string BIN { get; set; }
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string ID { get; set; }
        public string IssueDateTime { get; set; }
        public string ReferenceNo { get; set; }
        public string ReturnId { get; set; }
        public string Post { get; set; }
        public string Comments { get; set; }
        public List<IssueItem> Items { get; set; }
    }
    public class IssueItem
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
