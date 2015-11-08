using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareEquationLayers
{
    public class WrongFileFormatException : Exception
    {
        public WrongFileFormatException() : base("Ошибочный формат файла с данными!") { }
    }

    public class WrongSaveDataFormatException : Exception
    {
        public WrongSaveDataFormatException() : base("Данные для сохранения неверны") { }
    }

    /// <summary>
    /// DAL, реализованный в виде работы с текстовым файлом
    /// </summary>
    public class TextFileDAL : IDAL
    {
        protected String _sourceFile;
        protected String _resultsFile;

        public TextFileDAL()
        {
            if (App.Current != null)
            {
                _sourceFile = App.Current.Properties["source"].ToString();
                _resultsFile = App.Current.Properties["results"].ToString();
            }
            else
            {
                _sourceFile = "source.txt";
                _resultsFile = "results.txt";
            }
        }

        /// <summary>
        /// Вытаскивает данные из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Строку с данными</returns>
        protected String GetDataFromFile(String fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            String s = reader.ReadToEnd();
            reader.Close();

            return s;
        }

        /// <summary>
        /// Загружает данные из текстового файла
        /// </summary>
        /// <returns>Массив с данными</returns>
        public Double[] LoadData()
        {
            Double[] result = new Double[3];
            String s = GetDataFromFile(_sourceFile);

            String[] parsed = s.Split(';');
            if (parsed.Length == 3)
            {
                for (Int32 i = 0; i < 3; i++)
                {
                    try
                    {
                        result[i] = Double.Parse(parsed[i]);
                    }
                    catch (FormatException e)
                    {
                        //Если возникает ошибка приведения распарсенного элемента в Double - выдаём 
                        //WrongFileFormat
                        throw new WrongFileFormatException();
                    }
                }
                return result;
            }
            else
            {
                throw new WrongFileFormatException();
            }
        }


        /// <summary>
        /// Сохраняет данные в текстовый файл
        /// </summary>
        /// <param name="data">Корни уравнения</param>
        /// <param name="hasRoots">Есть ли корни</param>
        public void SaveData(double[] data, Boolean hasRoots)
        {
            if (data.Length == 2)
            {
                String dataToSave = String.Empty;
                foreach (Double d in data)
                {
                    dataToSave += d.ToString();
                    dataToSave += ";";
                }
                dataToSave += hasRoots.ToString();
                StreamWriter sw = new StreamWriter(_resultsFile);
                sw.Write(dataToSave);
                sw.Close();
            }
            else
            {
                throw new WrongSaveDataFormatException();
            }
        }
    }
}
