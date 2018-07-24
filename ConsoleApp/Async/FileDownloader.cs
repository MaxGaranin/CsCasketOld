using System;
using System.IO;
using System.Linq;
using System.Net;
using ConsoleApp.Helpers;

namespace ConsoleApp.Async
{
    public class FileDownloader
    {
        static FileDownloader()
        {
            ProxyHelper.SetupProxy();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public static void Download(string uri, string path)
        {
            var request = WebRequest.CreateHttp(uri);

            var fi = new FileInfo(path);
            var fileExists = fi.Exists;
            var fileLength = fi.Exists ? fi.Length : 0;

            if (fileExists)
                request.AddRange(fileLength);

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                var isAcceptRanges = false;
                var contentLength = 0;

                if (response.Headers.AllKeys.Contains("Accept-Ranges") &&
                    response.Headers.AllKeys.Contains("Content-Length"))
                {
                    isAcceptRanges = true;

                    if (int.TryParse(response.Headers.Get("Content-Length"), out contentLength))
                    {
                        if (fileExists && fileLength == contentLength)
                        {
                            Console.WriteLine("File already has read.\nExit.");
                            return;
                        }
                    }
                }

                using (var fileStream = File.Open(path, fileExists ? FileMode.Append : FileMode.Create))
                {
                    using (var netStream = response.GetResponseStream())
                    {
                        if (netStream == null)
                        {
                            Console.WriteLine("Can't read a file from url.\nExit.");
                            return;
                        }

                        if (isAcceptRanges)
                        {
                            int bufferSize = 1024;
                            byte[] buffer = new byte[bufferSize];
                            int readSize;
                            long fullReadSize = 0;

                            while ((readSize = netStream.Read(buffer, 0, bufferSize)) > 0)
                            {
                                fileStream.Write(buffer, 0, readSize);
                                fileStream.Flush();

                                fullReadSize += readSize;
                                var fullPercent = (fullReadSize + fileLength) * 100 / (contentLength + fileLength);
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("Downloaded {0}%", fullPercent);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Downloading...");
                            netStream.CopyTo(fileStream);
                        }
                    }
                }
            }
        }
    }
}