using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public class CommonApi
    {
        public void GetData()
        { }
    }

    public class WebSite
    {
        CommonApi api;
        public void GetDataFromDB() 
        {
            api.GetData();
        }
    }

    public class MobileApp 
    {
        CommonApi api;
        public void GetDataFromApi()
        {
            api.GetData();
        }
    }

    public class WPFApp 
    {
        public void GetDataFromAnotherApi() { }
    }
}
