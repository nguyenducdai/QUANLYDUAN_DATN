using QLDuAn.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Controllers
{
    public class DuAnController : Controller
    {
        private IDuAnService _duAnService;
        private IThamGiaService _thamgiaService;
        private IHangMucService _hangMucService;

        public DuAnController(DuAnService duAnService ,IThamGiaService thamgiaService, IHangMucService hangMucService)
        {
            this._duAnService = duAnService;
            this._thamgiaService = thamgiaService;
            this._hangMucService = hangMucService;
        }

        // GET: DuAn
        [HttpGet]
        public ActionResult HangMucCongViec()
        {
            if (Request.IsAuthenticated)
            {
               SetViewBag();
            }
            return View();
        }

        public JsonResult GetHangMucCV(int idDuAn)
        {
            var idNhanVien = User.Identity.GetUserId();
            var model = _hangMucService.GetHangMucByIdDuAn(idDuAn);
            var viewMd = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(model);
            foreach (var item in viewMd)
            {
                var thamGia =   _thamgiaService.GetListUserByIdHangMuc(item.ID);
                var thamgiaVm = Mapper.Map<IEnumerable<ThamGia>, IEnumerable<ThamGiaViewModel>>(thamGia);
                item.ThamGia = thamgiaVm;
            }
            var responseData = new JavaScriptSerializer().Serialize(viewMd);
            return Json(new {
                data = responseData,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBag(long? idSelected = null)
        {
            var model = _duAnService.GetDaByIdUser(User.Identity.GetUserId());
            var viewVM = Mapper.Map<IEnumerable<DuAn>, IEnumerable<DuAnViewModel>>(model);
            ViewBag.ID = new SelectList(viewVM, "ID", "TenDuAn", idSelected);

        }

        public ActionResult ThongKe()
        {
            if (Request.IsAuthenticated)
            {
                var model = _duAnService.GetDaByIdUser(User.Identity.GetUserId());
                var viewVM = Mapper.Map<IEnumerable<DuAn>, IEnumerable<DuAnViewModel>>(model);
                ViewBag.ID = new SelectList(viewVM, "ID", "TenDuAn");
            }
            return View();
        }

        public JsonResult ThongKeHangMc(int idDuAn)
        {
            var idNhanVien = User.Identity.GetUserId();
            var model = _hangMucService.GetHangMucByIdDuAnSuccess(idDuAn);
            var viewMd = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(model);
            foreach (var item in viewMd)
            {
                var thamGia = _thamgiaService.GetListUserByIdHangMuc(item.ID , idNhanVien);
                var thamgiaVm = Mapper.Map<IEnumerable<ThamGia>, IEnumerable<ThamGiaViewModel>>(thamGia);
                item.ThamGia = thamgiaVm;
            }
            var responseData = new JavaScriptSerializer().Serialize(viewMd);
            return Json(new
            {
                data = responseData,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}