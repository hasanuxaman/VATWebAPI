using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATServer.Library;
using VATViewModel.DTOs;
using WebAPI.Models;

namespace WebAPI.Repo
{
    public class CustomersRepo
    {
        private readonly IMapper _mapper;
        private SysDBInfoVMTemp connVM = new SysDBInfoVMTemp();

        public CustomersRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CustomersModel Insert(CustomersModel vm)
        {
            try
            {
                var _vm = _mapper.Map<CustomersModel, CustomerVM>(vm);
                List<CustomerVM> modelList = new List<CustomerVM>();
                _vm.CustomerCode = vm.Code;
                _vm.Address1 = vm.Address;
                _vm.BranchId = 1;
                _vm.CreatedBy = vm.User;
                _vm.CreatedOn = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss");

                modelList.Add(_vm);

                var result = new ImportDAL().ImportCustomer(modelList, null, null);

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
