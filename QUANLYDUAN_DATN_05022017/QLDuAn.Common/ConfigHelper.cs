using OfficeOpenXml;
using System;
using System.Configuration;

namespace QLDuAn.Common
{
    public class ConfigHelper
    {
        public static string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }       
    }
}