using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using SquareEquationLayers;
using System.Reflection;

namespace SquareEquationLayersTests
{
    [TestClass]
    public class TextFileDALTests
    {
        #region Вспомогательные методы, которые создают и потом удаляют текстовый файл с данными тестов
        protected void CreateTestFile(String fileName, String content)
        {
            TextWriter tw = new StreamWriter(fileName);
            tw.WriteLine(content);
            tw.Close();
        }
        protected String LoadTestFile(String fileName)
        {
            String content = String.Empty;
            if (File.Exists(fileName))
            {
                TextReader tr = new StreamReader(fileName);
                content = tr.ReadToEnd();
                tr.Close();
            }
            return content;
        }
        #endregion

        [TestMethod]
        public void TestLoadFileShouldSuccess()
        {
            //Создаём текстовый файл
            CreateTestFile("1.txt", "1;2;3");
            TextFileDAL dal = new TextFileDAL();

            //С помощью отражения присваиваем защищённому полю значение
            FieldInfo fi = dal.GetType().GetField("_sourceFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");

            //Запускаем метод
            Double[] d = dal.LoadData();

            //Проверяем результат
            Assert.AreEqual(3, d.Length);
            Assert.AreEqual(1, d[0]);
            Assert.AreEqual(2, d[1]);
            Assert.AreEqual(3, d[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestLoadFileShouldThrowFileNotFound()
        {
            CreateTestFile("1.txt", "1;2;3");
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_sourceFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "2.txt");

            Double[] d = dal.LoadData();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestLoadFileShouldThrowWronFileFormatOn4Values()
        {
            CreateTestFile("1.txt", "1;2;3;12");
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_sourceFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");

            Double[] d = dal.LoadData();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestLoadFileShouldThrowWronFileFormatOn2Values()
        {
            CreateTestFile("1.txt", "1;2");
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_sourceFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");

            Double[] d = dal.LoadData();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFileFormatException))]
        public void TestLoadFileShouldThrowWronFileFormatOnNotNumbers()
        {
            CreateTestFile("1.txt", "1;a;2");
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_sourceFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");

            Double[] d = dal.LoadData();
        }


        [TestMethod]
        public void SaveDataTestShouldSuccess()
        {
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_resultsFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");
            Double[] dataToSave = new Double[2] { 1, 2 };
            Boolean hasRoots = true;

            dal.SaveData(dataToSave, hasRoots);

            String result = LoadTestFile("1.txt");
            Assert.AreEqual("1;2;True", result);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongSaveDataFormatException))]
        public void SaveDataTestShouldFailOn3Numbers()
        {
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_resultsFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");
            Double[] dataToSave = new Double[3] { 1, 2, 3 };
            Boolean hasRoots = true;

            dal.SaveData(dataToSave, hasRoots);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongSaveDataFormatException))]
        public void SaveDataTestShouldFailOn1Number()
        {
            TextFileDAL dal = new TextFileDAL();

            FieldInfo fi = dal.GetType().GetField("_resultsFile",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fi.SetValue(dal, "1.txt");
            Double[] dataToSave = new Double[1] { 1 };
            Boolean hasRoots = true;

            dal.SaveData(dataToSave, hasRoots);
        }

    }
}
