using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/applicationrole")]
    public class ApplicationRoleController : BaseController
    {
        private IApplicationRoleService _applicationRoleService;

        public ApplicationRoleController(ErrorService errorService , IApplicationRoleService applicationRoleService) : base(errorService)
        {
            this._applicationRoleService = applicationRoleService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request,()=> {
                var applicationRole = _applicationRoleService.GetAll();
                if(applicationRole == null)
                {
                    return request.CreateResponse(HttpStatusCode.NoContent,"không có dữ liệu");
                }else
                {
                    var responseData = Mapper.Map<IEnumerable<ApplicationRole> , IEnumerable<ApplicationRoleViewModel>>(applicationRole);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
            });
        }
    }
}
