using System.Collections.Generic;
using System.Linq;

namespace QLDuAn.Web.Infastructure.Core
{
    public class Paginnation<T>
    {
        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (items != null) ? items.Count() : 0;
            }
        }

        public int TotalCount { get; set; }

        public int TotalPage { get; set; }

        public int MaxPage { get; set; }

        public IEnumerable<T> items { get; set; }
    }
}