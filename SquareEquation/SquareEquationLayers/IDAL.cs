using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareEquationLayers
{
    public interface IDAL
    {
        Double[] LoadData();
        void SaveData(Double[] data, Boolean hasRoots);
    }
}
