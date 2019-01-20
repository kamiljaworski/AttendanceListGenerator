using System.Collections.Generic;

namespace AttendanceListGenerator.Core
{
    public class MonthData : IMonthData
    {
        public IList<IDay> Days { get; private set; }
        public Month Month { get; private set; }
        public int Year { get; private set; }
    }
}
