using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SquareEquationLayers
{
    public class SquareEquationViewModel : INotifyPropertyChanged
    {
        protected Double _a;
        public Double A
        {
            get { return _a; }
            set
            {
                _a = value;
                ChangeProperty("A");
            }
        }
        protected Double _b;
        public Double B
        {
            get { return _b; }
            set
            {
                _b = value;
                ChangeProperty("B");
            }
        }
        protected Double _c;
        public Double C
        {
            get { return _c; }
            set
            {
                _c = value;
                ChangeProperty("C");
            }
        }

        public Double X1
        {
            get;
            private set;
        }

        public Double X2
        {
            get;
            private set;
        }

        public Boolean HasRoots
        {
            get;
            private set;
        }

        public Visibility ShowData
        {
            get;
            set;
        }


        protected String _logicTypeName;
        IBusinessLogic _logic;
        protected IBusinessLogic Logic
        {
            get
            {
                //Используем паттерн "отложенная инициализация" - логика будет инициализироваться только при 
                //первом использовании
                if (_logic == null)
                {
                    if (_logicTypeName != String.Empty)
                    {
                        Type t = Type.GetType(_logicTypeName);
                        _logic = (IBusinessLogic)(Activator.CreateInstance(t));
                    }
                }
                return _logic;
            }
            set
            {
                _logic = value;
            }
        }

        protected void SetApplicationPropertyValue(String property, String value)
        {
            if (value != String.Empty)
            {
                Object source = App.Current.Properties[property];
                if (source == null)
                {
                    App.Current.Properties.Add(property, value);                    
                }
                else
                {
                    App.Current.Properties[property] = value;

                }
            }
        }

        public String SourceFilePath
        {
            get
            {
                Object source = App.Current.Properties["source"];
                if (source == null)
                {
                    App.Current.Properties.Add("source", "1.txt");
                    source = "1.txt";
                }

                return source.ToString();
            }
            set
            {
                SetApplicationPropertyValue("source", value.ToString());                
                ChangeProperty("SourceFilePath");
                TrySolveEquation();
            }
        }
        public String ResultsFilePath
        {
            get
            {
                Object results = App.Current.Properties["results"];
                if (results == null)
                {
                    App.Current.Properties.Add("results", "2.txt");
                    results = "1.txt";
                }

                return results.ToString();
            }
            set
            {
                SetApplicationPropertyValue("results", value.ToString());                
                ChangeProperty("ResultsFilePath");
                TrySolveEquation();
            }
        }

        protected void TrySolveEquation()
        {
            try
            {
                Logic.Solve();
                this.A = Logic.A;
                this.B = Logic.B;
                this.C = Logic.C;
                this.X1 = Logic.X1;
                this.X2 = Logic.X2;
                this.HasRoots = Logic.HasRoots;
                ChangeProperty("A");
                ChangeProperty("B");
                ChangeProperty("C");
                ChangeProperty("X1");
                ChangeProperty("X2");
                ChangeProperty("HasRoots");
                ShowData = Visibility.Visible;
                ChangeProperty("ShowData");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                ShowData = Visibility.Hidden;
                ChangeProperty("ShowData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void ChangeProperty(String propertyName)
        {
            var h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SquareEquationViewModel(String logicType)
        {
            _logicTypeName = logicType;
            ShowData = Visibility.Hidden;
        }

    }
}
