using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public abstract class ItemBase
    {
        public Double Price { get; set; }
        public Int32 Amount { get; set; }

        public abstract Double Cost();
    }

    public abstract class ItemLeaf : ItemBase
    {
        public override double Cost()
        {
            return Price * Amount;
        }
    }

    public class RAM : ItemLeaf
    {

    }

    public abstract class ItemComposite : ItemBase
    {
        public IList<ItemBase> Items { get; set; }

        public override double Cost()
        {
            Double sum = 0;
            foreach (var item in Items)
            {
                sum += item.Cost();
            }

            return sum;
        }
    }

    public class Order : ItemComposite
    {

    }
}
