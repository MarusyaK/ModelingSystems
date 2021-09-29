using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ModelingSystems
{
    public class Norm
    {
        LehmerRngDistr distr;
        double mx,  sigm;
        public Norm(LehmerRngDistr distr, double Mx, double sigm)
        {
            this.distr = distr;
            this.mx = Mx;
            this.sigm = sigm;
        }
        public double Next()
        {
            double calc = 0;
            for(int i=0;i<12;i++)
            {
                calc+=distr.Next();
            }
            return mx+sigm*(calc-6);
        }

        public void generateFile(string filename, int count)
        {
            var file = File.CreateText("NormDistr.txt");
            for (int i = 0; i < count; ++i)
            {
                double x = Next();
                file.Write($"{x} ");
            }
        }
    }
}
