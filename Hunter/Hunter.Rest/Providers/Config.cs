using System;
using System.Web.Configuration;

namespace Hunter.Rest.Providers
{
    public static class Config
    {
        public static bool UseExternalAuth
        {
            get
            {
                var val = WebConfigurationManager.AppSettings["useExternalAuth"];
                bool res;
                if (bool.TryParse(val, out res))
                {
                    return res;
                }
                return false;
            }
        }

        public static string ExternalPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["externalAuthUrl"];
            }
        }

        public static string BasePath
        {
            get
            {
                string res = String.Empty;
                try
                {
                    res = WebConfigurationManager.AppSettings["basePath"];
                }
                catch (Exception)
                {
                }
                return string.IsNullOrEmpty(res) ? "/" : "/" + res.Trim('/');
            }
        }

        public static Uri GetLocalLoginUri()
        {
            var basePath = BasePath;
            if (string.IsNullOrEmpty(basePath))
                return new Uri("/login", UriKind.Relative);
            return new Uri(basePath.TrimEnd('/') + "/login", UriKind.Relative);
        }
    }
}