using Microsoft.AspNet.Identity.EntityFramework;
using QLDuAn.Model.Models;
using System.Data.Entity;

namespace QLDuAn.Data
{
    public class QLDuAnDbContext : IdentityDbContext<ApplicationUser>
    {
        public QLDuAnDbContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<DuAn> DuAn { get; set; }

        public DbSet<DuAnHangMuc> DuAnHangMuc { get; set; }

        public DbSet<HangMuc> HangMuc { get; set; }

        public DbSet<KhachHang> KhachHang { get; set; }

        public DbSet<NhomCongViec> NhomCongViec { get; set; }

        public DbSet<ThamGia> ThamGia { get; set; }

        public DbSet<NhanVien> NhanVien { get; set; }

        public DbSet<HopDong> HopDong { get; set; }

        public DbSet<Errors> Errors { get; set; }

        public static QLDuAnDbContext Create()
        {
            return new QLDuAnDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}