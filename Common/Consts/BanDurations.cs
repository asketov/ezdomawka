using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Consts
{
    public static class BanDurations
    {
        public static Dictionary<string, int> DurationInDays = new Dictionary<string, int>()
        {
            {"day", 1}, {"month", 30}, {"year", 365}, {"forever", 1000000}
        };
    }
}
