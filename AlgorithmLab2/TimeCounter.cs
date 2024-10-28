using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace AlgorithmLab2
{
    internal class TimeCounter
    {
        public static double[] MeasureTime(int discCount, IExucate algorithm)
        {
            List<double> tests = new List<double>();

            for (int i = 1; i <= discCount; i++)
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                algorithm.ExucateAlgorithm(i);
                stopwatch.Stop();

                tests.Add(stopwatch.Elapsed.TotalMilliseconds);
            }

            return tests.ToArray();
        }
    }
}
