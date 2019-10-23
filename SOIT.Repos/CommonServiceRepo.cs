using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Repos
{
    public class CommonServiceRepo : ICommonServiceRepo
    {
        public string _GetSystemTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
