using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqEq
{
    /// <summary>
    /// Стратегия расчёта дискриминанта
    /// </summary>
    public interface IDiscriminantStrategy
    {
        Double GetDiscriminant(Double a, Double b, Double c);
    }

    /// <summary>
    /// Исключение, которое вылезает, если коэффициент А =0
    /// </summary>
    public class NotSquareEquationException : Exception
    {
        public NotSquareEquationException() :
            base("Это не квадратное уравнение! Коэффициент А не может быть равен 0!") { }
    }

    /// <summary>
    /// Обычный расчёт дискриминанта
    /// </summary>
    public class UsualDiscriminantStrategy : IDiscriminantStrategy
    {
        /// <summary>
        /// Расчёт дискриминанта через коэффициенты квадратного уравнения
        /// </summary>
        /// <param name="a">Коэффициент а</param>
        /// <param name="b">Коэффициент b</param>
        /// <param name="c">Коэффициент c</param>
        /// <returns>Значение дискриминанта</returns>
        public double GetDiscriminant(double a, double b, double c)
        {
            if (a != 0)
            {
                return b * b - 4 * a * c;
            }
            else
            {
                throw new NotSquareEquationException();
            }
        }
    }
}
