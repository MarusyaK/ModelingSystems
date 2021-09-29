using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingSystems
{
    public class TableDistr
    {
        LehmerRngDistr distr;

        Dictionary<int, double> tableDistr;
        public TableDistr(LehmerRngDistr extdistr)
        {
            this.distr = extdistr;
            tableDistr = new Dictionary<int, double>();
            tableDistr[1] = 0.05;
            tableDistr[2] = 0.1;
            tableDistr[3] = 0.15;
            tableDistr[4] = 0.3;
            tableDistr[5] = 0.15;
            tableDistr[6] = 0.1;
            tableDistr[7] = 0.1;
            tableDistr[8] = 0.05;
        }

        public int Next ()
        {
            var rnd = distr.Next();

            return 0;
        }
    }
}
