using Microsoft.FSharp.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Bernoulli
{
    public class BernoulliNumbers : Row<BigInteger, Fractional>
    {
        internal override IEnumerable<(BigInteger, Fractional)> Yielder()
        {
            List<Fractional> bernoulli = new List<Fractional>();
            bernoulli.Add(1);
            Console.WriteLine($"B_{0}: {bernoulli[0]}");

            for (int n = 1; ; n++)
            {
                Fractional currentSum = 0;
                for (int k = 0; k < n; k++)
                {
                    currentSum += MoreMath.Choose(n + 1, k) * bernoulli[k];
                }

                Fractional newVal = (-currentSum / MoreMath.Choose(n + 1, n)).Shorten();
                bernoulli.Add(newVal);
                yield return (n, newVal);
            }
        }
    }

    public class BernoulliPolynomial : RARow<BigDecimal, BigDecimal>
    {
        public int n;
        public BigDecimal[] constants;

        public BernoulliPolynomial(int n)
        {
            this.n = n;
            BernoulliNumbers bernoulli = new BernoulliNumbers();
            constants = bernoulli[0, n+1].Select(x => (BigDecimal)x.value * MoreMath.Choose(n, x.index)).ToArray();
        }

        internal override BigDecimal Func(BigDecimal index)
        {
            BigDecimal sum;

            for (int i = 0; i < n+1; i++) sum += constants[i] * BigDecimal.po
        }

        internal override IEnumerable<BigDecimal> Indexer()
        {
            throw new NotImplementedException();
        }
    }
}
