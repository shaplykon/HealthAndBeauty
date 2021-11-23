using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models.OrderModels
{
    public class HistoryGroup
    {
        public int Key { get; set; }

        public List<History> History { get; set; }
        public int Count { get; set; }
    }
}
