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
using QLDuAn.Web.Infastructure.Extentions;

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

        [HttpPost]
        [Route("created")]
        public HttpResponseMessage Created(HttpRequestMessage request , NhomCongViecViewModel nhomCVVM)
        {
            return CreateReponse(request, () => {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);

                }else
                {
                    var ncv = new NhomCongViec();
                    ncv.UpdateNhomCongViec(nhomCVVM);

                    var model = _nhomCongViecSv.Add(ncv);
                    _nhomCongViecSv.Save();
                    var responseData = Mapper.Map<NhomCongViec, NhomCongViecViewModel>(model);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
            });
        }

        [HttpPut]
        [Route("updated")]
        public HttpResponseMessage Updated(HttpRequestMessage request, NhomCongViecViewModel nhomCVVM)
        {
            return CreateReponse(request, () => {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);

                }
                else
                {
                    var ncv = _nhomCongViecSv.GetById(nhomCVVM.ID);
                    ncv.UpdateNhomCongViec(nhomCVVM);

                    _nhomCongViecSv.Update(ncv);
                    _nhomCongViecSv.Save();
                    return request.CreateResponse(HttpStatusCode.Accepted, nhomCVVM);
                }
            });
        }


        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () => {
                    _nhomCongViecSv.Delete(id);
                    _nhomCongViecSv.Save();
                    return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () => {
               var model =  _nhomCongViecSv.GetById(id);
                var responseData = Mapper.Map<NhomCongViec, NhomCongViecViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }
    }
}
