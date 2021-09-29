using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;




namespace ModelingSystems
{
    class Runner
    {
        static void Main(string[] args)
        {
            LehmerRngDistr d = new LehmerRngDistr(1);
            d.generateFile("LehmerDistr.txt", 10000);
            Norm nd = new Norm(d,50,5);
            nd.generateFile("NormdDistr.txt",10000);
            GeneratorTest test = new GeneratorTest();
            var res = test.Analize(d);

            res = test.Analize2(d);
            

        }
    }
}
