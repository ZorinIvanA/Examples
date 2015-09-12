using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqEq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите а");
            Double a = Double.Parse(Console.ReadLine());
            Console.WriteLine("Введите b");
            Double b = Double.Parse(Console.ReadLine());
            Console.WriteLine("Введите c");
            Double c = Double.Parse(Console.ReadLine());

            try
            {
                Double[] result = new SquareEquation().Solve(a, b, c);
                if (result.Length > 0)
                {
                    Console.WriteLine(result[0]);
                    Console.WriteLine(result[1]);
                }
                else
                {
                    Console.WriteLine("Действительных корней нет!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
