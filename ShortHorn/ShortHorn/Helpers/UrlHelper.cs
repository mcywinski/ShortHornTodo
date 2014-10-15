using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortHorn.Helpers
{
    public class UrlHelper
    {
        public static string GetAccessTokenFromQueryString()
        {
            string token = HttpContext.Current.Request.QueryString["token"];
            return token;
        }
    }
}