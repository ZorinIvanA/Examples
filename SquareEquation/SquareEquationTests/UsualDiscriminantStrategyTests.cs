using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SqEq;

namespace SquareEquationTests
{
    [TestClass]
    public class UsualDiscriminantStrategyTests
    {
        [TestMethod]
        public void TestGetDiscriminantShouldReturnValue()
        {
            UsualDiscriminantStrategy strategy = new UsualDiscriminantStrategy();
            Double result = strategy.GetDiscriminant(1, 2, 1);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSquareEquationException))]
        public void TestGetDiscriminantShouldThrowNoSquareEquation()
        {
            UsualDiscriminantStrategy strategy = new UsualDiscriminantStrategy();
            Double result = strategy.GetDiscriminant(0, 2, 1);
        }
    }
}
