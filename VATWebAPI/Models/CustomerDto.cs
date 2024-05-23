using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class CustomerList
    {
        public List<CustomerDto> customers { get; set; }
    }
    public class CustomerDto
    {

        public string BIN { get; set; }
        public string User { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerGroup { get; set; }
        public string VATRegistrationNo { get; set; }
        public string Address { get; set; }
        public string ActiveStatus { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string StartDateTime { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonTelephone { get; set; }
        public string ContactPersonEmail { get; set; }
        public string TIN { get; set; }
        public string Comments { get; set; }
        public string IsVDSWithHolder { get; set; }
        public string IsInstitution { get; set; }
        public string IsSpecialRate { get; set; }
    }
}
