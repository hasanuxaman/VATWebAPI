using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;
using VATViewModel.DTOs;
using WebAPI.Models;

namespace WebAPI.Repo
{
    public class SalesRepo
    {
        private readonly IMapper _mapper;
        private SysDBInfoVMTemp connVM = new SysDBInfoVMTemp();

        public SalesRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SalesModel Insert(DataTable dt)
        {
            try
            {
                SalesModel vm =new SalesModel();

                 var result = new APIIntegrationDAL().SaveSaleData(dt,  null);

                if (result[0].ToLower()== "success")
                {
                    vm.Result.Status = ResultStatus.Success;
                    vm.Result.Message = result[1].ToString();

                }
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
