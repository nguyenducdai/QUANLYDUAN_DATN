using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;
using AutoMapper;
namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/applicationgroup")]
    [Authorize]
    public class ApplicationGroupController : BaseController
    {
        private IApplicationGroupService _applicationGroupService;

        public ApplicationGroupController(ErrorService errorService , IApplicationGroupService applicationGroupService) : base(errorService)
        {
            this._applicationGroupService = applicationGroupService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request , ()=> {
                HttpResponseMessage response;
                 var ApplicationGroup = _applicationGroupService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(ApplicationGroup);
                response = request.CreateResponse(HttpStatusCode.OK,responseData);
                return response;
            });
        }
    }
}
