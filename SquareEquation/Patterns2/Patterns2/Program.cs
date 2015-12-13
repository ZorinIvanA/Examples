using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


namespace Patterns2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Цепочка ответственности
            //MyTask task = new MyTask() { Rank = 10 };

            //Operator op = new Operator();
            //op.HandleTask(task);
            #endregion

            #region Наблюдатель
            //Observer observer1 = new Observer("1");
            //Observer observer2 = new Observer("2");

            //Observable observable1 = new Observable();
            //Observable observable2 = new Observable();
            //observable1.AddObserver(observer1);
            //observable1.AddObserver(observer2);
            //observable2.AddObserver(observer1);

            //observable1.NotifyObservers();
            //observable2.NotifyObservers();
            #endregion

            #region Посетитель
            //Visitor visitor = new Visitor("Визитёр");
            //Visitable target1 = new Visitable("Цель 1");
            //Visitable target2 = new Visitable("Цель 2");
            //visitor.Targets.Add(target1);
            //visitor.Targets.Add(target2);

            //visitor.VisitTargets();
            #endregion

            #region Шаблонный метод
            //MyJob1 job = new MyJob1();
            //job.Execute();
            #endregion

            Console.ReadLine();
        }
    }
}
