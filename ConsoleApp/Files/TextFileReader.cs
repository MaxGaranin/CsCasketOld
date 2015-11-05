using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ConsoleApp.Files
{
    public class TextFileReader
    {
        public static void ReadFiles(string folderName, string fileMask = "*.*")
        {
            var fileNames = Directory.GetFiles(folderName, fileMask);
            
            foreach (var fileName in fileNames)
            {
                var outFileName = Path.Combine(folderName,
                    Path.GetFileNameWithoutExtension(fileName) + "_out" + Path.GetExtension(fileName));
                
                var watch = Stopwatch.StartNew();
                using (var sr = new StreamReader(fileName, Encoding.Default))
                using (var sw = new StreamWriter(outFileName, false, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(line);
                    }
                }
                var time = watch.Elapsed.Milliseconds;
                Console.WriteLine("{0} мс", time);
            }
        }
    }
}