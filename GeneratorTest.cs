using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingSystems
{
    class GeneratorTest
    {
        List<int> changeSignSource;
        public GeneratorTest() {
            changeSignSource = new List<int>();
        }
        static double Erf(double x) //Честно взяла отсюда https://www.johndcook.com/blog/csharp_erf/
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
        public bool Analize(LehmerRngDistr distrt)  //NIST test  частотный побитовый тест
        {
            int sum = 0;
            int sumBit = 0;
            while( sumBit < 100)
            {
                int d = (int)(distrt.Next() * 128); //Генерируем от [0,128) 
                sumBit += bitcount(d);
                sum += calcBits(d);
            }
            var s = sum / Math.Sqrt(sumBit);
            var val = 1-GeneratorTest.Erf(s/Math.Sqrt(2)); //заменила erfc на 1-erf, где-то в интернете прочла что так надо
            return val>0.01;
        }
        int bitcount (int value)
        {
            for (int i = 0; i < 8; i++)
                if (1 << i > value)
                {
                   return   i;
                }
            return -1;
        }
        int calcBits(int value)
        {
            int calc=0;
            int power = bitcount(value);
            for (int i = 0; i < power; i++)
            {
                if ((value & 1) == 1)
                    calc++;
                else
                    calc--;
                value = value >> 1;
            }
            return calc;
        }
        
        int calcOnly1(int value)
        {
            int calc = 0;
            int power = bitcount(value);
            for (int i = 0; i < power; i++)
            {
                changeSignSource.Add(value & 1);
                if ((value & 1) == 1)
                    calc++;
                value = value >> 1;
            }
            return calc;
        }
        public bool Analize2(LehmerRngDistr distrt)  //NIST test  на одинаково подряд идущие биты
        {
            changeSignSource.Clear();
            int sum = 0;
            int sumBit = 0;
            while (sumBit < 100)
            {
                int d = (int)(distrt.Next() * 128); //Генерируем от [0,128) 
                sumBit += bitcount(d);
                sum += calcOnly1(d);
            }
            //проверяем долю единиц в общей массе
            var pi = sum*1.0 / sumBit;
            if (pi - 0.5 > 2.0 / Math.Sqrt(sumBit))
                return false;
            var znak = 0;
            var arr = changeSignSource.ToArray();
            for (int i=1;i<arr.Length;i++)
            {
                znak += arr[i] == arr[i - 1] ? 0 : 1;
            }
            var expr = Math.Abs(znak - 2 * sumBit * pi * (1 - pi)) / (2 * Math.Sqrt(2 * sumBit) * pi * (1 - pi));
            var val = 1 - GeneratorTest.Erf(expr);
            return val > 0.01;
        }
    }
}
