using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Bernoulli
{
    using DecRow = System.Collections.Generic.IEnumerable<(BigInteger index, decimal value)>;
    using FracRow = System.Collections.Generic.IEnumerable<(BigInteger index, Fractional value)>;
    using IntRow = System.Collections.Generic.IEnumerable<(BigInteger index, BigInteger value)>;

    static class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Harmonic().Get(10));
            HarmonicDeviation().Print(-1);
            //BernoulliNumbers().Print(-1);


        }
    }
}
