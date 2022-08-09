using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Mvc;
using UmbracoEshop.lib.Util;

namespace UmbracoEshop.lib.Controllers
{
    public class _BaseController : SurfaceController
    {
        public string CurrentSessionId
        {
            get
            {
                return new _BaseControllerUtil().CurrentSessionId;
            }
        }
        public string DefaultImgPath
        {
            get
            {
                return this.HttpContext.Server.MapPath("~/Styles/Images");
            }
        }

        protected RedirectToUmbracoPageResult RedirectToEshopUmbracoPage(string pageKey)
        {
            return this.RedirectToUmbracoPage(GetPageId(pageKey));
        }
        protected RedirectToUmbracoPageResult RedirectToOsobnaStrankaUmbracoPage(string pageKey, string queryString)
        {
            return this.RedirectToUmbracoPage(GetPageId(pageKey), queryString);
        }

        int GetPageId(string pageKey)
        {
            return ConfigurationUtil.GetPageId(pageKey);
        }
    }
    public class _BaseControllerUtil
    {
        public string CurrentSessionId
        {
            get
            {
                return System.Web.HttpContext.Current.Session.SessionID;
            }
        }
        public System.Web.HttpRequest CurrentRequest
        {
            get
            {
                return System.Web.HttpContext.Current.Request;
            }
        }

        public string SiteRootUrl
        {
            get
            {
                System.Uri uri = this.CurrentRequest.Url;

                return string.Format("{0}://{1}{2}",
                    uri.Scheme,
                    uri.Host,
                    uri.IsDefaultPort ? "" : string.Format(":{0}", uri.Port));
            }
        }

        public string GetAbsoluteUrl(string relativeUrl)
        {
            return string.Format("{0}{1}", this.SiteRootUrl, relativeUrl);
        }
    }
}
