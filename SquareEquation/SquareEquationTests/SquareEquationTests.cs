using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SqEq;

namespace SquareEquationTests
{
    /// <summary>
    /// Заглушка расчёта дискриминанта - возвращает значение, переданное в конструкторе
    /// </summary>
    public class DiscriminantStrategyMock : IDiscriminantStrategy
    {
        protected Double _discriminant;

        public DiscriminantStrategyMock(Double discriminant)
        {
            _discriminant = discriminant;
        }

        public double GetDiscriminant(double a, double b, double c)
        {
            return _discriminant;
        }
    }

    [TestClass]
    public class SquareEquationTests
    {
        [TestMethod]
        public void TestSolveShouldReturn2Values()
        {
            SquareEquation eq = new SquareEquation(new DiscriminantStrategyMock(9));
            Double[] result = eq.Solve(1, 4, 0);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-0.5, result[0]);
            Assert.AreEqual(-3.5, result[1]);
        }

        [TestMethod]
        public void TestSolveShouldReturnSameValues()
        {
            SquareEquation eq = new SquareEquation(new DiscriminantStrategyMock(0));
            Double[] result = eq.Solve(1, 4, 0);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(-2, result[0]);
            Assert.AreEqual(-2, result[1]);
        }

        [TestMethod]
        public void TestSolveShouldReturnNoValues()
        {
            SquareEquation eq = new SquareEquation(new DiscriminantStrategyMock(-1));
            Double[] result = eq.Solve(1, 4, 0);
            Assert.AreEqual(0, result.Length);
        }
    }
}
