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

        public static string ExternalAuthPath
        {
            get
            {
#if DEBUG
                return WebConfigurationManager.AppSettings["debugExternalAuthUrl"];
#else
                     return WebConfigurationManager.AppSettings["releaseExternalAuthUrl"];
#endif
            }
        }

        public static string OAuthReferer
        {
            get
            {
                #if DEBUG
                    return WebConfigurationManager.AppSettings["debugRefererUrl"];
                #else
                     return WebConfigurationManager.AppSettings["releaseRefereUrl"];
                #endif
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
                if (String.IsNullOrEmpty(res) || res == "/")
                    return "/";
                
                return res;
            }
        }

        public static Uri GetLocalLoginUri()
        {
            var basePath = BasePath;
            if (string.IsNullOrEmpty(basePath))
                return new Uri("/login", UriKind.Relative);
            return new Uri(basePath.TrimEnd('/') + "/login", UriKind.Relative);
        }

        
        public static string TokenSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings["secret"];
            }
        }
    }
}