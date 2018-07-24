using System.Net;

namespace ConsoleApp.Helpers
{
    public class ProxyHelper
    {
        private const string Login = @"samnipineft\Garanin_MS";
        private const string Password = "iSochi06";
        private const string ProxyAddress = "10.214.104.214:3128";

        public static void SetupProxy()
        {
            var webProxy = new WebProxy(ProxyAddress, true)
            {
                Credentials = new NetworkCredential(Login, Password)
            };
            WebRequest.DefaultWebProxy = webProxy;
        }

        public static void SetupProxy2()
        {
            var webProxy = WebRequest.GetSystemWebProxy();
            webProxy.Credentials = new NetworkCredential(Login, Password);
            WebRequest.DefaultWebProxy = webProxy;
        }
    }
}