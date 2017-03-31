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
            khachHang.TenKhach = khachHangVM.TenKhach;
            khachHang.SoDienThoai = khachHangVM.SoDienThoai;
            khachHang.SoFax = khachHangVM.SoFax;
            khachHang.Email = khachHangVM.Email;
            khachHang.DiaChi = khachHangVM.DiaChi;
            khachHang.GioiTinh = khachHangVM.GioiTinh;
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
            duAnHangMuc.IdMucDoTruyenThong = hangMucDuAnVM.IdMucDoTruyenThong;
            duAnHangMuc.NgayBatDau = hangMucDuAnVM.NgayBatDau;
            duAnHangMuc.ThoiGianDuKien = hangMucDuAnVM.ThoiGianDuKien;
            duAnHangMuc.NgayHoanThanh = hangMucDuAnVM.NgayHoanThanh;
            duAnHangMuc.IdNguoiThucHienTheoLenhSX = hangMucDuAnVM.IdNguoiThucHienTheoLenhSX;
            duAnHangMuc.SoNguoiThucHien = hangMucDuAnVM.SoNguoiThucHien;
            duAnHangMuc.DiemDanhGia = hangMucDuAnVM.DiemDanhGia;
            duAnHangMuc.HesoKcn = hangMucDuAnVM.HesoKcn;
            duAnHangMuc.LoaiHangMuc = hangMucDuAnVM.LoaiHangMuc;
            duAnHangMuc.TrangThai = hangMucDuAnVM.TrangThai;
            duAnHangMuc.Created_at = hangMucDuAnVM.Created_at;
            duAnHangMuc.Updated_at = hangMucDuAnVM.Updated_at;
        }

        public static void UpdateThamGia(this ThamGia thamGia, ThamGiaViewModel thamGiaVM)
        {
            thamGia.IdDuAn = thamGiaVM.IdDuAn;
            thamGia.IdHangMuc = thamGiaVM.IdHangMuc;
            thamGia.IdNhanVien = thamGiaVM.IdNhanVien;
            thamGia.LoaiHangMuc = thamGiaVM.LoaiHangMuc;
            thamGia.HeSoThamGia = thamGiaVM.HeSoThamGia;
            thamGia.ThuNhap = thamGiaVM.ThuNhap;
            thamGia.DiemThanhVien = thamGiaVM.DiemThanhVien;
        }


        public static void UpdateApplicationUser(this ApplicationUser applicationUser, ApplicationUserViewModel applicationUserVM)
        {
            applicationUser.Id = applicationUserVM.Id;
            applicationUser.AccessFailedCount = applicationUserVM.AccessFailedCount;
            applicationUser.Adress = applicationUserVM.Adress;
            applicationUser.Sex = applicationUserVM.Sex;
            applicationUser.Bio = applicationUserVM.Bio;
            applicationUser.FullName = applicationUserVM.FullName;
            applicationUser.Email = applicationUserVM.Email;
            applicationUser.EmailConfirmed = applicationUserVM.EmailConfirmed;
            applicationUser.LockoutEnabled = applicationUserVM.LockoutEnabled;
            applicationUser.LockoutEndDateUtc = applicationUserVM.LockoutEndDateUtc;
            applicationUser.PasswordHash = applicationUserVM.PasswordHash;
            applicationUser.PhoneNumber = applicationUserVM.PhoneNumber;
            applicationUser.PhoneNumberConfirmed = applicationUserVM.PhoneNumberConfirmed;
            applicationUser.SecurityStamp = applicationUserVM.SecurityStamp;
            applicationUser.TwoFactorEnabled = applicationUserVM.TwoFactorEnabled;
            applicationUser.UserName = applicationUserVM.UserName;
            applicationUser.Image = applicationUserVM.Image;
            applicationUser.Function = applicationUserVM.Function;
            applicationUser.Birthday = applicationUserVM.Birthday;
            applicationUser.Startdate = applicationUserVM.Startdate;
            applicationUser.Created_at = applicationUserVM.Created_at;
            applicationUser.Updatted_at = applicationUserVM.Updatted_at;

        }

        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup , ApplicationGroupViewModel applicationGroupVM)
        {
            applicationGroup.Id = applicationGroupVM.Id;
            applicationGroup.Name = applicationGroupVM.Name;
            applicationGroup.Description = applicationGroupVM.Description;

        }

        public static void UpdateApplicationRole(this ApplicationRole applicationRole, ApplicationRoleViewModel applicationRoleVM)
        {
            applicationRole.Id = applicationRoleVM.Id;
            applicationRole.Name = applicationRoleVM.Name;
            applicationRole.Description = applicationRoleVM.Description;

        }
    }
}