using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;
using VATViewModel.DTOs;

namespace WebAPI.Repo
{
    public class UserInformationRepo
    {
        public List<UserInformationVM> SelectForLogin(LoginVM vm)
        {
            try
            {
                return new UserInformationDAL().SelectForLogin(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
