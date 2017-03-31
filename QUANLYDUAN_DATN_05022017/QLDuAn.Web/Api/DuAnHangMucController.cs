using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/duanhangmuc")]
    public class DuAnHangMucController : BaseController
    {
        private IThamGiaService _thamGiaService;
        private IDuAnHangMucService _duAnHangMucService;
        private IHeSoNhanCongService _heSoNhanCongService;

        public DuAnHangMucController(
            ErrorService errorService, 
            IThamGiaService thamGiaService, 
            IDuAnHangMucService duAnHangMucService, 
            IHeSoNhanCongService heSoNhanCongService) : base(errorService)
        {
            this._thamGiaService = thamGiaService;
            this._duAnHangMucService = duAnHangMucService;
            this._heSoNhanCongService = heSoNhanCongService;
        }

        [Route("created")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request, HangMucDuAnViewModel HangMucduAnVM)
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
                    var hangMucDa = new DuAnHangMuc();
                    var HeSoNC = _heSoNhanCongService.GetHeSoKcn(HangMucduAnVM.SoNguoiThucHien);
                    hangMucDa.UpdateDuAnHangMuc(HangMucduAnVM);
                    hangMucDa.HesoKcn = HeSoNC.HeSoNcKcn;
                    _duAnHangMucService.Add(hangMucDa);
                    _duAnHangMucService.Save();

                    if (hangMucDa != null)
                    {
                        List<ThamGia> listTG = new List<ThamGia>();
                        foreach (var item in HangMucduAnVM.ThamGia)
                        {
                            listTG.Add(new ThamGia()
                            {
                                IdDuAn = item.IdDuAn,
                                IdHangMuc = hangMucDa.IdHangMuc,
                                IdNhanVien = item.IdNhanVien,
                                LoaiHangMuc = hangMucDa.LoaiHangMuc,
                                HeSoThamGia = item.HeSoThamGia
                            });
                        }
                        _thamGiaService.Add(listTG, hangMucDa.IdDuAn, hangMucDa.IdHangMuc, hangMucDa.LoaiHangMuc);
                        _thamGiaService.Save();
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }

        [HttpGet]
        [Route("gethangmucduan")]
        public HttpResponseMessage GetHangMucDuAn(HttpRequestMessage request, int id, int LoaiHangMuc)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _duAnHangMucService.GetInfoByIdProject(id, LoaiHangMuc);
                var reposeData = Mapper.Map<IEnumerable<DuAnHangMuc>, IEnumerable<HangMucDuAnViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int IdHangMuc, int IdDuAn, int IdNhomCongViec, int LoaiHangMuc)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var b = _duAnHangMucService.DeleteMediate(IdHangMuc, IdDuAn, IdNhomCongViec, LoaiHangMuc);
                response = request.CreateResponse(HttpStatusCode.Created, b);
                return response;
            });
        }

        [HttpGet]
        [Route("getSingle")]
        public HttpResponseMessage GetSingleById(HttpRequestMessage request, int IdHangMuc, int IdDuAn, int IdNhomCongViec, int LoaiHangMuc)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _duAnHangMucService.GetSingleById(IdHangMuc, IdDuAn, IdNhomCongViec, LoaiHangMuc);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateHangMuc(HttpRequestMessage request, HangMucDuAnViewModel hangMucDuAnMD)
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
                    var hangMucDa = new DuAnHangMuc();
                    hangMucDa.UpdateDuAnHangMuc(hangMucDuAnMD);
                    _duAnHangMucService.Add(hangMucDa);
                    _duAnHangMucService.Save();

                    if (hangMucDa != null)
                    {
                        //var oldTG =  _thamGiaService.GetByIdHm(hangMucDa.IdDuAn, hangMucDa.IdHangMuc, hangMucDa.LoaiHangMuc);
                        // _thamGiaService.delMuti(oldTG, hangMucDa.IdDuAn, hangMucDa.IdHangMuc, hangMucDa.LoaiHangMuc);
                        // _thamGiaService.Save();

                        List<ThamGia> listTG = new List<ThamGia>();
                        foreach (var item in hangMucDuAnMD.ThamGia)
                        {
                            listTG.Add(new ThamGia()
                            {
                                IdDuAn = item.IdDuAn,
                                IdHangMuc = item.IdHangMuc,
                                IdNhanVien = item.IdNhanVien,
                                LoaiHangMuc = item.LoaiHangMuc,
                                HeSoThamGia = item.HeSoThamGia
                            });
                        }
                        _thamGiaService.Add(listTG, hangMucDa.IdDuAn, hangMucDa.IdHangMuc, hangMucDa.LoaiHangMuc);
                        _thamGiaService.Save();
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, "Update success");
                }
                return response;
            });
        }
    }
}