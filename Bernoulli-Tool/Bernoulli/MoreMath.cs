using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Bernoulli
{
    public class MoreMath
    {
        public static long Choose(long n, long k)
        {
            long result = 1;
            for (long i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }
            return result;
        }

        public static BigInteger Choose(BigInteger n, BigInteger k)
        {
            BigInteger result = 1;
            for (BigInteger i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }
            return result;
        }
    }
}
