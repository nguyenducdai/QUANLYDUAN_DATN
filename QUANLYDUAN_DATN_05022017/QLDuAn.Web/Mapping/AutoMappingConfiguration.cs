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
                cfg.CreateMap<HopDong, HopDongViewModel>();
                cfg.CreateMap<KhachHang, KhachHangViewModel>();
                cfg.CreateMap<DuAn, DuAnViewModel>();
                cfg.CreateMap<ThamGia, ThamGiaViewModel>();
                cfg.CreateMap<DuAnHangMuc, HangMucDuAnViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<NhomCongViec, NhomCongViecViewModel>();
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            });
        }
    }
}