using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data
{
    public static class DataUtilities
    {

        public static int getscore(string teststr, ref string filterstr)
        {
            if (string.IsNullOrEmpty(teststr)) return 0;
            return teststr.ToLower().Contains(filterstr) ? 1 : 0;
        }
    }
}
