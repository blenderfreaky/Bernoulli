import abc
import math
from fractions import Fraction
from decimal import Decimal
from matplotlib import pyplot as plt
from scipy import integrate
from scipy.special import binom


class LazyList:
    def __init__(self):
        self.sequence = []

    def __getitem__(self, key: int):
        while len(self.sequence)-1 <= key:
            self._calculate_next()
        return self.sequence[key]

    @abc.abstractmethod
    def _calculate_next(self):
        raise NotImplementedError


class Bernoulli(LazyList):
    def __init__(self):
        super().__init__()
        self.sequence = [Fraction(1)]

    def _calculate_next(self):
        n = len(self.sequence)
        bn = -1 * (sum(Fraction(binom(n+1, k)) * self.sequence[k]
                       for k in range(n))
                   / Fraction(binom(n+1, n)))
        self.sequence.append(bn)

    def plot(self):
        plt.plot(tuple(range(len(self.sequence))), self.sequence)
        plt.show()


class BernoulliPolynomial:
    def __init__(self, n: int, bern = Bernoulli()):
        self.n = n
        self.bern = bern

    def __call__(self, x):
        return sum(Fraction(binom(self.n, k)) * self.bern[k] * x**(self.n-k)
                   for k in range(self.n+1))


class PeriodicalBernoulliPolynomial(BernoulliPolynomial):
    def __call__(self, x):
        return super().__call__(x - (int(x) if int(x) < x else int(x)-1))


def gamma(n, m, bern = Bernoulli()):
    Hn = sum(1/k for k in range(1, n+1))
    bsum = sum(bern[k]/(k * n**k) for k in range(2, m+1))
    pintegral = 0  # TODO
    return Hn - math.log(n) - 1/(2*n) + bsum - pintegral


def test_bern():
    b = Bernoulli()
    for i in range(10):
        print(b[i])
    b.plot()

def test_bern_pol():
    bp = BernoulliPolynomial(2)
    print(bp(-0.25))

def test_periodical_bern_pol():
    pbp = PeriodicalBernoulliPolynomial(1)
    a, b, c = pbp(0.75), pbp(3.75), pbp(-7.25)
    print(f"P(0.75): {a}; P(3.75): {b}; P(-7.25): {c}")
    assert a == b == c

def test_gamma():
    y = gamma(n=10, m=3)
    print(y)

