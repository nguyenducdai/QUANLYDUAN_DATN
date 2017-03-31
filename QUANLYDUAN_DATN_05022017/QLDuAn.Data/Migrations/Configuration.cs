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

            // AddNhomCongViec(context);
            AddHesl(context);
            AddHsoKcn(context);
            HeSoTg(context);
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

        private void AddNhomCongViec(QLDuAnDbContext context)
        {
            var list = new List<NhomCongViec>()
            {
                new NhomCongViec()
                {
                    NhomCV = "A",
                    HeSoCV = 3,
                    GhiChu= "nhóm công việc A"
                },   new NhomCongViec()
                {
                    NhomCV = "B",
                    HeSoCV = 2,
                    GhiChu= "nhóm công việc B"
                },
                 new NhomCongViec(){
                    NhomCV = "C",
                    HeSoCV = 1.5m,
                    GhiChu= "nhóm công việc C"
                }
                 ,
                 new NhomCongViec(){
                    NhomCV = "D",
                    HeSoCV = 1,
                    GhiChu= "nhóm công việc D"
                }
            };
            context.NhomCongViec.AddRange(list);
            context.SaveChanges();
        }

        private void AddHesl(QLDuAnDbContext context)
        {
            var list = new List<HeSoLap>()
            {
                new HeSoLap()
                {
                   SoNam ="1",
                   Hesl = 1.00m
                },
                      new HeSoLap()
                {
                   SoNam ="1",
                   Hesl = 1.00m
                },
                            new HeSoLap()
                {
                   SoNam ="2",
                   Hesl =   0.70m
                },
                new HeSoLap()
                {
                   SoNam ="3",
                   Hesl = 0.60m
                },
                 new HeSoLap()
                {
                   SoNam =">3 ",
                   Hesl =  0.50m
                },
            };
            context.HeSoLap.AddRange(list);
            context.SaveChanges();
        }

        private void AddHsoKcn(QLDuAnDbContext context)
        {
            var list = new List<HeSoNhanCong>()
            {
                new HeSoNhanCong()
                {
                   SoNguoiThucHien =0,
                   HeSoNcKcn = 0
                },
                new HeSoNhanCong()
                {
                   SoNguoiThucHien =1,
                   HeSoNcKcn = 1.00m
                },
                new HeSoNhanCong()
                {
                   SoNguoiThucHien =2,
                   HeSoNcKcn = 1.4m
                } ,
                new HeSoNhanCong()
                {
                   SoNguoiThucHien =3,
                   HeSoNcKcn = 2.01m
                },
                 new HeSoNhanCong()
                {
                   SoNguoiThucHien =4,
                   HeSoNcKcn = 2.64m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =5,
                   HeSoNcKcn = 5.35m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =6,
                   HeSoNcKcn = 3.84m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =7,
                   HeSoNcKcn = 4.41m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =8,
                   HeSoNcKcn = 4.96m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =9,
                   HeSoNcKcn = 5.49m
                }, new HeSoNhanCong()
                {
                   SoNguoiThucHien =10,
                   HeSoNcKcn = 6.00m
                }
            };
            context.HeSoNhanCong.AddRange(list);
            context.SaveChanges();
        }

        private void HeSoTg(QLDuAnDbContext context)
        {
            var list = new List<HeSoTg>()
            {
                new HeSoTg()
                {
                   ThoiGianDk ="1-4",
                   HeSoTgdk = 1.00m
                },
                   new HeSoTg()
                {
                   ThoiGianDk ="5-9",
                   HeSoTgdk = 1.50m
                },
                   new HeSoTg()
                {
                   ThoiGianDk ="10-14",
                   HeSoTgdk = 2.00m
                },
                  new HeSoTg()
                {
                   ThoiGianDk ="15-19",
                   HeSoTgdk = 2.40m
                },
                  new HeSoTg()
                {
                   ThoiGianDk ="20-24",
                   HeSoTgdk = 2.80m
                },

                new HeSoTg()
                {
                   ThoiGianDk ="25-29",
                   HeSoTgdk = 3.20m
                },
                 new HeSoTg()
                {
                   ThoiGianDk ="30-34",
                   HeSoTgdk = 3.50m
                },
                   new HeSoTg()
                {
                   ThoiGianDk =">35",
                   HeSoTgdk = 3.80m
                }

            };
            context.HeSoTg.AddRange(list);
            context.SaveChanges();
        }
    }
}
