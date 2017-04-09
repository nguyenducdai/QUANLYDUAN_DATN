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

        public DbSet<HangMuc> HangMuc { get; set; }

        public DbSet<KhachHang> KhachHang { get; set; }

        public DbSet<NhomCongViec> NhomCongViec { get; set; }

        public DbSet<ThamGia> ThamGia { get; set; }

        public DbSet<HopDong> HopDong { get; set; }

        public DbSet<Errors> Errors { get; set; }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<ApplicationGroup> ApplicationGroup { get; set; }

        public DbSet<ApplicationRoleGroup> ApplicationRoleGroup { get; set; }

        public DbSet<ApplicationUserGroup> ApplicationUserGroup { get; set; }

        public DbSet<HeSoLap> HeSoLap { get; set; }

        public DbSet<HeSoTg> HeSoTg { get; set; }

        public DbSet<HeSoNhanCong> HeSoNhanCong { get; set; }

        public DbSet<ThongBao> ThongBao { get; set; }

        public static QLDuAnDbContext Create()
        {
            return new QLDuAnDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserRole>().HasKey(x => new { x.RoleId, x.UserId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(x => x.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
    }
}