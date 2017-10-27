using System;
using System.IO;
using System.Net;

namespace ConsoleApp.Async
{
    public class FileDownloader
    {
        static FileDownloader()
        {
            SetupProxy();
        }

        public static void Download(string uri, string path)
        {
            var request = WebRequest.CreateHttp(uri);

//            var cache = new CredentialCache();
//            cache.Add(new Uri("http://as21470.samnipineft.ru"), "NTLM", new NetworkCredential("Garanin_MS", "<f,jxrf12"));
//            request.Credentials = cache;
            
            var fi = new FileInfo(path);
            var fileExists = fi.Exists;
            var fileLength = fi.Exists ? fi.Length : 0;

            if (fileExists)
                request.AddRange(fileLength);

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                if (int.TryParse(response.Headers.Get("Content-Length"), out var contentLength))
                {
                    if (fileExists && fileLength == contentLength)
                    {
                        Console.WriteLine("File already has read.\nExit.");
                        return;
                    }
                }

                using (var fileStream = File.Open(path, fileExists ? FileMode.Append : FileMode.Create))
                {
                    using (var netStream = response.GetResponseStream())
                    {
                        // быстрый способ без индикатора
                        // netStream.CopyTo(fileStream);

                        int bufferSize = 1024;
                        byte[] buffer = new byte[bufferSize];
                        int readSize;
                        long fullReadSize = 0;

                        while ((readSize = netStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            fileStream.Write(buffer, 0, readSize);
                            fileStream.Flush();

                            fullReadSize += readSize;
                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine("Прочитано {0}%", fullReadSize*100/contentLength);
                        }
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