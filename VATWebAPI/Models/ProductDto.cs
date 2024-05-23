using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class productsList
    {
        public List<ProductDto> products { get; set; }
    }
    public class ProductDto
    {
       
        public string BIN { get; set; }
        public string User { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string CommercialName { get; set; }
        public string CategoryType { get; set; }
        public string Group { get; set; }
        public string UOM { get; set; }
        public decimal NBRPrice { get; set; }
        public int VATRate { get; set; }
        public decimal TotalPrice { get; set; }
        public int OpeningQuantity { get; set; }
        public int SDRate { get; set; }
        public decimal CostPrice { get; set; }
        public string RefNo { get; set; }
        public string HSCode { get; set; }
        public string OpeningDate { get; set; }
        public string Comments { get; set; }
        public string ActiveStatus { get; set; }
        public string NonStock { get; set; }
        public decimal TollCharge { get; set; }
        public string IsFixedVATRebate { get; set; }
    }

}
