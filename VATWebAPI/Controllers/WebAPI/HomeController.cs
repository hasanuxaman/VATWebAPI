using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Newtonsoft.Json;
using VATWebAPI.Models;

namespace VATWebAPI.Controllers.WebAPI
{
    [RoutePrefix("api/Home"), Authorize]
    public class HomeController : ApiController
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [Route("ErrorTest"), ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult ErrorTest()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return BadRequest();
            }
        }


        
        [Route("GetUserInfo"), ApiExplorerSettings(IgnoreApi = true)]
        public UserDto GetUserInfo()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;

            Dictionary<string, string> claims = identity.Claims.ToDictionary(identityClaim => identityClaim.Type, identityClaim => identityClaim.Value);

            UserModel userModel = new UserModel
            {
                FullName = identity.Name,
                UserId = identity.AuthenticationType,
                Claims = claims
            };

            return _mapper.Map<UserModel, UserDto>(userModel);
        }

        //[Route("GetUserNames")]
        //public List<UserDto> GetUserNames()
        //{
        //    COAAPI coaAPI = new COAAPI();

        //    var result = coaAPI.GetNames(JsonConvert.SerializeObject(new { }));


        //    List<UserDto> users = new List<UserDto>();
        //    users.Add(new UserDto
        //    {
        //        FullName = "x",
        //        UserId = "10"
        //    });
        //    users.Add(new UserDto
        //    {
        //        FullName = "a",
        //        UserId = "11"
        //    });

        //    return users;
        //}


    }
}
