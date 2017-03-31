using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Web.Models;
using QLDuAn.Model.Models;
using AutoMapper;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hstg")]
    public class HeSoThoiGianController : BaseController
    {
        private IHeSoThoiGianService _heSoThoiGianService;
        public HeSoThoiGianController(ErrorService errorService, IHeSoThoiGianService heSoThoiGianService) : base(errorService)
        {
            this._heSoThoiGianService = heSoThoiGianService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () => {
                HttpResponseMessage response;
                var model = _heSoThoiGianService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }

   
}
