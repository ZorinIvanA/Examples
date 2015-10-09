using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataBindingExample
{
    public class ExamViewModel : INotifyPropertyChanged
    {
        public String Name { get; set; }
        public DateTime Passed { get; set; }
        protected Byte _mark;
        public Byte Mark
        {
            get { return _mark; }
            set
            {
                _mark = value;
                Color = (_mark > 2) ? "Green" : "Red";
            }
        }

        protected String _color;
        public String Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String property)
        {
            var ev = PropertyChanged;
            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(property));
            }
        }

        public ExamViewModel(Exam model)
        {
            this.Name = model.Name;
            this.Passed = (model.PassedDate != null) ? model.PassedDate.Value : DateTime.Now;
            this.Mark = model.Mark;
        }
    }

    public class StudentViewModel
    {
        public String Name { get; set; }
        public String Markbook { get; set; }

        public ObservableCollection<ExamViewModel> Exams { get; set; }

        public StudentViewModel(Student student)
        {
            Name = student.Name;
            Markbook = student.MarkbookNumber;

            Exams = new ObservableCollection<ExamViewModel>();
            foreach (var e in student.Exams)
            {
                Exams.Add(new ExamViewModel(e));
            }
        }
    }
}
