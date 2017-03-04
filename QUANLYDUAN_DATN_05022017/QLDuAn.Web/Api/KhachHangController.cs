using System.Web.Http;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Service;
using QLDuAn.Web.Models;
using System.Net.Http;
using AutoMapper;
using System.Collections.Generic;
using QLDuAn.Model.Models;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/kh")]
    public class KhachHangController : BaseController
    {
       
        private IKhachHangService _ikhachHangService;
        #region
        public KhachHangController(ErrorService error , IKhachHangService iKhachHangService) :base(error)
        {
            this._ikhachHangService = iKhachHangService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                var model = _ikhachHangService.GetAll();
                var responseData = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<HopDongViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(System.Net.HttpStatusCode.OK, responseData);
                return response;

            });
        }
    }
}