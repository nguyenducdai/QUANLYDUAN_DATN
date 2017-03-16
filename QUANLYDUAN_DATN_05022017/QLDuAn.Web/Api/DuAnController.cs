using QLDuAn.Web.Infastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Models;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Extentions;
using AutoMapper;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/duan")]
    public class DuAnController : BaseController
    {
        #region
        private IDuAnService _daService;

        public DuAnController(ErrorService errorService, IDuAnService daService) : base(errorService)
        {
            this._daService = daService;
        }
        #endregion

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _daService.GetAll();
                var responseData = Mapper.Map<IEnumerable<DuAn>, IEnumerable<DuAnViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });

        }

        [Route("created")]
        [HttpPost]
        public HttpResponseMessage created(HttpRequestMessage request, DuAnViewModel duAnVM)
        {
            return CreateReponse(request, () =>
            {

                HttpResponseMessage response;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newDuan = new DuAn();
                    newDuan.UpdateDuAn(duAnVM);
                    var model = _daService.Add(newDuan);
                    _daService.Save();
                    var responseData = Mapper.Map<DuAn, DuAnViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });

        }

        [Route("getdetail")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _daService.GetDetail(id);
                var responseData = Mapper.Map<DuAn, DuAnViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                _daService.Delete(id);
                _daService.Save();
                response = request.CreateResponse(HttpStatusCode.Created, id);
                return response;
            });

        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetByIdUpdate(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _daService.GetById(id);
                var reposeData = Mapper.Map<DuAn, DuAnViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, reposeData);
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, DuAnViewModel duAnVm)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                if (!ModelState.IsValid) {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var duanById = _daService.GetById(duAnVm.ID);
                    duanById.UpdateDuAn(duAnVm);
                    _daService.Update(duanById);
                    duanById.Updated_at = DateTime.Now;
                    _daService.Save();
                    var reposeData = Mapper.Map<DuAn, DuAnViewModel>(duanById);
                    response = request.CreateResponse(HttpStatusCode.Created, reposeData);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("getInfo")]
        public HttpResponseMessage GetInfoById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var data = _daService.GetAllInfoById(id);
                response = request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            });

        }
       
    }
}
