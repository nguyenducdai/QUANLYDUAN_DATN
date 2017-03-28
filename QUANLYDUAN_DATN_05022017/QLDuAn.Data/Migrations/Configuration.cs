namespace QLDuAn.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QLDuAn.Data.QLDuAnDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QLDuAn.Data.QLDuAnDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

           // AddApplicationGroup(context);

        }

        private void AddAdnin(QLDuAnDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new QLDuAnDbContext()));
            var userRole = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new QLDuAnDbContext()));
            var user = new ApplicationUser()
            {
                UserName = "aso2017",
                Email = "aso.codien.2017@gmail.com",
                EmailConfirmed = true,
                FullName = "Aso Company"

            };

            userManager.Create(user, "123456");

            if (!userRole.Roles.Any())
            {
                userRole.Create(new ApplicationRole { Name = "Admin" });
                userRole.Create(new ApplicationRole { Name = "User" });
            }

            var adminUser = userManager.FindByEmail(user.Email);

            userManager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void AddUser(QLDuAnDbContext context)
        {
            var list = new List<KhachHang>()
            {
                new KhachHang()
                {
                  TenKhach="Nguyễn văn Bình",
                  SoDienThoai=0988777888,
                  Email="nguyenvanbinh.aso@gmail.com",
                  GioiTinh=true,
                  DiaChi="Thái nguyên",
                  SoFax="Fax:098877577"
                },
                  new KhachHang()
                {
                  TenKhach="Nguyễn văn Thắng",
                  SoDienThoai=0988777355,
                  Email="nguyenvanthang.aso@gmail.com",
                  GioiTinh=true,
                  DiaChi="Hà nội",
                  SoFax="Fax:098877537"
                },
                    new KhachHang()
                {
                  TenKhach="Bùi thì lan",
                  SoDienThoai=0988777222,
                  Email="buithilan.aso@gmail.com",
                  GioiTinh=true,
                  DiaChi="bắc ninh",
                  SoFax="Fax:098877537"
                }
            };

            try
            {
                context.KhachHang.AddRange(list);
                context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void AddApplicationGroup(QLDuAnDbContext context)
        {
            var list = new List<ApplicationGroup>()
            {
                new ApplicationGroup()
                {
                    Name="Admin",
                    Description="nhóm người có quyền tối điều hành hệ thống"
                },
                new ApplicationGroup()
                {
                   Name="User",
                    Description="nhóm thành viên"
                }
               
            };

            try
            {
                context.ApplicationGroup.AddRange(list);
                context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
