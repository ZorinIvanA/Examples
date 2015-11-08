using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareEquationLayers
{
    public class WrongDALException : Exception
    {
        public WrongDALException() : base("Ошибка доступа к данным!") { }
    }

    public interface IBusinessLogic
    {
        Double A { get; set; }
        Double B { get; set; }
        Double C { get; set; }

        Double X1 { get; set; }
        Double X2 { get; set; }
        Boolean HasRoots { get; set; }

        void Solve();
    }

    public class BL : IBusinessLogic
    {
        public Double A { get; set; }
        public Double B { get; set; }
        public Double C { get; set; }

        public Double X1 { get; set; }
        public Double X2 { get; set; }
        public Boolean HasRoots { get; set; }

        protected IDAL _dal;
        
        protected void CreateDAL()
        {
            if (App.Current != null)
            {
                String dalText = Properties.Settings.Default["DAL"].ToString();
                Type t = Type.GetType(dalText);
                _dal = (IDAL)(Activator.CreateInstance(t));
            }
        }

        public BL()
        {
            CreateDAL();
        }

        /// <summary>
        /// Решение уравнения
        /// </summary>
        public void Solve()
        {
            try
            {
                CreateDAL();
                GetDataFromSource();
            }
            catch
            {
                throw new WrongDALException();
            }

            FindRoots();

            Double[] roots = new Double[2] { X1, X2 };
            try
            {
                _dal.SaveData(roots, HasRoots);
            }
            catch
            {
                throw new WrongDALException();
            }

        }

        private void FindRoots()
        {
            Double d = B * B - 4 * A * C;
            if (d >= 0)
            {
                X1 = (-B + Math.Sqrt(d)) / 2 / A;
                X2 = (-B - Math.Sqrt(d)) / 2 / A;
                HasRoots = true;
            }
            else
            {
                HasRoots = false;
            }
        }

        private void GetDataFromSource()
        {
            Double[] source = _dal.LoadData();
            A = source[0];
            B = source[1];
            C = source[2];
        }
    }
}
