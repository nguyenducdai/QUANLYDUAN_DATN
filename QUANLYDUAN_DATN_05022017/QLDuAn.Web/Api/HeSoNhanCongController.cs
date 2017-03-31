using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
    }
}