using System;
using System.Web.Mvc;

namespace QLDuAn.Common
{
    public static class ActivelUrlHelper
    {
        public static string MakeActiveClass(this UrlHelper urlHelper, string controller)
        {
            string result = " active open";

            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();

            if (!controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                result = "";
            }

            return result;
        }

        
    }
}