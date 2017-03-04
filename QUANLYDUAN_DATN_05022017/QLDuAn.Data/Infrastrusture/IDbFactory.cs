using QLDuAn.Data;
using System;

namespace QLDuAn.Data.Infrastrusture
{
    public interface IDbFactory : IDisposable
    {
        //giao tiep de khoi tao cac doi tuong entity
        QLDuAnDbContext Init();
    }
}