using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Data.Infrastrusture
{
    // ke thua tu IDisposable cho0 phep cai dat cac phuong thuc tu dong huy
    public class Dispostable : IDisposable
    {
        public bool isDisposed;

        //khi huy doi tuong dispose thi se khong dispose
        ~Dispostable()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);

            // gadit connection ->thu hoach bo nho
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool disposing)
        {
           if(!isDisposed && disposing)
            {
                DisposeCore();
            }
            isDisposed =true;
        }

        protected virtual void DisposeCore()
        {

        }
    }
}
