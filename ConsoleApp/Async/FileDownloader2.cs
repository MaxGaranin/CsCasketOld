using System;
using System.IO;
using System.Net;

namespace ConsoleApp.Async
{
    public class FileDownloader2
    {
        static FileDownloader2()
        {
            SetupProxy();
        }

        public static void CheckRange(string uri, string path)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = "http://as21470.samnipineft.ru:8010";
            wc.Proxy = new WebProxy("127.0.0.1:8888", false);

            var cache = new CredentialCache();
            cache.Add(new Uri("http://as21470.samnipineft.ru:8010"), "NTLM", new NetworkCredential("Garanin_MS", "<f,jxrf12"));
            wc.Credentials = cache;

            wc.DownloadFile("http://as21470.samnipineft.ru:8010/repository/download/Tsfm2_DevelopMaster/7475:id/TSFM2-1.2.5.3397.dev.zip", @"d:\test.zip");
        }

        public static void Download(string uri, string path)
        {
            var request = WebRequest.CreateHttp(uri);

            var fi = new FileInfo(path);
            var havePart = fi.Exists;

            if (havePart)
                request.AddRange(fi.Length);

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                if (int.TryParse(response.Headers.Get("Content-Length"), out var contentLength))
                {
                    //Do something useful with ContentLength here 
                }

                var partialDownload = havePart;

                using (var fileStream = File.Open(path, partialDownload ? FileMode.Append : FileMode.Create))
                {
                    using (var netStream = response.GetResponseStream())
                    {
                        netStream.CopyTo(fileStream);

//                        int bufferSize = 1024*1024*10;
//                        byte[] buffer = new byte[bufferSize];
//                        int readSize;
//                        long fullReadSize = partialDownload ? fi.Length : 0;
//
//                        while ((readSize = netStream.Read(buffer, 0, bufferSize)) > 0)
//                        {
//                            fileStream.Write(buffer, 0, readSize);
//                            fileStream.Flush();
//
//                            fullReadSize += readSize;
//                            Console.SetCursorPosition(0, 0);
//                            Console.WriteLine("Прочитано {0}%", fullReadSize*100/contentLength);
//                        }
                    }
                }
            }
        }

        private static void SetupProxy()
        {
            var webProxy = new WebProxy("10.214.104.214:3128", true)
            {
                Credentials = new NetworkCredential("Garanin_MS", "<f,jxrf12")
            };
            WebRequest.DefaultWebProxy = webProxy;
        }
    }
}