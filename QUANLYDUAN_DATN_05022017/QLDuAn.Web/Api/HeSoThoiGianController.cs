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
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _heSoThoiGianService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpPost]
        [Route("created")]
        public HttpResponseMessage Created(HttpRequestMessage request, HeSoTgViewModel hesotgVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hstg = new HeSoTg();
                    hstg.UpdateHeSoTg(hesotgVM);

                    var model = _heSoThoiGianService.Add(hstg);
                    _heSoThoiGianService.save();
                    var responseData = Mapper.Map<HeSoTg, HeSoTgViewModel>(model);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
            });
        }

        [HttpPut]
        [Route("updated")]
        public HttpResponseMessage Updated(HttpRequestMessage request, HeSoTgViewModel hesotgVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hstg = _heSoThoiGianService.GetById(hesotgVM.Id);
                    hstg.UpdateHeSoTg(hesotgVM);

                    _heSoThoiGianService.Update(hstg);
                    _heSoThoiGianService.save();
                    return request.CreateResponse(HttpStatusCode.Accepted, hesotgVM);
                }
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _heSoThoiGianService.delete(id);
                _heSoThoiGianService.save();
                return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _heSoThoiGianService.GetById(id);
                var responseData = Mapper.Map<HeSoTg, HeSoTgViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }
    }
}