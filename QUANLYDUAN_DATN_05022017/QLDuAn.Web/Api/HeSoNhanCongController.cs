using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Web.Infastructure.Extentions;
using AutoMapper;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hsnc")]
    public class HeSoNhanCongController : BaseController
    {
        private IHeSoNhanCongService _heSoNhanCongService;

        public HeSoNhanCongController(ErrorService errorService, IHeSoNhanCongService heSoNhanCongService) : base(errorService)
        {
            this._heSoNhanCongService = heSoNhanCongService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _heSoNhanCongService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpPost]
        [Route("created")]
        public HttpResponseMessage Created(HttpRequestMessage request, HeSoNhanCongViewModel hesoncVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hsnc = new HeSoNhanCong();
                    hsnc.UpdateNhanCong(hesoncVM);

                    var model = _heSoNhanCongService.Add(hsnc);
                    _heSoNhanCongService.save();
                    var responseData = Mapper.Map<HeSoNhanCong, HeSoNhanCongViewModel>(model);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
            });
        }

        [HttpPut]
        [Route("updated")]
        public HttpResponseMessage Updated(HttpRequestMessage request, HeSoNhanCongViewModel hesoncVM)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hsnc = _heSoNhanCongService.GetById(hesoncVM.Id);
                    hsnc.UpdateNhanCong(hesoncVM);

                    _heSoNhanCongService.Update(hsnc);
                    _heSoNhanCongService.save();
                    return request.CreateResponse(HttpStatusCode.Accepted, hesoncVM);
                }
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _heSoNhanCongService.delete(id);
                _heSoNhanCongService.save();
                return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _heSoNhanCongService.GetById(id);
                var responseData = Mapper.Map<HeSoNhanCong, HeSoNhanCongViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }
    }
}