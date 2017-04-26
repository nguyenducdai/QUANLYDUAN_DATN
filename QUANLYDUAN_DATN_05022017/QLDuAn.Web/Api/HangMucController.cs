using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using System.Linq;
using System;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hm")]
    [Authorize]
    public class HangMucController : BaseController
    {
        #region
        private IHangMucService _hangMucService;
        private IHeSoNhanCongService _heSoNhanCongService;
        private IThamGiaService _thamGiaService;
        private IDuAnService _duAnService;


        public HangMucController(
            IHangMucService hangMucService,
            IHeSoNhanCongService heSoNhanCongService,
            IThamGiaService thamGiaService,
            IDuAnService duAnService,
            ErrorService error) : base(error)
        {
            this._hangMucService = hangMucService;
            this._heSoNhanCongService = heSoNhanCongService;
            this._thamGiaService = thamGiaService;
            this._duAnService = duAnService;
        }

        #endregion
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string keyword)
        {
            return CreateReponse(request, () =>
            {
                var model = _hangMucService.getAll(keyword);
                var query = model.OrderByDescending(x => x.Created_at).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(query);
                Paginnation<HangMucViewModel> pagination = new Paginnation<HangMucViewModel>
                {
                    items = responseData,
                    Page = page,
                    TotalPage = Convert.ToInt32(Math.Ceiling((decimal)model.Count() / pageSize)),
                    TotalCount = model.Count()
                };

                return request.CreateResponse(HttpStatusCode.OK, pagination);
            });
        }


        [Route("gethangmucduan")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string keyword, int idDuAn, int LoaiHm , bool? filter)
        {
            return CreateReponse(request, () =>
            {
                var model = _hangMucService.GetHangMucDuAn(idDuAn, LoaiHm, keyword , filter);
                var query = model.Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(query);
                Paginnation<HangMucViewModel> pagination = new Paginnation<HangMucViewModel>
                {
                    items = responseData,
                    Page = page,
                    TotalPage = Convert.ToInt32(Math.Ceiling((decimal)model.Count() / pageSize)),
                    TotalCount = model.Count()
                };

                return request.CreateResponse(HttpStatusCode.OK, pagination);
            });
        }



        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _hangMucService.getByID(id);
                var responseData = Mapper.Map<HangMuc, HangMucViewModel>(model);
                HttpResponseMessage respose = request.CreateResponse(HttpStatusCode.OK, responseData);
                return respose;
            });
        }

        [Route("created")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request, HangMucViewModel hangMucViewModel)
        {
            return CreateReponse(request, () =>
            {
                if (!ModelState.IsValid)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hangMuc = new HangMuc();
                    var HeSoNC = _heSoNhanCongService.GetHeSoKcn(hangMucViewModel.SoNguoiThucHien);
                    hangMuc.UpdateHangMuc(hangMucViewModel);
                    hangMuc.HesoKcn = HeSoNC.HeSoNcKcn;
                    var hangMucResponse = _hangMucService.Add(hangMuc);
                    _hangMucService.save();
                    if (hangMucResponse != null)
                    {
                        var donGiaDiemTT = 0;
                        var donGiaDiemGT = 0;
                        var hm = _hangMucService.GetHangMucById(hangMucResponse.ID);

                        decimal? diemHm = 0m;
                        if (hm.HeSoLap != null && hm.HeSoTg != null)
                        {
                            diemHm = hm.DiemDanhGia * hm.HeSoLap.Hesl * hm.HeSoTg.HeSoTgdk * hm.HesoKcn * hm.NhomCongViec.HeSoCV;
                        }

                        if (hangMucViewModel.ThamGia.Count() > 0)
                        { 

                        List<ThamGia> listTG = new List<ThamGia>();
                        foreach (var item in hangMucViewModel.ThamGia)
                        {
                            if (hangMucResponse.LoaiHangMuc == 0)
                            {
                                listTG.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMucResponse.ID,
                                    IdDuAn = hangMucResponse.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia) ?? 0,
                                    ThuNhap = donGiaDiemTT * diemHm * item.HeSoThamGia
                                });
                            }
                            else
                            {
                                listTG.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMucResponse.ID,
                                    IdDuAn = hangMuc.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia) ?? 0,
                                    ThuNhap = donGiaDiemGT * diemHm * item.HeSoThamGia
                                });
                            }

                        }
                        _thamGiaService.Add(listTG, hangMucResponse.ID, hangMucResponse.LoaiHangMuc);
                        _thamGiaService.Save();

                        var duan = _duAnService.GetAllInfoById(hangMuc.IdDuAn);
                        var point = _thamGiaService.TotalPoint(hangMuc.IdDuAn, hangMuc.LoaiHangMuc);

                        //tính đơn giá điểm trục tiếp
                        var q0 = (duan.GiaTriHopDong * duan.TyLeTheoDT) / 100;
                        var q1 = q0 - duan.LuongThueNgoai;
                        var q2 = (q1 * duan.LuongTTQtt) / 100;

                        if (point != 0)
                        {
                            donGiaDiemTT = Convert.ToInt32(q2 / point);
                        }

                        // tính đơn giá điểm gián tiếp
                        var g0 = (duan.GiaTriHopDong * duan.TyLeTheoDT) / 100;
                        var g1 = g0 - duan.LuongThueNgoai;
                        var g2 = (g1 * duan.LuongGTQgt) / 100;
                        var g3 = (g2 * duan.LuongGTV22) / 100;

                        if (point != 0)
                        {
                            donGiaDiemGT = Convert.ToInt32(g3 / point);
                        }

                        if (hangMuc.LoaiHangMuc == 0)
                        {
                            duan.TongDiemTT = point;
                            duan.DonGiaDiemTT = donGiaDiemTT;
                            _duAnService.Update(duan);
                        }
                        else
                        {
                            duan.TongDiemGT = point;
                            duan.DonGiaDiemGT = donGiaDiemGT;
                            _duAnService.Update(duan);
                        }

                        var Tg = _thamGiaService.GetByIdHm(hangMucResponse.ID, hangMucResponse.LoaiHangMuc);
                        List<ThamGia> listTGUp = new List<ThamGia>();
                        foreach (var item in Tg)
                        {
                            if (hangMucResponse.LoaiHangMuc == 0)
                            {
                                listTGUp.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMucResponse.ID,
                                    IdDuAn = hangMucResponse.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia) ?? 0,
                                    ThuNhap = donGiaDiemTT * diemHm * item.HeSoThamGia
                                });
                            }
                            else
                            {
                                listTGUp.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMucResponse.ID,
                                    IdDuAn = hangMucResponse.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia) ?? 0,
                                    ThuNhap = donGiaDiemGT * diemHm * item.HeSoThamGia
                                });
                            }

                        }
                        _thamGiaService.Add(listTGUp, hangMucResponse.ID, hangMucResponse.LoaiHangMuc);
                        _thamGiaService.Save();
                        }
                    }
                   return request.CreateResponse(HttpStatusCode.Created, hangMucResponse);
                }
            });
        }


        [Route("updated")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, HangMucViewModel hangMucViewModel)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage respose = null;
                if (!ModelState.IsValid)
                {

                    respose = request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }
                else
                {
                    var hangMuc = _hangMucService.getByID(hangMucViewModel.ID);
                    var HeSoNC = _heSoNhanCongService.GetHeSoKcn(hangMucViewModel.SoNguoiThucHien);
                    hangMuc.UpdateHangMuc(hangMucViewModel);
                    hangMuc.HesoKcn = HeSoNC.HeSoNcKcn;
                    _hangMucService.Update(hangMuc);
                    _hangMucService.save();
                    if (hangMuc != null)
                    {
                        var duan = _duAnService.GetAllInfoById(hangMuc.IdDuAn);
                        var point = _thamGiaService.TotalPoint(hangMuc.IdDuAn, hangMuc.LoaiHangMuc);
                        decimal? donGiaDiemTT = 0m;
                        decimal? donGiaDiemGT = 0m;
                        if (point > 0) { 
                            if (hangMuc.LoaiHangMuc == 0)
                            {
                                //tính đơn giá điểm trục tiếp
                                var q0 = (duan.GiaTriHopDong * duan.TyLeTheoDT) / 100;
                                var q1 = q0 - duan.LuongThueNgoai;
                                var q2 = (q1 * duan.LuongTTQtt) / 100;
                                donGiaDiemTT = q2 / point;
                                duan.TongDiemTT = point;
                                duan.DonGiaDiemTT = donGiaDiemTT;
                                _duAnService.Update(duan);
                            }
                            else
                            {
                                // tính đơn giá điểm gián tiếp
                                var g0 = (duan.GiaTriHopDong * duan.TyLeTheoDT) / 100;
                                var g1 = g0 - duan.LuongThueNgoai;
                                var g2 = (g1 * duan.LuongGTQgt) / 100;
                                var g3 = (g2 * duan.LuongGTV22) / 100;
                                donGiaDiemGT = g3 / point;
                                duan.TongDiemGT = point;
                                duan.DonGiaDiemGT = donGiaDiemGT;
                                _duAnService.Update(duan);
                            }
                            _duAnService.Save();
                          }
                        var hm = _hangMucService.GetHangMucById(hangMuc.ID);
                        decimal? diemHm = 0m;
                        if (hm.HeSoLap != null && hm.HeSoTg != null)
                        {
                            diemHm = hm.DiemDanhGia * hm.HeSoLap.Hesl * hm.HeSoTg.HeSoTgdk * hm.HesoKcn * hm.NhomCongViec.HeSoCV;
                        }
                        List<ThamGia> listTG = new List<ThamGia>();
                        if(hangMucViewModel.ThamGia.Count() > 0) { 
                        foreach (var item in hangMucViewModel.ThamGia)
                        {
                            if (hangMuc.LoaiHangMuc == 0)
                            {
                                listTG.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMuc.ID,
                                    IdDuAn = hangMuc.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia)??0,
                                    ThuNhap = donGiaDiemTT * diemHm * item.HeSoThamGia
                                });
                            }
                            else
                            {
                                listTG.Add(new ThamGia()
                                {
                                    IdHangMuc = hangMuc.ID,
                                    IdDuAn = hangMuc.IdDuAn,
                                    IdNhanVien = item.IdNhanVien,
                                    HeSoThamGia = item.HeSoThamGia,
                                    LoaiHangMuc = item.LoaiHangMuc,
                                    DiemThanhVien = (diemHm * item.HeSoThamGia)??0,
                                    ThuNhap = donGiaDiemGT * diemHm * item.HeSoThamGia
                                });
                            }
                        }
                        _thamGiaService.Add(listTG, hangMuc.ID, hangMuc.LoaiHangMuc);
                        }
                    }
                    _hangMucService.save();
                    respose = request.CreateResponse(HttpStatusCode.Accepted, hangMucViewModel);
                }

                return respose;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _hangMucService.Delete(id);
                _hangMucService.save();
                HttpResponseMessage respose = request.CreateResponse(HttpStatusCode.OK, id); ;
                return respose;
            });
        }

        [Route("getHangMucById")]
        [HttpGet]
        public HttpResponseMessage GetHangMucById(HttpRequestMessage request, int idHangMuc)
        {
            return CreateReponse(request, () =>
            {
                var model = _hangMucService.GetHangMucById(idHangMuc);
                var responseData = Mapper.Map<HangMuc, HangMucViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }


        [Route("updatestatus")]
        [HttpGet]
        public HttpResponseMessage UpdateStatus(HttpRequestMessage request, int id , bool status)
        {
            return CreateReponse(request, () => {

                var oldHangMuc = _hangMucService.getByID(id);
                oldHangMuc.TrangThai = status;
                _hangMucService.Update(oldHangMuc);
                _hangMucService.save();
                return request.CreateResponse(HttpStatusCode.Accepted , id);
            });

        }


    }
}