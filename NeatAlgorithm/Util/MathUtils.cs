using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Util
{
    // 필요한 수학 함수를 모와둔 클래스
    public class MathUtils
    {
        public static double ReLU(double x)
        {
            return x < 0 ? 0 : x;
        }

        // 시그모이드 함수, 원 제작자의 논문에서 기재된 조정된 함수를 가져와 사용함
        public static double 
            Sigmoid(double x)
        {
            return 2 / (1 + System.Math.Exp(-4.9 * x)) - 1;
        }

        // 자연수인 지수에 대해서 제곱을 수행함. b - 밑, ex - 지수
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
