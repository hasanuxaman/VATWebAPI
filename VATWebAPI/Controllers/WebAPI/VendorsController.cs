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
    [RoutePrefix("api/Vendors"), Authorize]
    public class VendorsController : ApiController
    {
        private readonly IMapper _mapper;
        private AuthHelper _authHelper = new AuthHelper();
        private APIUtilities APIUtilities = new APIUtilities();

        public VendorsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost, Route("Import")]
        public RequestResult Import(VendorList vms)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            string userName = claimsIdentity.FindFirst("UserName").Value;
            string bin = claimsIdentity.FindFirst("BIN").Value;
            string Db = claimsIdentity.FindFirst("Db").Value;


           
            RequestResult Model = new RequestResult();

            try
            {
                if (vms.vendors != null)
                {
                    LogInModel user = new LogInModel();

                    var modelsWithDifferentCompanyCode = vms.vendors
              .Where(vendor => vendor.BIN != bin)
              .ToList();
                    if (modelsWithDifferentCompanyCode.Count > 0)
                    {
                        throw new Exception("Wrong Company Code Given for this Vendor Name: " + modelsWithDifferentCompanyCode.FirstOrDefault().VendorName);
                    }

                    foreach (var vendor in vms.vendors)
                    {
                        VendorsRepo Repo = new VendorsRepo(_mapper);
                        var _vm = _mapper.Map<VendorDto, VendorsModel>(vendor);
                        if (string.IsNullOrEmpty(vendor.BIN))
                        {
                            throw new Exception("Company Code Not Given for this Vendor Name: " + vendor.VendorName);
                        }
                        else
                        {
                            user.BIN = vendor.BIN;
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
