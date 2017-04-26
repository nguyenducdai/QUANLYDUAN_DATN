using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hsl")]
    public class HeSoLapController : BaseController
    {
        private IHeSoLapService _heSoLapService;

        public HeSoLapController(ErrorService errorService, IHeSoLapService heSoLapService) : base(errorService)
        {
            this._heSoLapService = heSoLapService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _heSoLapService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpPost]
        [Route("created")]
        public HttpResponseMessage Created(HttpRequestMessage request, HeSoLapViewModel hesolapVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hesl = new HeSoLap();
                    hesl.UpdateHeSoLap(hesolapVM);

                    var model = _heSoLapService.Add(hesl);
                    _heSoLapService.save();
                    var responseData = Mapper.Map<HeSoLap, HeSoLapViewModel>(model);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
            });
        }

        [HttpPut]
        [Route("updated")]
        public HttpResponseMessage Updated(HttpRequestMessage request, HeSoLapViewModel hesolapVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hsl = _heSoLapService.GetById(hesolapVM.Id);
                    hsl.UpdateHeSoLap(hesolapVM);

                    _heSoLapService.Update(hsl);
                    _heSoLapService.save();
                    return request.CreateResponse(HttpStatusCode.Accepted, hesolapVM);
                }
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _heSoLapService.delete(id);
                _heSoLapService.save();
                return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _heSoLapService.GetById(id);
                var responseData = Mapper.Map<HeSoLap, HeSoLapViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }
    }
}