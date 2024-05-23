using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
    [RoutePrefix("api/Transfers"), Authorize]
    public class TransfersController : ApiController
    {
        private readonly IMapper _mapper;
        private AuthHelper _authHelper = new AuthHelper();
        private APIUtilities APIUtilities = new APIUtilities();

        public TransfersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost, Route("Import")]
        public RequestResult Import(TransferList vms)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            string userName = claimsIdentity.FindFirst("UserName").Value;
            string bin = claimsIdentity.FindFirst("BIN").Value;
            string Db = claimsIdentity.FindFirst("Db").Value;



            RequestResult Model = new RequestResult();

            try
            {
                DataTable dataTable = new DataTable();  
                if (vms.Invoices != null)
                {
                    LogInModel user = new LogInModel();
                    TransfersModel _vm = new TransfersModel();
                    var modelsWithDifferentCompanyCode = vms.Invoices
              .Where(Invoices => Invoices.Invoice.BIN != bin)
              .ToList();
                    if (modelsWithDifferentCompanyCode.Count > 0)
                    {
                        throw new Exception("Wrong Company Code Given for this Invoice ID: " + modelsWithDifferentCompanyCode.FirstOrDefault().Invoice.ID);
                    }
                    else

                    {
                        TransfersRepo Repo = new TransfersRepo(_mapper);
                        user.BIN = vms.Invoices.FirstOrDefault().Invoice.BIN;

                        APIUtilities.Login(user);

                         dataTable = APIUtilities.TransferConvertToDataTable(vms.Invoices);
                        if (dataTable.Rows.Count > 0)
                        {
                            _vm = Repo.Insert(dataTable);

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
