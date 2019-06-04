using Microsoft.FSharp.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Bernoulli.Rows
{
    public class Harmonic : Row<BigInteger, BigRational>
    {
        internal override IEnumerable<(BigInteger, BigRational)> Yielder()
        {
            BigRational sum = 0;

            for (BigInteger k = 1; ; k++)
            {
                BigRational add = 1 / k;
                sum = (sum + add).Shorten();
                yield return (k, sum);
            }
        }
    }
}
