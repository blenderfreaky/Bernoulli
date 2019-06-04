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
            //HarmonicDeviation(10000);
            BernoulliNumbers().Print(-1);


        }

        /*public static FracRow FactorialApprox()
        {
            for (BigInteger n = 0; ; n++) yield return (n, Math.Sqrt(2*Math.PI*n));
        }*/

        public static IntRow Factorial()
        {
            BigInteger product = 1;
            for (BigInteger i = 1; ; i++) yield return (i-1, product *= i);
        }

        public static void ApproximateGamma(int m, int n)
        {

        }

        public static void Get(this DecRow row, BigInteger index) => row.First(x => x.index == index);
        public static void Get(this FracRow row, BigInteger index) => row.First(x => x.index == index);

        public static void Print(this DecRow row, BigInteger count)
        {
            foreach (var (index, value) in row)
            {
                if (count-- == 0) return;
                Console.WriteLine($"{index}: {value}");
            }
        }
        public static void Print(this FracRow row, BigInteger count)
        {
            foreach (var (index, value) in row)
            {
                if (count-- == 0) return;
                Console.WriteLine($"{index}: {value}");
            }
        }

        public static void HarmonicDeviation() => Harmonic().Select(x => (x.index, x.value - (decimal)Math.Log((ulong)x.index))).Print(-1);

        public static DecRow Harmonic()
        {
            decimal sum = 0;

            for (ulong k = 1; ; k++) yield return (k, sum += 1m / k);
        }

        private static FracRow BernoulliNumbers()
        {
            List<Fractional> bernoulli = new List<Fractional>();
            bernoulli.Add(1);
            Console.WriteLine($"B_{0}: {bernoulli[0]}");

            for (int n = 1; ; n++)
            {
                Fractional currentSum = 0;
                for (int k = 0; k < n; k++)
                {
                    currentSum += Choose(n + 1, k) * bernoulli[k];
                }

                Fractional newVal = (-currentSum / Choose(n + 1, n)).Shorten();
                bernoulli.Add(newVal);
                yield return (n, newVal);
            }
        }

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
    }
}
