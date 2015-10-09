using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExample
{
    public class Exam
    {
        public String Name { get; set; }
        public Byte Mark { get; set; }
        public DateTime? PassedDate { get; set; }

        public Exam(String name, Byte mark, DateTime? passed) 
        {
            Name = name;
            Mark = mark;
            PassedDate = passed;
        }
    }

    public class Student
    {
        public String Name { get; set; }
        public String MarkbookNumber { get; set; }

        public List<Exam> Exams { get; set; }

        public Student(String name, String markbook)
        {
            Exams = new List<Exam>();
            Exams.Add(new Exam("Дискретная математика", 4, DateTime.Parse("12.01.2015")));
            Exams.Add(new Exam("Программирование", 0, null));
            Exams.Add(new Exam("Математический анализ", 5, DateTime.Parse("15.01.2015")));
            Name = name;
            MarkbookNumber = markbook;
        }
    }
}
