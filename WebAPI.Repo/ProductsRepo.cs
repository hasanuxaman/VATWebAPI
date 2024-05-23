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
    public class ProductsRepo
    {
        private readonly IMapper _mapper;
        private SysDBInfoVMTemp connVM = new SysDBInfoVMTemp();

        public ProductsRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductsModel Insert(ProductsModel vm)
        {
            try
            {
                var _vm = _mapper.Map<ProductsModel, ProductVM>(vm);
                List<ProductVM> modelList = new List<ProductVM>();
                _vm.ProductCode = vm.Code;
                _vm.CategoryName = vm.CategoryType;
                _vm.BranchId = 1;
                _vm.ProductDescription = vm.CommercialName;
                _vm.CreatedBy = vm.User;
                _vm.CreatedOn = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss");

                modelList.Add(_vm);

                var result = new ImportDAL().ImportProduct(modelList, null, null, null, null);

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
