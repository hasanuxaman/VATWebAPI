using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;

namespace WebAPI.Repo
{
    public class CommonRepo
    {
        public bool SuperInformationFileExist(string path)
        {
            try
            {
                return new CommonDAL().SuperInformationFileExist(path);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool LoginSuccess(string DatabaseName)

        {
            try
            {
                return new CommonDAL().LoginSuccess(DatabaseName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
