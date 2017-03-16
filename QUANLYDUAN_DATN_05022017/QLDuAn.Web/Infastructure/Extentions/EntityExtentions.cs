using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Infastructure.Extentions
{
    public static class EntityExtentions
    {
        public static void UpdateHangMuc(this HangMuc hangMuc, HangMucViewModel hangMucVM)
        {
            hangMuc.ID = hangMucVM.ID;
            hangMuc.TenHangMuc = hangMucVM.TenHangMuc;
            hangMuc.MoTaHangMuc = hangMucVM.MoTaHangMuc;
            hangMuc.LoaiHangMuc = hangMucVM.LoaiHangMuc;
            hangMuc.TrangThai = hangMucVM.TrangThai;
            hangMuc.Created_at = hangMucVM.Created_at;
            hangMuc.Updated_at = hangMucVM.Updated_at;
        }

        public static void UpdateHopDong(this HopDong hopDong, HopDongViewModel hopDongVM)
        {
            hopDong.ID = hopDongVM.ID;
            hopDong.SoHopDong = hopDongVM.SoHopDong;
            hopDong.TenHopDong = hopDongVM.TenHopDong;
            hopDong.GiaTriHopDong = hopDongVM.GiaTriHopDong;
            hopDong.SoNgayThucHien = hopDongVM.SoNgayThucHien;
            hopDong.NoiDung = hopDongVM.NoiDung;
            hopDong.NgayBatDau = hopDongVM.NgayBatDau;
            hopDong.NgayKetThuc = hopDongVM.NgayKetThuc;
            hopDong.NgayKy = hopDongVM.NgayKy;
            hopDong.IdKhachHang = hopDongVM.IdKhachHang;
            hopDong.Created_at = hopDongVM.Created_at;
            hopDong.Updated_at = hopDongVM.Updated_at;
        }

        public static void UpdateKhachHang(this KhachHang khachHang, KhachHangViewModel khachHangVM)
        {
            khachHang.ID = khachHangVM.ID;
            khachHang.TenKhach = khachHang.TenKhach;
            khachHang.SoDienThoai = khachHang.SoDienThoai;
            khachHang.SoFax = khachHang.SoFax;
            khachHang.Email = khachHang.Email;
            khachHang.DiaChi = khachHang.DiaChi;
            khachHang.GioiTinh = khachHang.GioiTinh;
        }

        public static void UpdateDuAn(this DuAn duAn, DuAnViewModel duAnVM)
        {
            duAn.ID = duAnVM.ID;
            duAn.TenDuAn = duAnVM.TenDuAn;
            duAn.IdHopDong = duAnVM.IdHopDong;
            duAn.MoTa = duAnVM.MoTa;
            duAn.SoNgayThucHienThucTe = duAnVM.SoNgayThucHienThucTe;
            duAn.NamQuyetToan = duAnVM.NamQuyetToan;
            duAn.LoaiCongTrinh = duAnVM.LoaiCongTrinh;
            duAn.TyLeTheoDT = duAnVM.TyLeTheoDT;
            duAn.TongDiem = duAnVM.TongDiem;
            duAn.DonGiaDiemDiem = duAnVM.DonGiaDiemDiem;
            duAn.LuongThueNgoai = duAnVM.LuongThueNgoai;
            duAn.TongChiQL = duAnVM.TongChiQL;
            duAn.LuongTTQtt = duAnVM.LuongTTQtt;
            duAn.LuongDPQdp = duAnVM.LuongDPQdp;
            duAn.LuongGTQgt = duAnVM.LuongGTQgt;
            duAn.LuongGTV21 = duAnVM.LuongGTV21;
            duAn.LuongGTV22 = duAnVM.LuongGTV22;
            duAn.TrangThai = duAnVM.TrangThai;
            duAn.Created_at = duAnVM.Created_at;
            duAn.Updated_at = duAnVM.Updated_at;
        }

        public static void UpdateDuAnHangMuc(this DuAnHangMuc duAnHangMuc, HangMucDuAnViewModel hangMucDuAnVM)
        {
            duAnHangMuc.IdDuAn = hangMucDuAnVM.IdDuAn;
            duAnHangMuc.IdHangMuc = hangMucDuAnVM.IdHangMuc;
            duAnHangMuc.IdNhomCongViec = hangMucDuAnVM.IdNhomCongViec;
            duAnHangMuc.MucDoTruyenThong = hangMucDuAnVM.MucDoTruyenThong;
            duAnHangMuc.NgayBatDau = hangMucDuAnVM.NgayBatDau;
            duAnHangMuc.NgayDuKienKetThuc = hangMucDuAnVM.NgayDuKienKetThuc;
            duAnHangMuc.NgayHoanThanh = hangMucDuAnVM.NgayHoanThanh;
            duAnHangMuc.IdNguoiThucHienTheoLenhSX = hangMucDuAnVM.IdNguoiThucHienTheoLenhSX;
            duAnHangMuc.HeSoLapLai = hangMucDuAnVM.HeSoLapLai;
            duAnHangMuc.HeSoNhanCong = hangMucDuAnVM.HeSoNhanCong;
            duAnHangMuc.HeSoCongViec = hangMucDuAnVM.HeSoCongViec;
            duAnHangMuc.SoNguoiThucHien = hangMucDuAnVM.SoNguoiThucHien;
            duAnHangMuc.HeSoThoiGian = hangMucDuAnVM.HeSoThoiGian;
            duAnHangMuc.DiemDanhGia = hangMucDuAnVM.DiemDanhGia;
            duAnHangMuc.DiemHangMuc = hangMucDuAnVM.DiemHangMuc;
            duAnHangMuc.LoaiHangMuc = hangMucDuAnVM.LoaiHangMuc;
            duAnHangMuc.TrangThai = hangMucDuAnVM.TrangThai;
            duAnHangMuc.Created_at = hangMucDuAnVM.Created_at;
            duAnHangMuc.Updated_at = hangMucDuAnVM.Updated_at;
            //duAnHangMuc.TrangThai = hangMucDuAnVM.TrangThai;
        }

        public static void UpdateThamGia(this ThamGia thamGia, ThamGiaViewModel thamGiaVM)
        {
            thamGia.IdDuAn = thamGiaVM.IdDuAn;
            thamGia.IdHangMuc = thamGiaVM.IdHangMuc;
            thamGia.IdNhanVien = thamGiaVM.IdNhanVien;
            thamGia.HeSoThamGia = thamGiaVM.HeSoThamGia;
            thamGia.ThuNhap = thamGiaVM.ThuNhap;
            thamGia.DiemThanhVien = thamGiaVM.DiemThanhVien;
        }
    }
}