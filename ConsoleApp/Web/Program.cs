using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp.Web
{
    internal class Program
    {
        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
            // MainAsync().Wait();  // а можно и так

            Console.ReadKey();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("----- Enter main");
            await TestHttpClient();
            Console.WriteLine("----- Exit main");
        }

        private static async Task TestHttpClient()
        {
            var webProxy = WebRequest.GetSystemWebProxy();
            webProxy.Credentials = new NetworkCredential(@"samnipineft\Garanin_MS", "iSochi06");
            WebRequest.DefaultWebProxy = webProxy;

            var url = "https://www.sports.ru/rss/main.xml";

            using (var client = new HttpClient(new HttpClientHandler() {Proxy = WebRequest.DefaultWebProxy}))
            {
                using (var response = await client.GetAsync(url))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
            }
        }
    }
}