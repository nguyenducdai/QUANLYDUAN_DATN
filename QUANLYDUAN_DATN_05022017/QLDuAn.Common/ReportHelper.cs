using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Common
{
    public static class ReportHelper
    {
        public static Task GenerateExcel<T>(List<T> data , string path)
        {
            return Task.Run(()=> {

                using (ExcelPackage ep = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorksheet exs = ep.Workbook.Worksheets.Add("Trực tiếp");
                    exs.Cells["A1"].LoadFromCollection<T>(data, true, OfficeOpenXml.Table.TableStyles.Light1);
                    exs.Cells.AutoFitColumns();
                    ep.Save();
                }
            });
        }
    }
}
