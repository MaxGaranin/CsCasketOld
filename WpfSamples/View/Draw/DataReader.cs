using System;
using System.Collections.Generic;
using System.IO;

namespace WpfSamples.View.Draw
{
    public class DataReader
    {
        public static IList<WellPoint> ReadWells(string fileName)
        {
            var points = new List<WellPoint>();
            using (var sr = new StreamReader(fileName))
            {
                var line = sr.ReadLine();

                while (line != null)
                {
                    line = line.Replace('.', ',');
                    var tokens = line.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    points.Add(new WellPoint()
                        {
                            WellName = tokens[3],
                            X = double.Parse(tokens[0]),
                            Y = double.Parse(tokens[1]),
                            Z = double.Parse(tokens[2])
                        });

                    line = sr.ReadLine();
                }
            }

            return points;
        }
        
    }
}