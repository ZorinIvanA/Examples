using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    //Карта учитывает только выдачу
    public class DiscountCard
    {
        public String Number { get; set; }
        public String FIO { get; set; }
        public DateTime GivenDate { get; set; }
    }

    /// <summary>
    /// Декоратор на DiscountCard
    /// </summary>
    public class RevertableCard : DiscountCard
    {
        public void Return(String FIO, DateTime dateReturn)
        { }
    }
}
