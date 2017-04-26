using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Mapping
{
    public class AutoMappingConfiguration
    {
        public static void Configuration()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<HangMuc, HangMucViewModel>();
                cfg.CreateMap<KhachHang, KhachHangViewModel>();
                cfg.CreateMap<DuAn, DuAnViewModel>();
                cfg.CreateMap<ThamGia, ThamGiaViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<NhomCongViec, NhomCongViecViewModel>();
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                cfg.CreateMap<HeSoLap, HeSoLapViewModel>();
                cfg.CreateMap<HeSoNhanCong, HeSoNhanCongViewModel>();
                cfg.CreateMap<HeSoTg, HeSoTgViewModel>();
                cfg.CreateMap<ThongBao, ThongBaoViewModel>();
            });
        }
    }
}