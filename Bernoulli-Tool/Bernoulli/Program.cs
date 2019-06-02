using System;

namespace Bernoulli
{
    class Program
    {
        static void Main(string[] args)
        {
            BernoulliNumbers();
        }

        private static void BernoulliNumbers()
        {
            Fractional[] bernoulli = new Fractional[100];
            bernoulli[0] = 1;
            Console.WriteLine($"B_{0}: {bernoulli[0]}");

            for (long n = 1; n < 50; n++)
            {
                Fractional currentSum = 0;
                for (long k = 0; k < n; k++)
                {
                    currentSum += Choose(n + 1, k) * bernoulli[k];
                }
                bernoulli[n] = -currentSum / Choose(n + 1, n);

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
