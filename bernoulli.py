import abc
import math
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
        self.sequence = [Decimal(1)]

    def _calculate_next(self):
        n = len(self.sequence)
        bn = -1 * (sum(Decimal(binom(n+1, k)) * self.sequence[k]
                       for k in range(n))
                   / Decimal(binom(n+1, n)))
        self.sequence.append(bn)

    def plot(self):
        seq = []
        for i, x in enumerate(self.sequence):
            if i % 2 == 1 or i < 3:
                seq.append(0)
            else:
                seq.append(x)
        sseq = logscale(seq)
        ax = plt.subplot()
        ax.plot(sseq, 'bx')
        ax.plot(sseq, 'r-', linewidth=0.4)
        plt.yticks(sseq[::10], ['%.1E' % x for x in seq[::10]])
        ax.legend([r"$B_n$"])
        plt.xlabel('x')
        plt.ylabel('y')
        ax.spines['right'].set_visible(False)
        ax.spines['top'].set_visible(False)
        ax.yaxis.set_ticks_position('left')
        ax.xaxis.set_ticks_position('bottom')
        plt.show()


class BernoulliPolynomial:
    def __init__(self, n: int, bern = Bernoulli()):
        self.n = n
        self.bern = bern

    def __call__(self, x):
        return sum(Decimal(binom(self.n, k))
                     * self.bern[k]
                     * Decimal(x**(self.n-k))
                   for k in range(self.n+1))


class PeriodicalBernoulliPolynomial(BernoulliPolynomial):
    def __call__(self, x):
        return super().__call__(x - (int(x) if int(x) <= x else int(x)-1))

    def plot(self, accuracy = 1000):
        data = [self(x / accuracy)
                for x in range(-10 * accuracy, 10 * accuracy + 1)]
        x = [x / accuracy
             for x in range(-10 * accuracy, 10 * accuracy + 1)]
        ax = plt.subplot()
        for i in range(0, len(data), accuracy):
            ax.plot(x[i:i+accuracy], data[i:i+accuracy], 'b')
        ax.plot([n for n in range(-10, 10)], [-0.5]*20, 'go',
                markersize=5.0)
        ax.plot([n for n in range(-9, 11)], [0.5]*20, 'ro',
                markersize=5.0, markerfacecolor='white')
        ax.legend([f"$P_{self.n}(x)$"])
        plt.xlabel('x')
        plt.ylabel('y')
        ax.spines['right'].set_visible(False)
        ax.spines['top'].set_visible(False)
        ax.yaxis.set_ticks_position('left')
        ax.xaxis.set_ticks_position('bottom')
        plt.show()

def gamma(n, m, bern = Bernoulli()):
    Hn = sum(1/k for k in range(1, n+1))
    bsum = sum(bern[k]/(k * n**k) for k in range(2, m+1))
    pintegral = 0  # TODO
    return Hn - math.log(n) - 1/(2*n) + bsum - pintegral


def logscale(seq):
    res = []
    for x in seq:
        x = Decimal(x)
        if x > 0:
            res.append(math.log(x+1))
        else:
            res.append(-math.log(abs(x)+1))
    return res


def test_bern(n=10):
    b = Bernoulli()
    for i, n in enumerate(range(n)):
        b[n]
        print(i)
    b.plot()

def test_bern_pol():
    bp = BernoulliPolynomial(2)
    print(bp(-0.25))

def test_periodical_bern_pol():
    pbp = PeriodicalBernoulliPolynomial(1)
    a, b, c = pbp(0.75), pbp(3.75), pbp(-7.25)
    print(f"P(0.75): {a}; P(3.75): {b}; P(-7.25): {c}")
    assert a == b == c
    pbp.plot()

def test_gamma():
    y = gamma(n=10, m=3)
    print(y)

if __name__ == '__main__':
    test_periodical_bern_pol()
