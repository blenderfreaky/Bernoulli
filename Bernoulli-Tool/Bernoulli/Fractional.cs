using System;
using System.Collections.Generic;
using System.Text;

namespace Bernoulli
{
    public class Fractional
    {
        public long numerator;
        public long denominator;
        public decimal Value => numerator / denominator;

        public Fractional(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Fractional operator +(Fractional left, Fractional right) =>
            new Fractional(left.numerator*right.denominator + right.numerator*left.denominator, left.denominator * right.denominator).Shorten();

        public static Fractional operator -(Fractional value) => new Fractional(-value.numerator, value.denominator);

        public static Fractional operator -(Fractional left, Fractional right) =>
            left + (-right);

        public static Fractional operator *(Fractional left, Fractional right) =>
            new Fractional(left.numerator * right.numerator, left.denominator * right.denominator).Shorten();

        public static Fractional operator /(Fractional left, Fractional right) =>
            new Fractional(left.numerator * right.denominator, left.denominator * right.numerator).Shorten();


        private static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return a == 0 ? b : a;
        }

        public Fractional Shorten() => Shorten(GCD(numerator, denominator));

        public Fractional Shorten(long factor) => new Fractional(numerator / factor, denominator / factor) * Math.Sign(denominator);

        public static implicit operator Fractional(long val) => new Fractional(val, 1);

        public override string ToString() => $"{numerator} / {denominator}";
    }
}
