using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotImplementedLab.Math
{
    public static class Arithmetics
    {
        public static int GCD(params int[] numbers)
            => numbers.Aggregate(GCD);

        public static int GCD(int a, int b)
            => b == 0 ? a : GCD(b, a % b);

        public static int GetSignOf(this int a, int b)
        {
            var _a = System.Math.Abs(a);
            return b < 0 ? -_a : _a;
        }
    }
}
