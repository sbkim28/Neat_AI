using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Util
{
    public class MathUtils
    {

        public static double 
            Sigmoid(double x)
        {
            return 2 / (1 + System.Math.Exp(-4.9 * x)) - 1;
        }


        public static int Pow(int b, int ex)
        {
            if (b < 0 || ex < 0) return -1;

            int ret = 1;
            while (ex != 0)
            {
                if ((ex & 1) == 1)
                    ret *= b;
                b *= b;
                ex >>= 1;
            }
            return ret;
        }
    }
}
