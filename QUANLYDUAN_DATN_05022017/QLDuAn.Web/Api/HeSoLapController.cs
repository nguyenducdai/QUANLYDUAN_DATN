using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Model.Models;
using AutoMapper;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hsl")]
    public class HeSoLapController : BaseController
    {
        private IHeSoLapService _heSoLapService;
        public HeSoLapController(ErrorService errorService , IHeSoLapService heSoLapService) : base(errorService)
        {
            this._heSoLapService = heSoLapService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () => {
                HttpResponseMessage response;
                var model = _heSoLapService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
