using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Bernoulli
{
    public class Fractional
    {
        public BigInteger numerator;
        public BigInteger denominator;

        public Fractional(BigInteger numerator, BigInteger denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }


        public static Fractional operator +(Fractional left, Fractional right) =>
            new Fractional(left.numerator*right.denominator + right.numerator*left.denominator, left.denominator * right.denominator);

        public static Fractional operator -(Fractional value) => new Fractional(-value.numerator, value.denominator);

        public static Fractional operator -(Fractional left, Fractional right) =>
            left + (-right);

        public static Fractional operator *(Fractional left, Fractional right) =>
            new Fractional(left.numerator * right.numerator, left.denominator * right.denominator);

        public static Fractional operator /(Fractional left, Fractional right) =>
            new Fractional(left.numerator * right.denominator, left.denominator * right.numerator);


        public Fractional Shorten() => Shorten(BigInteger.GreatestCommonDivisor(numerator, denominator));

        public Fractional Shorten(BigInteger factor) => new Fractional(numerator / factor * denominator.Sign, denominator / factor * denominator.Sign);

        public static implicit operator Fractional(long val) => new Fractional(val, 1);
        public static implicit operator Fractional(BigInteger val) => new Fractional(val, 1);
        public static implicit operator BigDecimal(Fractional val) => 
            new BigDecimal(val.numerator, 0) / new BigDecimal(val.denominator, 0);

        public override string ToString() => $"{numerator} / {denominator}";

    }
}
