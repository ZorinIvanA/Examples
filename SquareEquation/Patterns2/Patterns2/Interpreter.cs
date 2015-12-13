using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public class Interpreter
    {
        IDictionary<Type, String> ExcelFormats { get;set }
        public Interpreter()
        {
            ExcelFormats = new Dictionary<Type, String>();
            ExcelFormats.Add(typeof(Double), "# ##0,00");
            ExcelFormats.Add(typeof(Int32), "# ##0");
        }

        public void WriteToExcel(Object d)
        {
            Type t = d.GetType();
            String Format = ExcelFormats[t];

            //Cell.format = Format;
        }
    }
}
