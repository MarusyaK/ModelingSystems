using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ModelingSystems
{
    /// <summary>
    ///  Взяла пример из https://docs.microsoft.com/ru-ru/archive/msdn-magazine/2016/august/test-run-lightweight-random-number-generation
    /// </summary>
    public class LehmerRngDistr
    {
        private const int a = 16807;
        private const int m = 2147483647;
        private const int q = 127773;
        private const int r = 2836;
        private int seed;
        public LehmerRngDistr(int seed)
        {
            if (seed <= 0 || seed == int.MaxValue)
                throw new Exception("Bad seed");
            this.seed = seed;
        }
        public double Next()
        {
            int hi = seed / q;
            int lo = seed % q;
            seed = (a * lo) - (r * hi);
            if (seed <= 0)   // предотвращение переполнения
                seed = seed + m;
            return (seed * 1.0) / m;
        }
        public void generateFile(string filename, int count)
        {
            int hi = 100;
            int lo = 0;
            LehmerRngDistr lehmer = new LehmerRngDistr(1);
            var file = File.CreateText("LehmerDistr.txt");
            for (int i = 0; i < count; ++i)
            {
                double x = lehmer.Next();
                int ri = (int)((hi - lo) * x + lo); // 0 to 100
                file.Write($"{ri} ");
            }
        }
    }
}
