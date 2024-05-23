using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Newtonsoft.Json;
using VATViewModel.DTOs;
using VATWebAPI.Models;
using VATWebAPI.Security;
using WebAPI.Models;
using WebAPI.Repo;

namespace VATWebAPI.Controllers.WebAPI
{
    [RoutePrefix("api/Products"), Authorize]
    public class ProductsController : ApiController
    {
        private readonly IMapper _mapper;
        private AuthHelper _authHelper = new AuthHelper();
        private APIUtilities APIUtilities = new APIUtilities();

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost, Route("Import")]
        public RequestResult Import(productsList vms)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            string userName = claimsIdentity.FindFirst("UserName").Value;
            string bin = claimsIdentity.FindFirst("BIN").Value;
            string Db = claimsIdentity.FindFirst("Db").Value;


           
            RequestResult Model = new RequestResult();

            try
            {
                if (vms.products !=null)
                {
                    LogInModel user = new LogInModel();

                    var modelsWithDifferentCompanyCode = vms.products
              .Where(product => product.BIN != bin)
              .ToList();
                    if (modelsWithDifferentCompanyCode.Count > 0)
                    {
                        throw new Exception("Wrong Company Code Given for this Product Code: " + modelsWithDifferentCompanyCode.FirstOrDefault().Code);
                    }

                    foreach (var product in vms.products)
                    {
                        ProductsRepo Repo = new ProductsRepo(_mapper);
                        var _vm = _mapper.Map<ProductDto, ProductsModel>(product);
                        if (string.IsNullOrEmpty(product.BIN))
                        {
                            throw new Exception("Company Code Not Given for this Product Code: " + product.Code);
                        }
                        else
                        {
                            user.BIN = product.BIN;
                            APIUtilities.Login(user);
                            _vm = Repo.Insert(_vm);
                            if (_vm.Result.Status == ResultStatus.Success)
                            {
                                Model.Message = _vm.Result.Message;
                                Model.StatusCode = HttpStatusCode.Created;
                            }
                            else
                            {
                                if (_vm.Result.Status == ResultStatus.Undefined)
                                {
                                    Elmah.ErrorSignal.FromCurrentContext().Raise(_vm.Result.Exception);
                                }
                                Model.Message = _vm.Result.Message;
                                Model.StatusCode = HttpStatusCode.NotModified;
                            }
                        }

                    }

                }
                else
                {
                    throw new Exception("No data send For Process");

                }


                return Model;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return new RequestResult()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = e.Message
                };
            }
        }





    }
}
