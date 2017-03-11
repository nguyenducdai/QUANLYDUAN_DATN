using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using static QLDuAn.Web.App_Start.IdentityConfig;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/appuser")]
    public class ApplicationUserController : BaseController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserController(ErrorService errorService , ApplicationUserManager userManager) : base(errorService)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, ()=>{
                HttpResponseMessage response;
                var model = _userManager.Users;
                var responseData = Mapper.Map<IEnumerable<ApplicationUser> , IEnumerable<ApplicationUserViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
