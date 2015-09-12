using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqEq
{
    /// <summary>
    /// Квадратное уравнение
    /// </summary>
    public class SquareEquation
    {
        /// <summary>
        /// Текущая стратегия расчёта дискриминанта
        /// </summary>
        protected IDiscriminantStrategy _discriminantStrategy;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SquareEquation()
        {
            _discriminantStrategy = new UsualDiscriminantStrategy();
        }

        /// <summary>
        /// Конструктор с переданной стратегией
        /// </summary>
        /// <param name="strategy">Стратегия</param>
        public SquareEquation(IDiscriminantStrategy strategy)
        {
            _discriminantStrategy = strategy;
        }

        /// <summary>
        /// Решение уравнения
        /// </summary>
        /// <param name="a">Коэффициент А</param>
        /// <param name="b">Коэффициент B</param>
        /// <param name="c">Коэффициент C</param>
        /// <returns></returns>
        public Double[] Solve(Double a, Double b, Double c)
        {
            try
            {
                Double d = this._discriminantStrategy.GetDiscriminant(a, b, c);
                if (d >= 0)
                {
                    Double[] result = new Double[2];
                    result[0] = (-b + Math.Sqrt(d)) / 2 / a;
                    result[1] = (-b - Math.Sqrt(d)) / 2 / a;
                    return result;
                }
                else
                {
                    return new Double[0];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
