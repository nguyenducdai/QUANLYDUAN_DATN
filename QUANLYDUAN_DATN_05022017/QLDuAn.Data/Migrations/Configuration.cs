namespace QLDuAn.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QLDuAn.Data.QLDuAnDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(QLDuAn.Data.QLDuAnDbContext context)
        {
            // this.AddRowThanhVien(context);
           // this.AddRowUser(context);
            //this.AddGroupJob(context);
        }

        private void AddRowThanhVien(QLDuAn.Data.QLDuAnDbContext context)
        {
            List<KhachHang> thanhvien = new List<KhachHang>()
            {
                new KhachHang()
                 {
                    TenKhach = "Nguyễn văn A",
                    SoDienThoai = 01656199283,
                    SoFax="Fax:2035",
                    Email="example@gmail.com",
                    DiaChi = "Tây Nguyên",
                    GioiTinh=true
                },
                 new KhachHang()
                 {
                    TenKhach = "Nguyễn văn B",
                    SoDienThoai = 01656199284,
                    SoFax="Fax:2035",
                    Email="example1@gmail.com",
                    DiaChi = "Hà Nội",
                    GioiTinh=true
                },
                  new KhachHang()
                 {
                    TenKhach = "Nguyễn văn C",
                    SoDienThoai = 01656199285,
                    SoFax="Fax:2035",
                    Email="example2@gmail.com",
                    DiaChi = "PHú Lương",
                    GioiTinh=false
                },
                   new KhachHang()
                 {
                    TenKhach = "Nguyễn văn D",
                    SoDienThoai = 01656199286,
                    SoFax="Fax:2035",
                    Email="example3@gmail.com",
                    DiaChi = "Thái Nguyên",
                    GioiTinh=true
                },
                    new KhachHang()
                 {
                    TenKhach = "Nguyễn văn F",
                    SoDienThoai = 01656199287,
                    SoFax="Fax:2035",
                    Email="example4@gmail.com",
                    DiaChi = "Quảng Ninh",
                    GioiTinh=false
                }
            };

            context.KhachHang.AddRange(thanhvien);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        private void AddRowUser(QLDuAn.Data.QLDuAnDbContext context)
        {
            var user = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userRole = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var obj = new ApplicationUser();
            obj.UserName = "NguyenvanC";
            obj.Email = "nguyenvanb@gmail.com";
            obj.EmailConfirmed = true;
            obj.PhoneNumberConfirmed = false;
            obj.TwoFactorEnabled = false;
            obj.LockoutEnabled = false;
            obj.AccessFailedCount = 0;
            user.Create(obj, "123456&");

            //if (!userRole.Roles.Any())
            //{
            //    userRole.Create(new IdentityRole { Name = "Admin" });
            //    userRole.Create(new IdentityRole { Name = "User" });
            //}

            // find by mail 
            var result = user.FindByEmail(obj.Email);

            // if success then add user in group created
            user.AddToRolesAsync(result.Id, new string[] {"User" });

        }

        private void AddGroupJob(QLDuAn.Data.QLDuAnDbContext context)
        {
            List<NhomCongViec> cv = new List<NhomCongViec>()
            {
                new NhomCongViec
                {
                    NhomCV="A",
                    HeSoCV = 3
                },
                new NhomCongViec
                {
                    NhomCV="B",
                    HeSoCV = 2
                },
                new NhomCongViec
                {
                    NhomCV="C",
                    HeSoCV = 1.5M
                },
                new NhomCongViec
                {
                    NhomCV="D",
                    HeSoCV = 1
                }
            };
            context.NhomCongViec.AddRange(cv);
            context.SaveChanges();
        }
    }
}
