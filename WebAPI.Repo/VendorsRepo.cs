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
    public class VendorsRepo
    {
        private readonly IMapper _mapper;
        private SysDBInfoVMTemp connVM = new SysDBInfoVMTemp();

        public VendorsRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public VendorsModel Insert(VendorsModel vm)
        {
            try
            {
                var _vm = _mapper.Map<VendorsModel, VendorVM>(vm);
                List<VendorVM> modelList = new List<VendorVM>();
                _vm.VendorCode = vm.Code;
                _vm.Address1 = vm.Address;
                _vm.BranchId = 1;
                _vm.CreatedBy = vm.User;
                _vm.CreatedOn = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss");

                modelList.Add(_vm);

                var result = new ImportDAL().ImportVendor(modelList, null);

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
