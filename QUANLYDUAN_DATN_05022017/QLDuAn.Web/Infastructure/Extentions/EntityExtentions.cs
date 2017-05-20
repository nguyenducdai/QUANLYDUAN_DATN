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
            hangMuc.IdDuAn = hangMucVM.IdDuAn;
            hangMuc.IdNhomCongViec = hangMucVM.IdNhomCongViec;
            hangMuc.IdMucDoTruyenThong = hangMucVM.IdMucDoTruyenThong;
            hangMuc.NgayBatDau = hangMucVM.NgayBatDau;
            hangMuc.ThoiGianDuKien = hangMucVM.ThoiGianDuKien;
            hangMuc.NgayHoanThanh = hangMucVM.NgayHoanThanh;
            hangMuc.IdNguoiThucHienTheoLenhSX = hangMucVM.IdNguoiThucHienTheoLenhSX;
            hangMuc.SoNguoiThucHien = hangMucVM.SoNguoiThucHien;
            hangMuc.DiemDanhGia = hangMucVM.DiemDanhGia;
            hangMuc.HesoKcn = hangMucVM.HesoKcn;
            hangMuc.LoaiHangMuc = hangMucVM.LoaiHangMuc;
            hangMuc.TrangThai = hangMucVM.TrangThai;
            hangMuc.isDelete = hangMucVM.isDelete;
            hangMuc.Created_at = hangMucVM.Created_at;
            hangMuc.Updated_at = hangMucVM.Updated_at;
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
            duAn.SoHopDong = duAnVM.SoHopDong;
            duAn.GiaTriHopDong = duAnVM.GiaTriHopDong;
            duAn.IdKhachHang = duAnVM.IdKhachHang;
            duAn.NgayBatDau = duAnVM.NgayBatDau;
            duAn.NgayKetThuc = duAnVM.NgayKetThuc;
            duAn.NgayHoanThanh = duAnVM.NgayHoanThanh;
            duAn.NgayKy = duAnVM.NgayKy;
            duAn.TenDuAn = duAnVM.TenDuAn;
            duAn.MoTa = duAnVM.MoTa;
            duAn.NamQuyetToan = duAnVM.NamQuyetToan;
            duAn.LoaiCongTrinh = duAnVM.LoaiCongTrinh;
            duAn.TyLeTheoDT = duAnVM.TyLeTheoDT;
            duAn.LuongThueNgoai = duAnVM.LuongThueNgoai;
            duAn.TongChiQL = duAnVM.TongChiQL;
            duAn.LuongTTQtt = duAnVM.LuongTTQtt;
            duAn.LuongDPQdp = duAnVM.LuongDPQdp;
            duAn.LuongGTQgt = duAnVM.LuongGTQgt;
            duAn.LuongGTV21 = duAnVM.LuongGTV21;
            duAn.LuongGTV22 = duAnVM.LuongGTV22;
            duAn.DonGiaDiemGT = duAnVM.DonGiaDiemGT;
            duAn.DonGiaDiemTT = duAnVM.DonGiaDiemTT;
            duAn.TongDiemGT = duAnVM.TongDiemGT;
            duAn.TongDiemTT = duAnVM.TongDiemTT;
            duAn.TrangThai = duAnVM.TrangThai;
            duAn.Created_at = duAnVM.Created_at;
            duAn.Updated_at = duAnVM.Updated_at;
        }

        public static void UpdateThamGia(this ThamGia thamGia, ThamGiaViewModel thamGiaVM)
        {
            thamGia.IdHangMuc = thamGiaVM.IdHangMuc;
            thamGia.IdNhanVien = thamGiaVM.IdNhanVien;
            thamGia.LoaiHangMuc = thamGiaVM.LoaiHangMuc;
            thamGia.HeSoThamGia = thamGiaVM.HeSoThamGia;
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

        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup, ApplicationGroupViewModel applicationGroupVM)
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

        public static void UpdateThongBao(this ThongBao thongbao, ThongBaoViewModel thongbaoVM)
        {
            thongbao.Id = thongbaoVM.Id;
            thongbao.TieuDe = thongbaoVM.TieuDe;
            thongbao.NoiDung = thongbaoVM.NoiDung;
            thongbao.NguoiTao = thongbaoVM.NguoiTao;
            thongbao.MoreFile = thongbaoVM.MoreFile;
            thongbao.Created_at = thongbaoVM.Created_at;
            thongbao.Updated_at = thongbaoVM.Updated_at;
        }

        public static void UpdateNhomCongViec(this NhomCongViec ncv , NhomCongViecViewModel ncvvm)
        {
            ncv.ID = ncvvm.ID;
            ncv.NhomCV = ncvvm.NhomCV;
            ncv.HeSoCV = ncvvm.HeSoCV;
            ncv.GhiChu = ncvvm.GhiChu;
        }

        public static void UpdateHeSoLap(this HeSoLap hsl, HeSoLapViewModel hslVm)
        {
            hsl.Id = hslVm.Id;
            hsl.Hesl = hslVm.Hesl;
            hsl.SoNam = hslVm.SoNam;
            
        }

        public static void UpdateHeSoTg(this HeSoTg hstg, HeSoTgViewModel hstgVm)
        {
           hstg.Id = hstgVm.Id;
           hstg.ThoiGianDk = hstgVm.ThoiGianDk;
           hstg.HeSoTgdk = hstgVm.HeSoTgdk;

        }

        public static void UpdateNhanCong(this HeSoNhanCong hsnc, HeSoNhanCongViewModel hsncVm)
        {
           hsnc.Id = hsncVm.Id;
           hsnc.HeSoNcKcn = hsncVm.HeSoNcKcn;
           hsnc.SoNguoiThucHien = hsncVm.SoNguoiThucHien;

        }
    }
}