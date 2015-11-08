using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquareEquationLayers;
using System.Reflection;

namespace SquareEquationLayersTests
{
    /// <summary>
    /// Создаём заглушку DAL, которая нужна чтобы красиво тестить бизнес-логику
    /// </summary>
    public class DALMock : IDAL
    {
        public Double[] coefs { get; set; }
        public String results { get; set; }

        public double[] LoadData()
        {
            return coefs;
        }

        public void SaveData(double[] data, bool hasRoots)
        {
            results = data[0].ToString() + ";" + data[1].ToString() + ";" + hasRoots.ToString();
        }
    }

    public class DALError : IDAL
    {

        public double[] LoadData()
        {
            throw new NotImplementedException();
        }

        public void SaveData(double[] data, bool hasRoots)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class BLTests
    {
        DALMock mock;

        [TestInitialize]
        public void TestInit()
        {
            mock = new DALMock();
        }

        [TestMethod]
        public void TestSolveHas2Roots()
        {
            BL logic = new BL();
            mock.coefs = new Double[3] { 1, 4, 3 };

            FieldInfo fi = logic.GetType().GetField("_dal",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(logic, mock);

            logic.Solve();
            Assert.AreEqual("-1;-3;True", mock.results);
        }

        [TestMethod]
        public void TestSolveHasSameRoots()
        {
            BL logic = new BL();
            mock.coefs = new Double[3] { 1, 4, 4 };

            FieldInfo fi = logic.GetType().GetField("_dal",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(logic, mock);

            logic.Solve();
            Assert.AreEqual("-2;-2;True", mock.results);
        }

        [TestMethod]
        public void TestSolveHasNoRoots()
        {
            BL logic = new BL();
            mock.coefs = new Double[3] { 1, 4, 20 };

            FieldInfo fi = logic.GetType().GetField("_dal",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(logic, mock);

            logic.Solve();
            Assert.AreEqual("0;0;False", mock.results);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDALException))]
        public void TestSolveShouldThrowWrongDALOnLoad()
        {
            BL logic = new BL();
            mock.coefs = new Double[3] { 1, 4, 20 };

            FieldInfo fi = logic.GetType().GetField("_dal",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(logic, new DALError());

            logic.Solve();
        }
    }
}
