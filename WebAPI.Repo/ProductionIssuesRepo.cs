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
    public class ProductionIssuesRepo
    {
        private readonly IMapper _mapper;
        private SysDBInfoVMTemp connVM = new SysDBInfoVMTemp();

        public ProductionIssuesRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductionIssuesModel Insert(DataTable dt)
        {
            try
            {
                ProductionIssuesModel vm =new ProductionIssuesModel();

                 var result = new APIIntegrationDAL().SaveProductionIssueData(dt,  null);

                if (result[0].ToLower()== "success")
                {
                    vm.Result.Status = ResultStatus.Success;
                    vm.Result.Message = result[1].ToString();

                }
                else
                {
                    vm.Result.Status = ResultStatus.Fail;
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
