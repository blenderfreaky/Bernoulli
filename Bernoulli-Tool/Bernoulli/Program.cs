using System;

namespace Bernoulli
{
    class Program
    {
        static void Main(string[] args)
        {
            BernoulliNumbers(100000);
        }

        private static void BernoulliNumbers(int amount)
        {
            Fractional[] bernoulli = new Fractional[amount];
            bernoulli[0] = 1;
            Console.WriteLine($"B_{0}: {bernoulli[0]}");

            for (long n = 1; n < amount; n++)
            {
                Fractional currentSum = 0;
                for (long k = 0; k < n; k++)
                {
                    currentSum += Choose(n + 1, k) * bernoulli[k];
                }
                bernoulli[n] = (-currentSum / Choose(n + 1, n)).Shorten();

                Console.WriteLine($"B_{n}: {bernoulli[n]}");
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
