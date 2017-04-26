using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLDuAn.Common;
using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/duan")]
    public class DuAnController : BaseController
    {
        #region
        private IDuAnService _daService;
        private IHangMucService _hangmucService;
        private IThamGiaService _thamGia;

        public DuAnController(
            ErrorService errorService,
            IDuAnService daService,
            IHangMucService hangmucService,
        IThamGiaService thamGia) : base(errorService)
        {
            this._daService = daService;
            this._thamGia = thamGia;
            this._hangmucService = hangmucService;
        }

        #endregion

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string keyword)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _daService.GetAll(keyword);
                var query = model.OrderByDescending(x => x.Created_at).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<DuAn>, IEnumerable<DuAnViewModel>>(query);
                Paginnation<DuAnViewModel> pagination = new Paginnation<DuAnViewModel>
                {
                    items = responseData,
                    Page = page,
                    TotalPage = Convert.ToInt32(Math.Ceiling((double)model.Count() / pageSize)),
                    TotalCount = model.Count()
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagination);
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

                if (ModelState.IsValid)
                {
                    var newDuan = new DuAn();
                    newDuan.UpdateDuAn(duAnVM);
                    newDuan.Updated_at = null;
                    newDuan.NgayHoanThanh = null;
                    var model = _daService.Add(newDuan);
                    _daService.Save();
                    var responseData = Mapper.Map<DuAn, DuAnViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
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
                if (ModelState.IsValid)
                {
                    var duanById = _daService.GetById(duAnVm.ID);
                    duanById.UpdateDuAn(duAnVm);
                    duanById.Updated_at = DateTime.Now;
                    duanById.NgayHoanThanh = null;
                    _daService.Update(duanById);
                    _daService.Save();
                    var reposeData = Mapper.Map<DuAn, DuAnViewModel>(duanById);
                    response = request.CreateResponse(HttpStatusCode.Created, reposeData);
                  
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
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

        [HttpGet]
        [Route("exportexcel")]
        public async Task<HttpResponseMessage> ExportExcel(HttpRequestMessage request, int idDuAn)
        {
            return CreateReponse(request, () =>
            {
                string filename = string.Concat(DateTime.Now.ToString("ddMMyyyyhhmmss") + "aso.xlsx");
                var folderReport = ConfigHelper.GetValueByKey("ReportFolder");
                string path = HttpContext.Current.Server.MapPath(folderReport);

                var pathTemplate = ConfigHelper.GetValueByKey("Template");
                string fullPathTemplateStandad = Path.Combine(HttpContext.Current.Server.MapPath(pathTemplate));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullPath = Path.Combine(path, filename);

                try
                {
                    var duan = _daService.GetAllInfoById(idDuAn);
                    var hangmucGt = _hangmucService.GetHangMucByIdDuAn(idDuAn, 1);
                    var hangmucTT = _hangmucService.GetHangMucByIdDuAn(idDuAn, 0);

                    var thamgiaGt = _thamGia.GetIncomeById(idDuAn,1);
                    var thamgiaTt = _thamGia.GetIncomeById(idDuAn,0);

                    //fullPathTemplateStandad //, new System.IO.FileInfo()
                    using (ExcelPackage ep = new ExcelPackage(new System.IO.FileInfo(fullPath)))
                    {

                        ExcelWorksheet exs = ep.Workbook.Worksheets.Add("GIÁN TIẾP");
                        ExcelWorksheet dexs = ep.Workbook.Worksheets.Add("TRỰC TIẾP");

                        // indirect
                        this.ExcelAction(hangmucGt , thamgiaGt, exs,duan,1);
                        this.ExcelAction(hangmucTT, thamgiaTt,dexs, duan,0);


                        // direct
                        //ExcelWorksheet dexs = ep.Workbook.Worksheets[2];
                        //GenerateInfoProject(duan, dexs, 0);
                        //this.GenerateContent(hangmucTT, dexs);


                        //exs.Cells["B15"].AutoFitColumns(hangmuc);
                        //ExcelCellAddress start = range.Start;
                        //ExcelCellAddress end = range.End;
                        //range.Worksheet.Cells[start.Row, start.Column, end.Row, end.Column].Merge = true;

                        ep.Save();
                    }

                    return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, filename));
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            });
        }

        [HttpGet]
        [Route("income")]
        public HttpResponseMessage GetInComeByIdDa(HttpRequestMessage request, int idDuan, int loaiHm)
        {
            return CreateReponse(request, () =>
            {
                var model = _thamGia.GetIncomeById(idDuan, loaiHm);
                return request.CreateResponse(HttpStatusCode.OK, model);
            });
        } 
        public void GenerateContent(IEnumerable<HangMuc> hangmuc, IEnumerable<ThamGia> thamgia, ExcelWorksheet exs , int loaiDa)
        {
            int stt = 14;
            var totalIcome = 0m;
            // set Header 
            List <HangMuc> hm = new List<HangMuc>();
            foreach (var item in hangmuc)
            {
                hm.Add(item);
            }
         

            for (int i = 0; i < hm.Count(); i++)
            {
                var row = hm[i].ThamGia.Count();
                var rangeA = "A" + stt + ":A" + (stt + row - 1);
                var rangeB = "B" + stt + ":B" + (stt + row - 1);
                var boder = "A" + (stt + row - 1) + ":S" + (stt + row - 1);
                exs.Cells["A13:S" + (stt + row - 1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                exs.Cells["A13:S" + (stt + row - 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                exs.Cells[rangeA].Value = (i + 1);
                exs.Cells[rangeA].Merge = true;
                exs.Cells[boder].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

              //  exs.Cells[rangeB].Merge = true;
                exs.Cells["B" + stt].Value = hm[i].TenHangMuc;
              
                //exs.Cells["B" + stt].Style.VerticalAlignment = ExcelVerticalAlignment.Justify;

                exs.Cells["C" + stt].Value = hm[i].NhomCongViec.NhomCV;
                exs.Cells["D" + stt].Value = Convert.ToInt32(hm[i].HeSoLap.SoNam);
                exs.Cells["E" + stt].Style.Numberformat.Format = "dd/MM/yyyy";
                exs.Cells["E" + stt].Value = hm[i].NgayBatDau;
                exs.Cells["F" + stt].Style.Numberformat.Format = "dd/MM/yyyy";
                exs.Cells["F" + stt].Value = hm[i].NgayHoanThanh;
                exs.Cells["G" + stt].Value = hm[i].ApplicationUser.FullName;

                List<ThamGia> tg = new List<ThamGia>();
                foreach (var item in hm[i].ThamGia)
                {
                    tg.Add(item);
                }
               
                for (int j = 0; j < tg.Count(); j++)
                {
                    var tgStt = stt + j;
                    exs.Cells["H" + tgStt].Value = tg[j].ApplicationUser.FullName;
                    exs.Cells["I" + tgStt].Value = Convert.ToInt32(string.Format("{0:n0}", tg[j].HeSoThamGia));
                    exs.Cells["J" + tgStt].Value = Convert.ToInt32(Math.Round(tg[j].DiemThanhVien));
                    exs.Cells["J" + tgStt].Style.Numberformat.Format = "#,##0";
                    exs.Cells["K" + tgStt].Value = Convert.ToInt32(Math.Round((double)tg[j].ThuNhap));
                    exs.Cells["K" + tgStt].Style.Numberformat.Format = "#,##0";
                    totalIcome = Convert.ToDecimal(totalIcome + tg[j].ThuNhap);
                }
                exs.Cells["L" + stt].Value = hm[i].HeSoLap.Hesl;
                exs.Cells["M" + stt].Value = hm[i].NhomCongViec.HeSoCV;
                exs.Cells["N" + stt].Value = hm[i].SoNguoiThucHien;
                exs.Cells["O" + stt].Value = hm[i].HesoKcn;
                exs.Cells["P" + stt].Value = hm[i].HeSoTg.ThoiGianDk;
                exs.Cells["Q" + stt].Value = hm[i].HeSoTg.HeSoTgdk;
                exs.Cells["R" + stt].Value = hm[i].DiemDanhGia;
                int DiemHM = Convert.ToInt32(hm[i].DiemDanhGia * hm[i].NhomCongViec.HeSoCV * hm[i].HeSoTg.HeSoTgdk * hm[i].HeSoLap.Hesl * hm[i].HesoKcn);
                exs.Cells["S" + stt].Value = DiemHM;
                stt = stt + row;
            }
            exs.Cells["M11"].Value = string.Format("{0:n0}", totalIcome);
            this.GetIncome(exs,thamgia, loaiDa, stt);

        }

        public void GenerateInfoProject(DuAn duan, ExcelWorksheet exs , int loaiDA)
        {
            exs.Cells["C3:M3"].Value = duan.TenDuAn;
            exs.Cells["C4:M4"].Value = duan.KhachHang.TenKhach;
            exs.Cells["C5:G5"].Value = string.Format("{0:n0}", Math.Round((double)duan.GiaTriHopDong));
            exs.Cells["C6:G6"].Value = duan.NgayKetThuc - duan.NgayBatDau;
            exs.Cells["C6:G6"].Value = duan.NgayHoanThanh - duan.NgayBatDau;
            exs.Cells["C8:G8"].Value = duan.SoHopDong;
            exs.Cells["C9:G9"].Value = duan.NgayKy;
            exs.Cells["I5"].Value = duan.LoaiCongTrinh;
            exs.Cells["I6"].Value = duan.TyLeTheoDT.ToString("0") + "%";
            exs.Cells["I8"].Value = string.Format("{0:N0}", duan.TongChiQL) + "%";

            exs.Cells["C3:M3"].Merge = true;
            exs.Cells["C4:M4"].Merge = true;
            exs.Cells["C5:G5"].Merge = true;
            exs.Cells["C6:G6"].Merge = true;
            exs.Cells["C8:G8"].Merge = true;
            exs.Cells["C9:G9"].Merge = true;
            exs.Cells["C9:G9"].Style.Numberformat.Format = "dd/MM/yyyy";
            exs.Cells["A3:S11"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            exs.Cells["A3:S11"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            exs.Cells["A3:S11"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            exs.Cells["A3:S11"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            exs.Cells["C5:G9"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            var k6 = (duan.GiaTriHopDong * duan.TyLeTheoDT) / 100;
            var k8 = k6 - duan.LuongThueNgoai;
            if (loaiDA.Equals(1))
            {
                exs.Cells["I9"].Value = Convert.ToInt32(duan.LuongGTQgt) + " %";
                exs.Cells["I10"].Value = Convert.ToInt32(duan.LuongGTV21) + " %";
                exs.Cells["I11"].Value = Convert.ToInt32(duan.LuongGTV22) + " %";
                var k9 = (k8 * duan.LuongGTQgt) / 100;
                var k10 = (k9 * duan.LuongGTV21) / 100;
                var k11 = (k9 * duan.LuongGTV22) / 100;
                exs.Cells["K6"].Value = string.Format("{0:n0}", Math.Round((double)k6));
                exs.Cells["K7"].Value = string.Format("{0:n0}", Math.Round((double)duan.LuongThueNgoai));
                exs.Cells["K8"].Value = string.Format("{0:n0}", Math.Round((double)k8));
                exs.Cells["K9"].Value = string.Format("{0:n0}", Math.Round((double)k9));
                exs.Cells["K10"].Value = string.Format("{0:n0}", Math.Round((double)k10));
                exs.Cells["K11"].Value = string.Format("{0:n0}", Math.Round((double)k11));
                exs.Cells["P8"].Value = string.Format("{0:n0}", Math.Round((double)duan.TongDiemGT)) + " đ";
                exs.Cells["P9"].Value = string.Format("{0:n0}", Math.Round((double)duan.DonGiaDiemGT)) + " đ";
            }else
            {
                exs.Cells["I9"].Value = Convert.ToInt32(duan.LuongTTQtt) + " %";
                exs.Cells["I10"].Value = Convert.ToInt32(duan.LuongDPQdp) + " %";
                exs.Cells["K6"].Value = string.Format("{0:n0}", Math.Round((double)k6));
                exs.Cells["K7"].Value = string.Format("{0:n0}", Math.Round((double)duan.LuongThueNgoai));

                var QL2 = (k8 * duan.LuongTTQtt) / 100;
                var QL3 = (k8 * duan.LuongDPQdp) / 100;

                exs.Cells["K8"].Value = string.Format("{0:n0}", Math.Round((double)k8));
                exs.Cells["K9"].Value = string.Format("{0:n0}", Math.Round((double)QL2));
                exs.Cells["K10"].Value = string.Format("{0:n0}", Math.Round((double)QL3));

                exs.Cells["P8"].Value = string.Format("{0:n0}", Math.Round((double)duan.TongDiemTT)) + " đ";
                exs.Cells["P9"].Value = string.Format("{0:n0}", Math.Round((double)duan.DonGiaDiemTT)) + " đ";

            }
                       
        }

        public void SetHeader(ExcelWorksheet exs , int loaDA)
        {
            exs.Cells.Style.Font.Name = "Times New Roman";
            exs.Cells.Style.Font.Size = 13;
            var path = HttpContext.Current.Server.MapPath(ConfigHelper.GetValueByKey("Logo"));
            var fullpath = Path.Combine(path);

            var logo = Image.FromFile(fullpath);
            var picture = exs.Drawings.AddPicture("Logo", logo);
            picture.SetSize(150, 70);
            picture.SetPosition(0, 0, 1, 3);
 
            // custom info project
            exs.Cells["A1:C1"].Value = "CÔNG TY CP CƠ ĐIỆN TỬ ASO";
            exs.Cells["G1:N1"].Value = "QUYẾT TOÁN NHÂN CÔNG THỰC HIỆN CÔNG TRÌNH - LƯƠNG V2.2";
            exs.Cells["O1:S1"].Value = "Năm " + DateTime.Now.ToString("yyyy");

            exs.Row(1).Height = 75;
            exs.Cells["A1:C1"].Merge = true;
            exs.Cells["G1:N1"].Merge = true;
            exs.Cells["O1:S1"].Merge = true;
            exs.Cells["G1:S1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            exs.Cells["G1:S1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            exs.Cells["A1:C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            exs.Cells["A1:S1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            exs.Row(1).Style.Font.Bold = true;
            exs.Row(1).Style.Font.Size = 14;

            exs.Cells["B3"].Value = "Tên công trình:";
            exs.Cells["B4"].Value = "Tên khách hàng:";
            exs.Cells["B5"].Value = " Giá trị hợp đồng trước VAT:";
            exs.Cells["B6"].Value = " Số ngày thực hiện theo HĐ:";
            exs.Cells["B7"].Value = " Số ngày thực hiện thực tế:";
            exs.Cells["B7"].Value = " Số HĐ::";
            exs.Cells["B7"].Value = " Ngày ký HĐ";

            exs.Cells["H5"].Value = "Loại công trình:";
            exs.Cells["H6"].Value = "Tỷ lệ theo DT:";
            exs.Cells["H7"].Value = "Lương Thuê ngoài:";
            exs.Cells["H8"].Value = "Tổng chi lương QL:";

            // indirect
            if (loaDA.Equals(1))
            {
                exs.Cells["H9"].Value = "Lương gián tiếp Qgt:";
                exs.Cells["H10"].Value = "Lương gián tiếp V2.1:";
                exs.Cells["H11"].Value = "Lương gián tiếp V2.2:";
                exs.Cells["J8"].Value = "QL";
                exs.Cells["J9"].Value = "QL";
                exs.Cells["J10"].Value = "Qgt";
                exs.Cells["J11"].Value = "Qgt";
            }else
            {
                exs.Cells["H9"].Value = "Lương trực tiếp Qtt:";
                exs.Cells["H10"].Value = "Lương DP Qdp:";
                exs.Cells["J8"].Value = "QL";
                exs.Cells["J9"].Value = "QL";
                exs.Cells["J10"].Value = "QL";
            }

            exs.Cells["N8 : O8"].Value = "Tổng điểm:";
            exs.Cells["N9 : O9"].Value = "Đơn giá điểm :";

            // categories
            exs.Cells["A13"].Value = "Loại CV";
            exs.Cells["B13"].Value = "Hạng mục công việc";
            exs.Cells["C13"].Value = "Nhóm công việc";
            exs.Cells["D13"].Value = "Mức độ truyền thống";
            exs.Cells["E13"].Value = "Ngày thực hiện";
            exs.Cells["F13"].Value = "Ngày yêu cầu hoàn thành";
            exs.Cells["G13"].Value = "Người thực hiện theo lệnh SX";
            exs.Cells["H13"].Value = "Người thực hiện theo thực tế";
            exs.Cells["I13"].Value = "Tỷ lệ % tham gia";
            exs.Cells["J13"].Value = "Điểm thành viên";
            exs.Cells["K13"].Value = "Thu nhập  (VNĐ)";
            exs.Cells["L13"].Value = "Hệ số lặp lại";
            exs.Cells["M13"].Value = "Hệ số công việc Kcv";
            exs.Cells["N13"].Value = "Số người thực hiện";
            exs.Cells["O13"].Value = "Hệ số nhân công Knc";
            exs.Cells["P13"].Value = "Thời gian thực hiện dự kiến (ngày)";
            exs.Cells["Q13"].Value = "Hệ số thời gian Ktg";
            exs.Cells["R13"].Value = "Điểm đánh giá";
            exs.Cells["S13"].Value = "Điểm hạng mục CV";

            var cell13 = exs.Row(13);
            cell13.Style.WrapText = true;
            cell13.Height = 70;
            cell13.Style.Font.Bold = true;
            exs.Cells["A13:S13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            exs.Cells["A13:S13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            exs.Cells["A13:S13"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            exs.Column(2).Width = 50;
            exs.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            exs.Column(2).Style.WrapText = true;
            exs.Column(5).Width = 15;
            exs.Column(6).Width = 15;
            exs.Column(7).Width = 25;
            exs.Column(8).Width = 27;
            exs.Column(10).Width = 15;
            exs.Column(11).Width = 17;
            exs.Column(16).Width = 15;
            exs.Column(18).Width = 10;
            exs.Column(19).Width = 15;

        }

        public void GetIncome(ExcelWorksheet exs,IEnumerable<ThamGia> thamgia, int loaiDA , int stt)
        {
            if (loaiDA.Equals(1))
            {
                var range = "A" + (stt + 1) + ":F" + (stt + 1);
                exs.Cells[range].Value = "LƯƠNG TĂNG THÊM V2.2";
                exs.Cells[range].Merge = true;
                exs.Cells["A" + (stt + 2)].Value = "STT";
                exs.Cells["B" + (stt + 2)].Value = "NỘI DUNG";
                exs.Cells["C" + (stt + 2)].Value = "SỐ ĐIỂM";
                exs.Cells["D" + (stt + 2)].Value = "TỈ LỆ";
                exs.Cells["E" + (stt + 2)].Value = "Tỷ lệ Luong V2.2";
            }
            else
            {
                var range = "A" + (stt + 1) + ":F" + (stt + 1);
                exs.Cells[range].Value = "LƯƠNG TĂNG THÊM V3";
                exs.Cells[range].Merge = true;
                exs.Cells["A" + (stt + 2)].Value = "STT";
                exs.Cells["B" + (stt + 2)].Value = "NỘI DUNG";
                exs.Cells["C" + (stt + 2)].Value = "SỐ ĐIỂM";
                exs.Cells["D" + (stt + 2)].Value = "TỈ LỆ";
                exs.Cells["E" + (stt + 2)].Value = "LƯƠNG V3";
            }
            List<ThamGia> tg = new List<ThamGia>();

            foreach (var item in thamgia)
            {
                tg.Add(item);
            }
            var end = 0;
            var totalPoint = 0m;
            var totalIncome = 0m;
            for (int i = 0; i < tg.Count(); i++)
            {
                totalPoint = totalPoint + tg[i].DiemThanhVien;
                totalIncome = totalIncome + tg[i].DiemThanhVien;
            }
            for (int i = 0; i < tg.Count(); i++)
            {
                var j = (stt+3) + i;
                exs.Cells["A" +j].Value = i+1 ;
                exs.Cells["B" +j].Value = tg[i].ApplicationUser.FullName;
                exs.Cells["C" +j].Value = string.Format("{0:n0}",tg[i].DiemThanhVien);
                exs.Cells["D" +j].Value =  Math.Round((tg[i].DiemThanhVien/totalPoint)*100) +" %";
                exs.Cells["E" +j].Value = string.Format("{0:n0}",tg[i].ThuNhap);
                end = j;
            }
            exs.Cells["B" +(end+1)].Value ="Tổng";
            exs.Cells["C" +(end+1)].Value =string.Format("{0:n0}", totalPoint);
            exs.Cells["D" +(end+1)].Value ="100%";
            exs.Cells["E" +(end+1)].Value = string.Format("{0:n0}", totalIncome);

            exs.Cells["A" + (stt + 1) + ":F" + (end+1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            exs.Cells["A" + (stt + 1) + ":F" + (end+1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            exs.Cells["A" + (stt + 1) + ":F" + (end+1)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            exs.Cells["A" + (stt + 1) + ":F" + (end+1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        public void ExcelAction(IEnumerable<HangMuc> hangmuc, IEnumerable<ThamGia> thamgia, ExcelWorksheet exs, DuAn duan, int loaiDA)
        {
            this.SetHeader(exs, loaiDA);
            this.GenerateInfoProject(duan, exs, loaiDA);
            this.GenerateContent(hangmuc, thamgia, exs , loaiDA);
        }
    }
}