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
    [RoutePrefix("api/nhomcv")]
    public class NhomCongViecController : BaseController
    {
        private INhomCongViecService _nhomCongViecSv;
        public NhomCongViecController(ErrorService errorService, INhomCongViecService nhomCongViecSv) : base(errorService)
        {
            this._nhomCongViecSv = nhomCongViecSv;
        }


        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request) {
            return CreateReponse(request, () => {
                HttpResponseMessage response;
                var model = _nhomCongViecSv.GetAll();
                var responseData = Mapper.Map<IEnumerable<NhomCongViec> , IEnumerable<NhomCongViecViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
    }
}
