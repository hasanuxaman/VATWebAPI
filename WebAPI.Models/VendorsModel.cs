using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class VendorsModel
    {
        public VendorsModel()
        {
            Result = new ResultModel();
        }
        public ResultModel Result { get; set; }
        public string BIN { get; set; }
        public string User { get; set; }
        public string Code { get; set; }
        public string VendorName { get; set; }
        public string VendorType { get; set; }
        public string VendorGroup { get; set; }
        public string Address { get; set; }
        public string StartDateTime { get; set; }
        public string VATRegistrationNo { get; set; }
        public string ActiveStatus { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonTelephone { get; set; }
        public string ContactPersonEmail { get; set; }
        public string TIN { get; set; }
        public string Comments { get; set; }
    }
}
