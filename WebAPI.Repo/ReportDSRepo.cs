using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;

namespace WebAPI.Repo
{
    public class ReportDSRepo
    {
        public DataSet ComapnyProfileString(string companyId, string userId)
        {
            try
            {
                ReportDSDAL reportDsdal = new ReportDSDAL();
                DataSet ReportResult = reportDsdal.ComapnyProfileString(companyId, userId);

                return ReportResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
