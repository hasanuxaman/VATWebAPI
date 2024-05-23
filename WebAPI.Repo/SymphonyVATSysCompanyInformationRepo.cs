using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;
using VATViewModel.DTOs;

namespace WebAPI.Repo
{
    public class SymphonyVATSysCompanyInformationRepo
    {
        public List<SymphonyVATSysCompanyInformationVM> SelectAll(string CompanyID = null, string[] conditionFields = null, string[] conditionValues=null)
        {
            try
            {
                return new CompanyInformationDAL().SelectAll(CompanyID, conditionFields, conditionValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
