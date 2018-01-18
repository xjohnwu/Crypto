using System.Collections.Generic;

namespace Crypto.Lib.Model
{
    public class HistoricalData
    {
        public IEnumerable<DataPoint> Data { get; set; }
        public long TimeTo { get; set; }
        public long TimeFrom { get; set; }
    }
}